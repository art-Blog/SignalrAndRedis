using Microsoft.AspNet.SignalR.Client;

namespace SignalrAndRedis.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //輸入使用者稱謂
            System.Console.Write("Please input user id: ");
            var id = System.Console.ReadLine();

            //與SignalR Hub Server 連線
            var connection = new HubConnection("http://localhost:46620", $"id={id}");
            var chatHub = connection.CreateHubProxy("ChatHub");

            //---實作(定義) Client 端方法----------------------------------------------------------------
            chatHub.On<string>("Received", (message) => System.Console.WriteLine(message));

            connection.Start().Wait();

            var msg = string.Empty;
            while (msg != "end")
            {
                msg = System.Console.ReadLine();
                chatHub.Invoke("Send", "Console", msg);
            }
            System.Console.WriteLine(" end...");

            connection.Stop();
        }
    }
}