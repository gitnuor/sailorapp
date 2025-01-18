using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Dapper;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace MailSendService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private DateTime start_time;
        private DateTime end_time;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Set up the timer to trigger every minute (60000 milliseconds)
            timer = new Timer(60000);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            start_time = DateTime.Now;
            // Log that the service has started
            WriteToFile("Service started at " + DateTime.Now);
        }

        protected override void OnStop()
        {
            // Stop the timer and log that the service has stopped
            timer.Stop();
            end_time = DateTime.Now;

            tran_mail_notification_service_DTO obj = new tran_mail_notification_service_DTO();
            obj.start_time = start_time;
            obj.end_time = end_time;
            clsTask.InsertServiceLog(obj);

            WriteToFile("Service stopped at " + DateTime.Now);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Write a message to the file every time the timer elapses
            WriteToFile("Timer ticked at " + DateTime.Now);

            clsTask.ExecuteTask();
        }

        private void WriteToFile(string message)
        {
            string path = ConfigurationManager.AppSettings["LogFile"];

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
            }
        }

     
      
    }
}
