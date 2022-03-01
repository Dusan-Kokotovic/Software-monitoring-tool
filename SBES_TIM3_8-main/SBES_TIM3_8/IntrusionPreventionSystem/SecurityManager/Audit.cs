using Common.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IntrusionPrevensionSystem.SecurityManager
{
    public class Audit : IDisposable
    {

        private static EventLog customLog = null;
        const string SourceName = "SecurityManager.Audit";
        const string LogName = "";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
                customLog = new EventLog(LogName,
                    Environment.MachineName, SourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }
        

        public static void LogEvents(string fileName,string filePath,ErrorLevel fileCriticality,System.DateTime time)
        {
            if(customLog != null)
            {
                string message = String.Format(fileCriticality.ToString(),fileName,filePath,time);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException("Error while trying to write event to event log.");
            }

        }


        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}
