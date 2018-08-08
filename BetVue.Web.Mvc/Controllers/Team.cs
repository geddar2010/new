using System;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class Team
    {
        public int    Id    { get; set; }
        public string Title { get; set; }
    }
}