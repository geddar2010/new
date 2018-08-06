using System;
using System.Collections.Generic;
using System.Data;
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

        private IDbConnection dbConn => _db.Connection;

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var gamesForDateInput = new GamesForDateInputParameters();
            var games             = _db.Connection.Query<GameListItem>("GetGamesForDate", gamesForDateInput);

            var gameStatsInput  = new StatsForGameInput {gameId = 10787343};
            var gameStatsOutput = new StatsForGameOutputParameters();
            var stats = _db.Connection.Query<GameStatsItem>("GetStatsForGame", gameStatsInput,
                    outputParameters: gameStatsOutput);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                    DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                    TemperatureC  = rng.Next(-20, 55),
                    Summary       = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public GamesForDateOutput GetGamesForDate(string dateIn)
        {
            var dateParsed = DateTime.Now;

            if (string.IsNullOrWhiteSpace(dateIn) || !DateTime.TryParseExact(dateIn, "dd.MM.yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateParsed))
                dateParsed = DateTime.Now;

            var input = new GamesForDateInputParameters {dateIn = dateParsed};

            var games = _db.Connection.Query<GameListItem>("GetGamesForDate", input).OrderBy(g => g.Date)
                    .AsEnumerable();

            return new GamesForDateOutput
            {
                    DateFormatted = $"{dateParsed:dd.MM.yyyy}",
                    Games         = games
            };
        }

        [HttpGet("[action]/{gameId}")]
        public StatsForGameOutput GetStatsForGame(int gameId,
                                                  bool? p1 = null,
                                                  bool? p2 = null,
                                                  bool? p3 = null,
                                                  bool? p4 = null,
                                                  bool? side = null)
        {
            var input = new StatsForGameInput
            {
                    gameId = gameId,
                    p1     = p1,
                    p2     = p2,
                    p3     = p3,
                    p4     = p4,
                    side   = side
            };
            var output = new StatsForGameOutputParameters();
            var items = _db.Connection.Query<GameStatsItem>("GetStatsForGame", input, outputParameters: output)
                    .AsEnumerable();

            return new StatsForGameOutput
            {
                    Stats = items,
                    p1awy = output.p1awy,
                    p2awy = output.p2awy,
                    p3awy = output.p3awy,
                    p4awy = output.p4awy,
                    ttawy = output.ttawy,
                    uniq  = output.uniq
            };
        }

        public class StatsForGameOutput
        {
            public IEnumerable<GameStatsItem> Stats { get; set; }

            public int uniq { get; set; }

            public int p1awy { get; set; }
            public int p2awy { get; set; }
            public int p3awy { get; set; }
            public int p4awy { get; set; }
            public int ttawy { get; set; }
        }

        public class GamesForDateInputParameters
        {
            public DateTime? dateIn { get; set; }
        }

        [Serializable]
        public class GamesForDateOutput
        {
            public string DateFormatted { get; set; }

            public IEnumerable<GameListItem> Games { get; set; }
        }

        [Serializable]
        public class GameListItem
        {
            public int      Id   { get; set; }
            public DateTime Date { get; set; }
            public DateTime UpdD { get; set; }
            public int      Sc1H { get; set; }
            public int      Sc1A { get; set; }
            public int      Sc2H { get; set; }
            public int      Sc2A { get; set; }
            public int      Sc3H { get; set; }
            public int      Sc3A { get; set; }
            public int      Sc4H { get; set; }
            public int      Sc4A { get; set; }
            public int      ScOH { get; set; }
            public int      ScOA { get; set; }
            public int      ScTH { get; set; }
            public int      ScTA { get; set; }
            public string   Stat { get; set; }
            public int      CntI { get; set; }
            public string   CntN { get; set; }
            public int      LeaI { get; set; }
            public string   LeaN { get; set; }
            public int      SeaI { get; set; }
            public string   SeaN { get; set; }
            public int      StgI { get; set; }
            public string   StgN { get; set; }
            public int      THId { get; set; }
            public string   THNm { get; set; }
            public int      TAId { get; set; }
            public string   TANm { get; set; }
        }

        public class StatsForGameInput
        {
            public int   gameId { get; set; }
            public bool? p1     { get; set; }
            public bool? p2     { get; set; }
            public bool? p3     { get; set; }
            public bool? p4     { get; set; }
            public bool? side   { get; set; }
            public int?  uniq   { get; set; } = null;
            public int?  p1awy  { get; set; } = null;
            public int?  p2awy  { get; set; } = null;
            public int?  p3awy  { get; set; } = null;
            public int?  p4awy  { get; set; } = null;
            public int?  ttawy  { get; set; } = null;
        }

        public class StatsForGameOutputParameters
        {
            public int uniq  { get; set; }
            public int p1awy { get; set; }
            public int p2awy { get; set; }
            public int p3awy { get; set; }
            public int p4awy { get; set; }
            public int ttawy { get; set; }
        }

        public class GameStatsItem
        {
            public  string Descr  { get; set; }
            public  int    Total  { get; set; }
            public  float  P1_Avg { get; set; }
            public  float  P1_Sqr { get; set; }
            public  int    P1_Awy { get; set; }
            public  float  P1_Per { get; set; }
            public  float  P2_Avg { get; set; }
            public  float  P2_Sqr { get; set; }
            public  int    P2_Awy { get; set; }
            public  float  P2_Per { get; set; }
            public  float  P3_Avg { get; set; }
            public  float  P3_Sqr { get; set; }
            public  int    P3_Awy { get; set; }
            public  float  P3_Per { get; set; }
            public  float  P4_Avg { get; set; }
            public  float  P4_Sqr { get; set; }
            public  int    P4_Awy { get; set; }
            public  float  P4_Per { get; set; }
            public  float  TT_Avg { get; set; }
            public  float  TT_Sqr { get; set; }
            public  int    TT_Awy { get; set; }
            public float  TT_Per { get; set; }
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
