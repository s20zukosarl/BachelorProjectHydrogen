using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace BachelorProjectHydrogen
{

    public static class ClientSocket
    {
        static Socket server;
        public static void ConnectToServerSocket()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                
            }
            catch (SocketException e)
            {
                MessageBox.Show(Convert.ToString(e));
            }
            Thread listen = new Thread(Receive);
            listen.IsBackground = true; ;
            listen.Start();
           

        }
        public static void Receive()
        {

            byte[] datarec = new byte[1024];
            try
            {
                while (true)
                {
                    string StringData;
                    int rec = server.Receive(datarec);
                    StringData = Encoding.ASCII.GetString(datarec, 0, rec);
                    Console.WriteLine(StringData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SendDataToSocketServer(string s)
        {
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(s);
            server.Send(data, data.Length, SocketFlags.None);
        }


    }
}
