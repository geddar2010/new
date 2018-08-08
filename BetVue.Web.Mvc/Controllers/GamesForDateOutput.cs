using System;
using System.Collections.Generic;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class GamesForDateOutput
    {
        public DateTime Date { get; set; }

        public IEnumerable<Game> Games { get; set; }
    }
}