using System;
using System.Collections.Generic;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class PeriodRasklad
    {
        public int    Id      { get; set; }
        public string Name    { get; set; }
        public int? OddUser { get; set; }
        public bool? OddReal { get; set; }
        public int   OddAway { get; set; }

        public List<PeriodStatsItem> PeriodStats { get; set; } = new List<PeriodStatsItem>();
    }
}
