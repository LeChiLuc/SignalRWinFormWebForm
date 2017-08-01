using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebServer.SignalR
{
    public class MyHub : Hub
    {
        public void broadcastMessage(string strMessage,string name,string price)
        {
            Clients.All.pushMessage(strMessage, name,price);
        }
    }
}