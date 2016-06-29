using System;
using System.Threading.Tasks;
using Alfredo.Resource;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Alfredo.Extensions;
using Alfredo.Domain;



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

            await context.PostAsync($"You're looking for {cafe}?");
            context.Done(0);
        }
    }
}