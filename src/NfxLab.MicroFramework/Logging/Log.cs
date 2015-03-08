using System;
using Microsoft.SPOT;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace NfxLab.MicroFramework.Logging
{
    public class Log
    {
        LogFormatter formatter = new LogFormatter();

        public int Indent { get; set; }

        public IAppender[] Appenders { get; private set; }

        public Log(params IAppender[] appenders)
        {
            this.Appenders = appenders;
        }

        public virtual void AssemblyInfo()
        {
            Write(Assembly.GetExecutingAssembly());
        }


        public virtual void Title(string name)
        {
            Line();
            Write(name);
            Line();
        }

        public virtual void Line()
        {
            Write(new string('_', 20));
        }

        [Conditional("DEBUG")]
        public virtual void WriteDebug(params object[] data)
        {
            Write(data);
        }

        public virtual void Write(params object[] data)
        {
            string message = formatter.Format(Indent, data);

            foreach (IAppender appender in Appenders)
                appender.Write(message);
        }
    }
}
