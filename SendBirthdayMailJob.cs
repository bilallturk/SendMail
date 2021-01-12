using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace SendMail
{
    public class SendBirthdayMailJob: IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            SendBirthdayMailHandler.SendBirthdayMail();
        }
    }
}
