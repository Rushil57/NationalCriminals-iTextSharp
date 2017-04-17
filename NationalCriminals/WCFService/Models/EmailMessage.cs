using System;
using System.Collections.Generic;

namespace WCFService.Models
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsAttachment { get; set; }
        public List<EmailAttachment> Attachments { get; set; }
    }

    public class EmailAttachment
    {
        private string _name;
        public string Name
        {
            get { return _name ?? Path.Substring(Path.LastIndexOf("/", Path.Length - 1, StringComparison.Ordinal) + 1); }
            set { _name = value; }
        }

        public string Path { get; set; }
    }
}
