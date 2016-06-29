using System.Collections.Generic;

namespace Alfredo.Domain
{
    public class Food
    {
        public string Name { get; set; }
        public string Cafe { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public Dictionary<string, string> Nutrition { get; set; }
    }
}