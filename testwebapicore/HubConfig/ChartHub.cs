using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.HubConfig
{
    public class ChartHub: Hub
    {
        public static string ConnectionID;
        public static string tosaveit;
        //public async Task NewMessage(string name,string msg,string points)
        //{
        //    await Clients.All.SendAsync("MessageReceived", name,msg, points.ToString());
        //}
        public override Task OnConnectedAsync()
        {
             ConnectionID = Context.ConnectionId;
            ConnectionID= GetConnectionID();
            return base.OnConnectedAsync();
        }
        public static string GetConnectionID()
        {
             tosaveit = ConnectionID;
            return tosaveit;
          
        }
    }
}
