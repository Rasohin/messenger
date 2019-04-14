using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//убрать лишние usingи
namespace Client
{
    static class ConnectionServer
    {
        //такие вещи лучше называть с большой буквы и ещё лучше делать с помощью свойств доступных только на get
        public static string servername = "localhost";
        public static string newusername;
    }
}
