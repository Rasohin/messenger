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
        TcpClient client, client1;        
        string messag=null, messag1=null;

        public Form1()
        {
            InitializeComponent();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string message = richTextBox2.Text;            
            SendMessage(message, writer);
            SendMessage(message, writer1);
            Console.ReadLine();
            richTextBox1.Text += "\nВы: " + richTextBox2.Text;
            richTextBox2.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        StreamReader reader, reader1;
        StreamWriter writer, writer1;
        private void SendMessage(string message, StreamWriter writer)
        {            
            //buffer = new char[1024];
            writer.WriteLine(message);
            writer.Flush();
            messag = null;
            messag1 = null;
        }
        char[] buffer = new char[1024];
        char[] buffer1 = new char[1024];
        private void Receive()
        {
            //int result = reader.Read(buffer, 0, 1024);
            //int resutl = reader1.Read(buffer1, 0, 1024);     
            try
            {
                messag = reader.ReadLine();
            }
            catch { }
            try
            {
                messag1 = reader1.ReadLine();
            }
            catch { }
            //messag1 = new string(buffer1);
            if (messag != null)
                SendMessage(messag, writer1);
            if (messag1 != null)
                SendMessage(messag1, writer);            
        }
        private async void ReceiveText()
        {
            while (true)
            {
                await Task.Run(() => Receive());
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 11000);
            listener.Start();
            client = listener.AcceptTcpClient();
            client1 = listener.AcceptTcpClient();
            if (client.Connected && client1.Connected)
            {
                reader = new StreamReader(client.GetStream());
                reader1 = new StreamReader(client1.GetStream());
                writer = new StreamWriter(client.GetStream());
                writer1 = new StreamWriter(client1.GetStream());
                client.GetStream().ReadTimeout = 50;
                client1.GetStream().ReadTimeout = 50;
            }
            SendMessage("Соединение установлено!", writer);
            SendMessage("Соединение установлено!", writer1);
            ReceiveText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}