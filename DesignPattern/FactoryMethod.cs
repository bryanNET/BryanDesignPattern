using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
   
    public interface ILogger
    {
        void write(string log);
    }
    public class EventLogger : ILogger
    {
        public void write(string log)
        {
            Console.WriteLine("EventLog:" + log);
        }
    }
    public class FileLogger : ILogger
    {
        public void write(string log)
        {
            Console.WriteLine("FileLog:" + log);
        }
    }
    /// <summary>
    /// 工厂方法
    /// </summary>
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }
    public class EventLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new EventLogger();
        }
    }
    public class FileLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new FileLogger();
        }
    }
}
