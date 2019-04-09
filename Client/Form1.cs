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


namespace Client
{
    public partial class Form1 : Form
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);        
        Thread thread;
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

        private void ReceiveText()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    socket.Receive(buffer);
                    tru = true;
                    messag = Encoding.UTF8.GetString(buffer);
                }
            }
            catch
            {
                MessageBox.Show("Пользователь отключился");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(ReceiveText);
        }

        private void ChangeServerName()
        {
            Form2 ServerName = new Form2();
            ServerName.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeServerName();
                socket.Connect(ConnectionServer.servername, 11000);//94.251.48.12
                byte[] buffer = Encoding.UTF8.GetBytes("Соединение установлено!");
                socket.Send(buffer);
                thread.Start();
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
                string message = richTextBox2.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                socket.Send(buffer);
                Console.ReadLine();
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
            thread.Abort();
            Application.Exit();
        }
    }
}
