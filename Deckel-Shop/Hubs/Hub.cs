using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deckel_Shop.Hubs
{
    public class DeckelHub : Hub
    {
        public DeckelHub()
        {
        }
        public async Task OrderPlaced()
        {
            await Clients.Others.SendAsync("OrderHasBeenPlaced");
        }
    }
}
