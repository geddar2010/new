using System;

namespace BetVue.Web.Mvc.Controllers
{
    [Serializable]
    public class League
    {
        public int    Id    { get; set; }
        public string Title { get; set; }
    }
}