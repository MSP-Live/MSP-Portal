using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MSP_Portal.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        static MessagesController()
        {
            _processors = new Dictionary<string, IMessageProcessor>();
            _processors.Add("user", new MSP_Portal.MessageProcessors.UsersProcessor());
        }

        [HttpPost]
        public string Post([FromBody]string message)
        {
            try
            {
                int id = 0; // TODO
                string[] part = message.Split(' ');
                string[] header = part[0].Split('.');
                string type = header[0].ToLower();
                string operation = header[1].ToLower();
                string[] args = new string[part.Length - 1];
                Array.Copy(part, 1, args, 0, args.Length);
                return _processors[type].Process(new Message(id, type, operation, args));
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return "ERROR: " + e.Message;
            }
        }
        public interface IMessageProcessor
        {
            string Process(Message message);
        }

        public delegate string MessageProcess(int id, string[] args);

        public abstract class MessageProcessor : MSP_Portal.Controllers.MessagesController.IMessageProcessor
        {
            public Dictionary<string, MessageProcess> Processors { get; set; }
            public string Process(MSP_Portal.Controllers.MessagesController.Message message)
            {
                if (Processors.ContainsKey(message.Operation))
                {
                    return Processors[message.Operation]?.Invoke(message.Id, message.Args);
                }
                else
                {
                    throw new NotSupportedException("Not supported operation.");
                }
            }
        }

        public class Message
        {
            public int Id { get; private set; }
            public string Type { get; private set; }
            public string Operation { get; private set; }
            public string[] Args { get; private set; }
            public Message(int id, string type, string operation, string[] args)
            {
                Id = id;
                Type = type;
                Operation = operation;
                Args = args;
            }
        }

        private static Dictionary<string, IMessageProcessor> _processors { get; }
    }
}
