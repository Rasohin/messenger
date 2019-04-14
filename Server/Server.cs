using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    public static class Server
    {
        //публичное поле - очень плохо, когда поле оно всегда private
        public static List<Client> Clients = new List<Client>();
        //статические поля лучше никогда не использовать
        public static string newClientConn;
        public static string newClientDisConn;

        public static void NewClient(Socket handle)
        {
            try
            {
                Client newClient = new Client(handle);
                Clients.Add(newClient);
                newClientConn = "New client connected: " + handle.RemoteEndPoint;
            }
            //пустой catch очень плохо
            catch
            {

            }
        }

        public static void EndClient(Client client)
        {
            try
            {
                client.End();
                Clients.Remove(client);
                newClientDisConn = "User " + client.UserName + " has been disconnected.";
            }
            catch
            {

            }
        }
        public static void UpdateAllChats()
        {
            try
            {
                int countUsers = Clients.Count;
                for (int i = 0; i < countUsers; i++)
                {
                    Clients[i].UpdateChat();
                }
            }
            //пустой catch очень плохо
            catch
            {

            }
        }
    }
}
