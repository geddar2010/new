using System;

namespace BetVue.Web.Mvc.Controllers
{
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
}