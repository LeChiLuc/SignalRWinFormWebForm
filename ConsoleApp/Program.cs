using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:51070/signalr/hubs");
            var myHub = connection.CreateHubProxy("MyHub");         

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");

                    myHub.On<string,string,string>("pushMessage", (strMessage,name, price) => {
                        Console.WriteLine(strMessage + ": " + name + ": " + price);
                    });
                }

            }).Wait();

            Console.WriteLine("MaCK là gì:");
            string maCK = Console.ReadLine();
            Console.WriteLine("TênCT là gì:");
            string tenCT = Console.ReadLine();
            Console.WriteLine("Giá bao nhiêu:");
            string gia = Console.ReadLine();
            myHub.Invoke("broadcastMessage", maCK, tenCT, gia);
            Console.Read();
            connection.Stop();
        }
    }
}
