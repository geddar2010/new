using System;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class PeriodStatsItem
    {
        public float Perc { get; set; }
        public float Freq { get; set; }

        public float Sqrt { get; set; }
    }
}
