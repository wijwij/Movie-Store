using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MovieStore.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task DiscountNotification()
        {
            await Clients.All.SendAsync("discountNotification", "30% discount on new movies");
        }
    }
}