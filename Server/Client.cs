using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
  //убрать лишние usingи
namespace Server
{
    //такой класс необходимо наследовать от интерфейса IDisposable  и реализовывать метод Dispose в котором закрывать все потоки и сокеты
    public class Client
    {
        //такие поля как Socket и Thread лучше делать readobly так как они инициализируются и меняют значения только в конструкторе
        private string _userName;
        private Socket _handler;
        private Thread _userThread;
        public Client(Socket socket)
        {
            _handler = socket;
              //не используйте Thread для многотопочности, лучше используйте Task
            _userThread = new Thread(listner);
            _userThread.IsBackground = true;
            _userThread.Start();
        }
        //используйте сокращённый синтаксис свойств public string UserName { get; }
        public string UserName
        {
            get { return _userName; }
        }
        //методы всегда лучше называть с большой буквы
        private void listner()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRec = _handler.Receive(buffer);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                    handleCommand(data);
                }
                //пустой блок catch не очень хорошо
                catch
                {
                    Server.EndClient(this); return;
                }
            }
        }
        //почему метод public?
        public void End()
        {
            try
            {
                _handler.Close();
                try
                {
                    _userThread.Abort();
                }
                catch { }
            }
            catch
            {

            }
        }
        //лучше метод назвать HandleCommand
        private void handleCommand(string data)
        {
            if (data.Contains("#setname"))
            {
                _userName = data.Split('&')[1];
                UpdateChat();
                return;
            }
            if (data.Contains("#newmsg"))
            {
                string message = data.Split('&')[1];
                MessengerController.AddMessage(_userName, message);
                return;
            }
        }
        public void UpdateChat()
        {
            Send(MessengerController.GetChat());
        }
        //архитектура приложения на статических методах - очень плохо
        public void Send(string command)
        {
            try
            {
                int bytesSent = _handler.Send(Encoding.UTF8.GetBytes(command));
            }
            catch
            {
                Server.EndClient(this);
            }
        }
    }
}
