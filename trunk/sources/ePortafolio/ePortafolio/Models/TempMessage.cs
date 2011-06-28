using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortafolio.Models
{
    public class TempMessage
    {
        public String Message { get; set; }
        public bool isHtml { get; set; }
        public MessageType MessageType { get; set; }

        public TempMessage()
        {
            Message = "";
            isHtml = false;
            MessageType = MessageType.Info;
        }
    }

    public enum MessageType { Error, Success, Info}
}