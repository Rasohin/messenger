using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.IO.Compression;
namespace Server
{
    public partial class Form1 : Form
    {
        private const string _serverHost = "localhost";
        private const int _serverPort = 11000;
        private static Thread _serverThread;

        public Form1()
        {
            InitializeComponent();
              //не используйте Thread для работы с многопоточностью, лучше используйте Task
            _serverThread = new Thread(startServer);
            _serverThread.IsBackground = true;
            _serverThread.Start();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void startServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(_serverHost);
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _serverPort);
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEndPoint);
             //лучше не слушать хорошо известные порты то есть порты с номерами меньшими 16535
            socket.Listen(1000);
            richTextBox1.Text = "Server has been started on IP:" + ipEndPoint + "\n";
            
            while (true)
            {
                try
                {
                    Socket user = socket.Accept();
                    Server.NewClient(user);
                    richTextBox1.AppendText(Server.newClientConn + "\n");
                    //richTextBox1.AppendText(Server.newClientDisConn);
                }
                catch
                {

                }
            }

        }
        //абсолютно лишний метод
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}