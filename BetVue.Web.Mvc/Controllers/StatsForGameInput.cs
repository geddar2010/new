namespace BetVue.Web.Mvc.Controllers
{
    public class StatsForGameInput
    {
        public int   gameId { get; set; }
        public bool? p1     { get; set; }
        public bool? p2     { get; set; }
        public bool? p3     { get; set; }
        public bool? p4     { get; set; }
        public bool? side   { get; set; }
        public int?  uniq   { get; set; } = null;
        public int?  p1awy  { get; set; } = null;
        public int?  p2awy  { get; set; } = null;
        public int?  p3awy  { get; set; } = null;
        public int?  p4awy  { get; set; } = null;
        public int?  ttawy  { get; set; } = null;
    }
}