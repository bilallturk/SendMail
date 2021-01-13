using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Quartz;
using Topshelf.Quartz;

namespace SendMail
{
    public partial class SendMail : ServiceBase
    {
        public SendMail()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<SendBirthdayMailService>(s =>
                {
                    s.ConstructUsing(name => new SendBirthdayMailService());
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());


                    s.ScheduleQuartzJob<SendBirthdayMailService>(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<SendBirthdayMailJob>().Build())
                        .AddTrigger(() =>
                            TriggerBuilder.Create()
                                 .WithDailyTimeIntervalSchedule(a =>
                                      a.WithIntervalInHours(24)
                                       .OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(07, 36)))
                                .Build())
                        );
                });

                c.SetServiceName("test.SendMailBirthdayAndStartJob.Service");
                c.SetDisplayName("test.SendMailBirthdayAndStartJob.Service");
                c.SetDescription("This service send mail for Birthday and Start Job ");
                c.RunAsLocalSystem();
            });
        }

        protected override void OnStop()
        {
        }
    }
}
