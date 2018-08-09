using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Insight.Database;
using Microsoft.AspNetCore.Mvc;

namespace BetVue.Web.Mvc.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static readonly string[] Summaries =
        {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDbInsight _db;


        public SampleDataController(IDbInsight db)
        {
            _db = db;
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                    DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                    TemperatureC  = rng.Next(-20, 55),
                    Summary       = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]/{dateIn?}")]
        public GamesForDateOutput GetGamesForDate(string dateIn)
        {
            if (string.IsNullOrWhiteSpace(dateIn) || !DateTime.TryParseExact(dateIn, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateParsed))
                dateParsed = DateTime.Now;

            var input = new GamesForDateInputParameters {dateIn = dateParsed};

            var games = _db.Connection.Query<GameListItem>("GetGamesForDate", input).OrderBy(g => g.Date)
                    .AsEnumerable().Select(x => new Game(x));

            return new GamesForDateOutput
            {
                    Date  = dateParsed,
                    Games = games
            };
        }

        [HttpGet("[action]/{gameId}")]
        public Stats GetStatsForGame(int gameId,[FromQuery]
                                     int? p1 = null,
                                     [FromQuery]int? p2 = null,
                                     [FromQuery]int? p3 = null,
                                     [FromQuery]int? p4 = null,
                                     bool? side = null)
        {
            var input = new StatsForGameInput
            {
                    gameId = gameId,
                    p1     = p1==null?null:p1==0?(bool?)false:true,
                    p2     = p2==null?null:p2==0?(bool?)false:true,
                    p3     = p3==null?null:p3==0?(bool?)false:true,
                    p4     = p4==null?null:p4==0?(bool?)false:true,
                    side   = side
            };
            var output = new StatsForGameOutputParameters();
            var items = _db.Connection.Query<GameStatsItem>("GetStatsForGame", input, outputParameters: output)
                    .GroupBy(g => g.Total).Select(x => x.First()).ToList();
            var gameLi = _db.Connection.Single<GameListItem>("GetGameForId", new {id = gameId});


            var stats = new Stats {Game = new Game(gameLi)};

            stats.Rasklad = new GameRasklad
            {
                    PrevUniq = output.uniq,
                    P1 = new PeriodRasklad
                    {
                            Id      = 1,
                            Name    = "1 четв",
                            OddAway = output.p1awy,
                            OddReal = stats.Game.Periods[1].Home > 0 || stats.Game.Periods[1].Away > 0
                                    ? (bool?) ((stats.Game.Periods[0].Home + stats.Game.Periods[0].Away) % 2 == 1)
                                    : null,
                            OddUser = p1,
                            PeriodStats = items.Select(x => new PeriodStatsItem
                            {
                                    Freq = x.P1_Per,
                                    Perc = x.P1_Avg,
                                    Sqrt = x.P1_Sqr
                            }).ToList()
                    },
                    P2 = new PeriodRasklad
                    {
                            Id      = 2,
                            Name    = "2 четв",
                            OddAway = output.p2awy,
                            OddReal = stats.Game.Periods[2].Home > 0 || stats.Game.Periods[2].Away > 0
                                    ? (bool?) ((stats.Game.Periods[1].Home + stats.Game.Periods[1].Away) % 2 == 1)
                                    : null,
                            OddUser = p2,
                            PeriodStats = items.Select(x => new PeriodStatsItem
                            {
                                    Freq = x.P2_Per,
                                    Perc = x.P2_Avg,
                                    Sqrt = x.P2_Sqr
                            }).ToList()
                    },
                    P3 = new PeriodRasklad
                    {
                            Id      = 3,
                            Name    = "3 четв",
                            OddAway = output.p3awy,
                            OddReal = stats.Game.Periods[3].Home > 0 || stats.Game.Periods[3].Away > 0
                                    ? (bool?) ((stats.Game.Periods[2].Home + stats.Game.Periods[2].Away) % 2 == 1)
                                    : null,
                            OddUser = p3,
                            PeriodStats = items.Select(x => new PeriodStatsItem
                            {
                                    Freq = x.P3_Per,
                                    Perc = x.P3_Avg,
                                    Sqrt = x.P3_Sqr
                            }).ToList()
                    },
                    P4 = new PeriodRasklad
                    {
                            Id      = 4,
                            Name    = "4 четв",
                            OddAway = output.p4awy,
                            OddReal = stats.Game.Status == "Завершена"
                                    ? (bool?) ((stats.Game.Periods[3].Home + stats.Game.Periods[3].Away) % 2 == 1)
                                    : null,
                            OddUser = p4,
                            PeriodStats = items.Select(x => new PeriodStatsItem
                            {
                                    Freq = x.P4_Per,
                                    Perc = x.P4_Avg,
                                    Sqrt = x.P4_Sqr
                            }).ToList()
                    },
                    TT = new PeriodRasklad
                    {
                            Id      = 6,
                            Name    = "Тотал",
                            OddAway = output.ttawy,
                            OddReal = stats.Game.Status == "Завершена"
                                    ? (bool?) ((stats.Game.Periods[5].Home + stats.Game.Periods[5].Away) % 2 == 1)
                                    : null,
                            OddUser = null,
                            PeriodStats = items.Select(x => new PeriodStatsItem
                            {
                                    Freq = x.TT_Per,
                                    Perc = x.TT_Avg,
                                    Sqrt = x.TT_Sqr
                            }).ToList()
                    }
            };

            for (var i = 0; i < items.Count; i++)
                stats.Rasklad.Statistics.Add(new GameStatsRow
                {
                        Count = items[i].Total,
                        Descr = items[i].Descr,
                        P1    = stats.Rasklad.P1.PeriodStats[i],
                        P2    = stats.Rasklad.P2.PeriodStats[i],
                        P3    = stats.Rasklad.P3.PeriodStats[i],
                        P4    = stats.Rasklad.P4.PeriodStats[i],
                        TT    = stats.Rasklad.TT.PeriodStats[i]
                });
            return stats;
        }


        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int    TemperatureC  { get; set; }
            public string Summary       { get; set; }

            public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
        }
    }
}
