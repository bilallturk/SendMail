using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail
{
    public class SendBirthdayMailService
    {
        public bool Start()
        {
            //using (EventLog eventLog = new EventLog("Application"))
            //{
            //    eventLog.Source = "Application";
            //    eventLog.WriteEntry("Service Started!", EventLogEntryType.Information);
            //}
            EventLog.WriteEntry(".NET Runtime", "Service Started!", EventLogEntryType.Information, 1000);
            return true;
        }
        public bool Stop()
        {
            //using (EventLog eventLog = new EventLog("Application"))
            //{
            //    eventLog.Source = "Application";
            //    eventLog.WriteEntry("Service Stopped!", EventLogEntryType.Information);
            //}
            EventLog.WriteEntry(".NET Runtime", "Service Stopped!", EventLogEntryType.Information, 1000);
            return true;
        }
    }
}
