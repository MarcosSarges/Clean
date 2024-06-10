using Limp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Clean
{
    public partial class Service1 : ServiceBase
    {
        private Timer Timer = new Timer();
        App app = null;

        public Service1()
        {
            InitializeComponent();
            this.ServiceName = "WindowsService.NET";
        }

        protected override void OnStart(string[] args)
        {
            WriteLog("Service has been started");
            Timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            app = new App();
            Timer.Interval = app.config.Intervalo * 1000;
            Timer.Enabled = true;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            App.Run(app.config);
        }

        protected override void OnStop()
        {
            Timer.Stop();
            WriteLog("Service has been stopped.");
        }

        public void WriteLog(string logMessage, bool addTimeStamp = true)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var filePath = $"{path}\\{DateTime.Now.ToString("dd_MM_yyyy", CultureInfo.CurrentCulture)}_{2}.txt";

            if (addTimeStamp) logMessage = $"[{DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentCulture)}] - {logMessage}\r\n";

            File.AppendAllText(filePath, logMessage);
        }
    }
}
