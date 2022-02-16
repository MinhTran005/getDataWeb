using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace getDataWeb
{
    public partial class Service1 : ServiceBase
    {
        Timer timerGetData = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteLog("Service start at " + DateTime.Now);
            timerGetData.Elapsed += new ElapsedEventHandler(TimeInterval);
            timerGetData.Interval = 600000;
            timerGetData.Enabled = true;
        }
        public void TimeInterval(object source, ElapsedEventArgs e)
        {
            WriteLog("Service is recall at" + DateTime.Now);
           
        }
        protected override void OnStop()
        {

        }
        private void WriteLog(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string file = path + DateTime.Now.ToString("dd-mm-yyyy") + ".txt";
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using(StreamWriter sw = File.AppendText(file))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
