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

namespace Client
{
    public partial class Form1 : Form
    {
        //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        TcpClient client;
        bool tru = false;
        string messag;

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
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tru)
            {
                richTextBox1.Text += "\nСобеседник: " + messag;
                tru = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ChangeServerName()
        {
            Form2 ServerName = new Form2();
            ServerName.ShowDialog();

        }
        StreamReader reader;
        StreamWriter writer;
        private void SendMessage(string message, StreamWriter writer)
        {
            writer.WriteLine(message);
            writer.Flush();
        }
        char[] buffer = new char[1024];
        private void Receive()
        {
            //int result = reader.Read(buffer, 0, 1024);
            messag = reader.ReadLine();
            //int resutl = reader1.Read(buffer1, 0, 1024);
            //messag = new string(buffer);
            buffer = new char[1024];
            //messag = new string(buffer1);
            tru = true;
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
            try
            {
                ChangeServerName();
                client = new TcpClient();
                client.Connect(ConnectionServer.servername, 11000);//94.251.48.12
                if (client.Connected)
                {
                    reader = new StreamReader(client.GetStream());
                    writer = new StreamWriter(client.GetStream());
                }
                SendMessage("Соединение установлено!", writer);
                ReceiveText();
                timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Упс, не удалось установить соединение");
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                //string message = richTextBox2.Text;
                //test for git
                string message = richTextBox2.Text;
                SendMessage(message, writer);
                richTextBox1.Text += "\nВы: " + richTextBox2.Text;
                richTextBox2.Text = "";
            }
            catch
            {
                MessageBox.Show("Упс, отправка неудалась, возможно пользователь не в сети");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}