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
 //убрать лишние usinggи
namespace Client
{
    public partial class Form1 : Form
    {
        //делегаты лучше называть с большой буквы
        private delegate void printer(string data);
        private delegate void cleaner();
        //приватные поля лучше называть соотвественно приватным полям илои в этом случае использовать события и лучше ихз называть в другой грамматической структуре например
        //public event Printer Printed;
        printer Printer;
        cleaner Cleaner;
        private Socket _serverSocket;
        private Thread _clientThread;
        private string _serverHost = ConnectionServer.servername;
        private const int _serverPort = 11000;

        public Form1()
        {
            InitializeComponent();
                //объекты делегатов - зло
            Printer = new printer(print);
            Cleaner = new cleaner(clearChat);
            connect();
            _clientThread = new Thread(listner);
            _clientThread.IsBackground = true;
            _clientThread.Start();
        }
        //методы лучше называть с большой буквы
        private void listner()
        {
            while (_serverSocket.Connected)
            {
                byte[] buffer = new byte[8196];
                int bytesRec = _serverSocket.Receive(buffer);
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                if (data.Contains("#updatechat"))
                {
                    UpdateChat(data);
                    continue;
                }
            }
        }
        //методы надо называть с большой буквы
        private void connect()
        {
            try
            {
                _serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Connect(_serverHost, _serverPort);
            }
            catch { print("Сервер недоступен!"); }
        }
        private void clearChat()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Cleaner);
                return;
            }
            richTextBox1.Clear();
        }
        private void UpdateChat(string data)
        {
            //#updatechat&userName~data|username~data
            clearChat();
            string[] messages = data.Split('&')[1].Split('|');
            int countMessages = messages.Length;
            if (countMessages <= 0) return;
            for (int i = 0; i < countMessages; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(messages[i])) continue;
                    print(String.Format("[{0}]:{1}.", messages[i].Split('~')[0], messages[i].Split('~')[1]));
                }
                //пустой catch - плохо
                catch { continue; }
            }
        }
        //методы лучше называть с большой буквы
        private void send(string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int bytesSent = _serverSocket.Send(buffer);
            }
            catch { print("Связь с сервером прервалась..."); }
        }
        //методы лучше называть с большой буквы и очень плохо что название совпадает с полем класса
        private void print(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Printer, msg);
                return;
            }
            if (richTextBox1.Text.Length == 0)
                richTextBox1.AppendText(msg);
            else
                richTextBox1.AppendText(Environment.NewLine + msg);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
                richTextBox2.Font = fontDialog1.Font;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
                richTextBox2.ForeColor = colorDialog1.Color;
            }
        }
        
        //лишний метод
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ChangeServerName()
        {
            Form2 ServerName = new Form2();
            ServerName.ShowDialog();

        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeServerName();
            string Name = ConnectionServer.newusername;
            send("#setname&" + Name);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            sendMessage();
        }
        //методы лучше называть с большой буквы
        private void sendMessage()
        {
            try
            {
                string data = richTextBox2.Text;
                send("#newmsg&" + data);
                richTextBox2.Text = string.Empty;
            }
            catch { MessageBox.Show("Ошибка при отправке сообщения!"); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}