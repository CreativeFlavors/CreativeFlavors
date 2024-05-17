using log4net;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace MRPCalculationService
{
    public partial class MRPCalculationService : ServiceBase
    {
        private Timer Schedular;

        private static readonly ILog log = LogManager.GetLogger("RollingFileAppender");

        private static bool isCalculateProcessRunning = false;

        public MRPCalculationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.ScheduleService();
        }

        private void ScheduleService()
        {
            try
            {
                Schedular = new Timer(new TimerCallback(SchedularCallback));

                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                //this.WriteToFile("Simple Service Mode: " + mode + " {0}");

                //Set the Default Time.
                DateTime scheduledTime = DateTime.MinValue;

                if (mode == "DAILY")
                {
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode.ToUpper() == "INTERVAL")
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                //this.WriteToFile("Simple Service scheduled to run after: " + schedule + " {0}");

                //Get the difference in Minutes between the Scheduled and Current Time.
                int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                //Change the Timer's Due Time.
                Schedular.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                log.Error("Error on Schedule Service:" + ex.Message);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("DamageRptToAmazonS3"))
                {
                    serviceController.Stop();
                }
            }
        }

        private void SchedularCallback(object e)
        {
            log.Info("Called Schedular Callback");

            if (!isCalculateProcessRunning)
            {
                isCalculateProcessRunning = true;
                CalculationService worker = new CalculationService();
                worker.CalculateMRP();
                isCalculateProcessRunning = false;
            }

            this.ScheduleService();
        }

        protected override void OnStop()
        {
            this.Schedular.Dispose();
        }
    }
}
