using System;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class Stats
    {
        public Game Game { get; set; }

        public GameRasklad Rasklad { get; set; }
    }
}