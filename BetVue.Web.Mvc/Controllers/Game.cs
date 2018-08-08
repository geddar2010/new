using System;
using System.Collections.Generic;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class Game
    {
        public Game(GameListItem item)
        {
            Id         = item.Id;
            DateStart  = item.Date;
            DateUpdate = item.UpdD;
            Country    = new Country {Id = item.CntI, Title = item.CntN};
            League     = new League {Id  = item.LeaI, Title = item.LeaN};
            Season     = new Season {Id  = item.SeaI, Title = item.SeaN};
            Stage      = new Stage {Id   = item.StgI, Title = item.StgN};
            Status     = item.Stat;
            TeamHome   = new Team {Id = item.THId, Title = item.THNm};
            TeamAway   = new Team {Id = item.TAId, Title = item.TANm};
            Periods.Add(new Score {Away = item.Sc1A, Home = item.Sc1H, Id = 1, Name = "P1"});
            Periods.Add(new Score {Away = item.Sc2A, Home = item.Sc2H, Id = 2, Name = "P2"});
            Periods.Add(new Score {Away = item.Sc3A, Home = item.Sc3H, Id = 3, Name = "P3"});
            Periods.Add(new Score {Away = item.Sc4A, Home = item.Sc4H, Id = 4, Name = "P4"});
            Periods.Add(new Score {Away = item.ScOA, Home = item.ScOH, Id = 5, Name = "OT"});
            var total = new Score {Away = item.ScTA, Home = item.ScTH, Id = 6, Name = "TT"};
            Periods.Add(total);
            Total = total;
        }

        public int      Id         { get; set; }
        public DateTime DateStart  { get; set; }
        public DateTime DateUpdate { get; set; }

        public Country Country { get; set; }

        public League League { get; set; }

        public Season Season { get; set; }

        public Stage Stage { get; set; }

        public Team TeamHome { get; set; }

        public Team TeamAway { get; set; }

        public string Status { get; set; }

        public List<Score> Periods { get; set; } = new List<Score>();

        public Score Total { get; set; }
    }
}
