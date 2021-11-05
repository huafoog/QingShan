using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Test.DependencyInjection.Services
{
    public interface IMessage
    {
        void Send(string content);
        void Receive(string content);
    }

    public class EmailMessage : IMessage
    {
        public void Send(string content)
        {
            Console.WriteLine("Send Email:" + content);
        }   
        public void Receive(string content)
        {
            Console.WriteLine("Receive Email:" + content);
        }
    }
}
