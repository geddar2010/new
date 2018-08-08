using System;
using System.Collections.Generic;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class GameRasklad
    {
        public int           PrevUniq      { get; set; }
        public int           CurrentPeriod { get; set; }
        public PeriodRasklad P1            { get; set; }
        public PeriodRasklad P2            { get; set; }
        public PeriodRasklad P3            { get; set; }
        public PeriodRasklad P4            { get; set; }
        public PeriodRasklad TT            { get; set; }

        public List<GameStatsRow> Statistics { get; set; } = new List<GameStatsRow>();
    }

    [Serializable]
    public class GameStatsRow
    {
        public string          Descr { get; set; }
        public int             Count { get; set; }
        public PeriodStatsItem P1    { get; set; }
        public PeriodStatsItem P2    { get; set; }
        public PeriodStatsItem P3    { get; set; }
        public PeriodStatsItem P4    { get; set; }
        public PeriodStatsItem TT    { get; set; }
    }
}
