using System;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class Score
    {
        public int    Id   { get; set; }
        public string Name { get; set; }
        public int    Home { get; set; }
        public int    Away { get; set; }
    }
}