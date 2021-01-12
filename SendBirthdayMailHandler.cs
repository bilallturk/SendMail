using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail
{
    public class SendBirthdayMailHandler
    {
        static string SpecialDaysDirectoryPath = Environment.CurrentDirectory + "/birthday.txt";
        static string LogPath = Environment.CurrentDirectory + "/logs.txt";
        public static void SendBirthdayMail()
        {
            var employee = new DtoEmployee();
            try
            {
                var nowDate = DateTime.Now;
                var lines = File.ReadAllLines(SpecialDaysDirectoryPath);
                string[] fields = null;
                foreach (string line in lines)
                {
                    fields = line.Split(',');
                    employee = new DtoEmployee()
                    {
                        NameSurname = fields[0],
                        Email = fields[3],
                        Birthday = Convert.ToDateTime(fields[1], new CultureInfo("tr-TR")),
                        JobStartDate = Convert.ToDateTime(fields[2], new CultureInfo("tr-TR"))
                    };

                    if (DateControl(employee.Birthday))
                    {
                        SendMailTemplate(employee, 0);
                    }

                    if (DateControl(employee.JobStartDate))
                    {
                        SendMailTemplate(employee, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Hata", ex.Message, EventLogEntryType.Error, 1000);
            }
        }

        private static bool DateControl(DateTime dateValue)
        {
            if (DateTime.Now.Date.Day == dateValue.Day && DateTime.Now.Date.Month == dateValue.Month)
            {
                return true;
            }
            return false;
        }

        private static void SendMailTemplate(DtoEmployee employee, int type)
        {
            try
            {
                var email = new MailSendRequest();
                var subject = String.Empty;
                var body = String.Empty;
                switch (type)
                {
                    case 0: // birthday
                        subject = $"Doğum Gününüz Kutlu Olsun";
                        body = "<html><body>";
                        body += $"Merhaba {employee.NameSurname}, </br></br>";
                        body += "Mobil VendeTTa takımı olarak doğum gününüzü kutlarız :) </br></br>";
                        body += "<img src =https://mobileupdate.thyteknik.com.tr/updatefiles/prod/images/dogumgunu.png>";
                        body += "</body></html>";
                        break;
                    case 1: // job start date
                        subject = $"İşe Başlangıç Yıldönümünüz Kutlu Olsun ";
                        body = "<html><body>";
                        body += $"Merhaba {employee.NameSurname}, </br></br>";
                        body += "Mobil VendeTTa takımı olarak işe başlangıç yıldönümünüzü kutlarız :) </br></br>";
                        body += "<img src =https://mobileupdate.thyteknik.com.tr/updatefiles/prod/images/dogumgunu.png>";
                        body += "</body></html>";
                        break;
                }

                email.To = employee.Email;
                // email.Cc = Worker._configValues.EmailCC;
                // email.Bcc = Worker._configValues.EmailBCC;
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;

                var emails = new List<MailSendRequest>();
                emails.Add(email);
                var sendmail = MailSender.SendMail(emails);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mail gonderilirken hata : " + ex.Message);
            }
        }
    }
}
