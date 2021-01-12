using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail
{
    public class MailSendRequest
    {
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string From { get; set; } = "huseyin.ozcan@thy.com";
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
    public class Attachment
    {
        public string FileName { get; set; }
        public string Data { get; set; }
    }
}
