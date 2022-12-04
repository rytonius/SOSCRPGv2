using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.EventArgs
{
    public class WorldMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }
        public WorldMessageEventArgs(string message)
        {
            Message = message;
        }

    }
}
