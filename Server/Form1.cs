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
namespace Server
{
    public partial class Form1 : Form
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket client, client1;
        Thread thread, thread1;
        bool tru = false;
       string messag, messag1;

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
            try
            {
                string message = richTextBox2.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                client.Send(buffer);
                client1.Send(buffer);
                Console.ReadLine();
                richTextBox1.Text += "\nВы: " + richTextBox2.Text;
                richTextBox2.Text = "";
            }
            catch
            {
                MessageBox.Show("Упс, отправка неудалась, возможно пользователь не в сети");
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
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    
                    client.Receive(buffer);
                    
                    tru = true;
                    
                    client1.Send(buffer);
                }
                catch
                {
                    MessageBox.Show("Пользователь отключился");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            thread.Abort();
            thread1.Abort();
            Application.Exit();
        }

        private void ReceiveText1()
        {
            while (true)
            {
                try
                {
                    byte[] buffer1 = new byte[1024];

                    client1.Receive(buffer1);

                    tru = true;

                    client.Send(buffer1);
                }
                catch
                {
                    MessageBox.Show("Пользователь отключился");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, 11000));
                socket.Listen(5);
                client = socket.Accept();
                client1 = socket.Accept();
                byte[] buffer = Encoding.UTF8.GetBytes("Соединение установлено!");
                client.Send(buffer);
                client1.Send(buffer);
                thread.Start();
                thread1.Start();
                timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Упс, пользователь отключился");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(ReceiveText);
            thread1 = new Thread(ReceiveText1);
        }


    }
}