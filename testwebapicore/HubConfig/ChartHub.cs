using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.HubConfig
{
    public class ChartHub: Hub
    {
        
        //public async Task NewMessage(string name,string msg,string points)
        //{
        //    await Clients.All.SendAsync("MessageReceived", name,msg, points.ToString());
        //}
        public override Task OnConnectedAsync()
        {
           // string ConnectionID = Context.ConnectionId;
            return base.OnConnectedAsync();
        }
        //public string GetConnectionID()
        //{
        //    string ConnectionID = Context.ConnectionId;
        //    return ConnectionID;
        //}
    }
}
