
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Threading.Tasks;
using Topshelf;


namespace ConsoleApp22
{
    public class Program
    {
        private static void Main()
        {
            var exitCode = HostFactory.Run(x =>
                {
                    x.Service<ServiceGetdata>(s =>
                   {
                       s.ConstructUsing(servicegetdata => new ServiceGetdata());
                       s.WhenStarted(servicegetdata => servicegetdata.Start());                     
                       s.WhenStopped(servicegetdata => servicegetdata.Stop());
                   });
                    x.RunAsLocalSystem();
                    x.SetServiceName("DataWebService");
                    x.SetDisplayName("DataWeb Service");
                    x.SetDescription("Lay du lieu tu web");

                });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
           
        }

        
    }
}

