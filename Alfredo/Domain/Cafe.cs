using System.Collections.Generic;

namespace Alfredo.Domain
{
    public class Cafe
    {
        public Dictionary<Day, List<Food>> Menu { get; set; }
    }
}