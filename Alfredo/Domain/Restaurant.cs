using System.Collections.Generic;

namespace Alfredo.Domain
{
    public class Restaurant
    {
        public Dictionary<Day, List<Food>> Menu { get; set; }
    }
}