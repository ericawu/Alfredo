using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using Microsoft.Bot.Builder.Luis.Models;

namespace Alfredo.Extensions
{
    public static class LuisExtensions
    {
        public static string Get(this LuisResult result, string typeName) =>
             result.Entities
                 .FirstOrDefault(e => e.Type == typeName)?
                 .Entity;
    }
}