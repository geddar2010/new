using System.Collections.Generic;

namespace BetVue.Web.Mvc.Controllers
{
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
}