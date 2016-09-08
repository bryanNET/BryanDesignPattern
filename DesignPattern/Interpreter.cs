using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class Context
    {
        private string msg;
        public Context(string msg)
        {
            this.msg = msg;
        }
        public string GetMsg()
        {
            return this.msg;
        }
    }
    public interface Interpreter
    {
        string Interprete(Context context);
    }
    public class UpperInterpreter : Interpreter
    {
        public string Interprete(Context context)
        {
            string msg = context.GetMsg();
            return msg.ToUpperInvariant();
        }
    }
    public class LowerInterpreter : Interpreter
    {
        public string Interprete(Context context)
        {
            string msg = context.GetMsg();
            return msg.ToLowerInvariant();
        }
    }
}
