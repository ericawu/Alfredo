using System;
using System.Threading.Tasks;
using Alfredo.Resource;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Alfredo
{
    [Serializable]
    [LuisModel(Constants.AppId, Constants.SubscriptionId)]
    public class RestaurantDialog : LuisDialog<int> // TODO Replace with actual object later
    {
        [LuisIntent("find_restaurant")]
        public async Task FindRestaurant(IDialogContext context, LuisResult result)
        {
        }
    }
}