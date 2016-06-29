using System;
using System.Text;
using System.Threading.Tasks;
using Alfredo.Resource;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Alfredo.Extensions;
using Alfredo.Domain;
using System.Linq;
using Alfredo.Service;



namespace Alfredo.Dialog
{
    [Serializable]
    [LuisModel(Constants.AppId, Constants.SubscriptionId)]
    public class RestaurantDialog : LuisDialog<Cafe> // TODO Replace with actual object later
    {
        [LuisIntent("find_restaurant")]
        public async Task FindRestaurant(IDialogContext context, LuisResult result)
        {
        }

        [LuisIntent("FindOneMenu")]
        public async Task FindOneMenu(IDialogContext context, LuisResult result)
        {

            var menu = result.Get(LuisType.Menu);
            var cafe = result.Get(LuisType.Cafe);

            context.ConversationData.SetValue(LuisType.Menu, menu);
            context.ConversationData.SetValue(LuisType.Cafe, cafe);

            var menudict = CafeService.GetRestaurant(DateTime.Now, "Café 9");
            var menulist = menudict.Menu[Day.Wednesday];

            var s = string.Join(", ", menulist.Select(m => m.Name));
            
            await context.PostAsync($"{s}");

            context.Done(0);
        }
    }
}