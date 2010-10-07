using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Protsenko.TheoryEditor.Core.Debug
{
    public class Log
    {
        public static readonly Log log;
        List<object> logList = new List<object>();
        public string fileExtension { get; set; }
        public event EventHandler logEvent;

        static Log()
        {
            log = new Log();
            log.fileExtension = ".log";
        }

        public void Add(object e)
        {
            logList.Add(e);
        }

        public void Clear()
        {
            logList.Clear();
        }

        public void ShowInMessageBox()
        {
            MessageBox.Show(FormatLog(), Languages.LanguageResource.currentDictionary["log"]);
        }

        public void Dump()
        {
            try
            {
                SaveLoad.SaveData(DateTime.Today.ToString(), "logs", fileExtension, FormatLog());
            }
            catch (Exception e)
            {
                ExceptionHandler.exceptionHandler.Add(e);
                ExceptionHandler.exceptionHandler.ShowInMessageBox();
                ExceptionHandler.exceptionHandler.Clear();
            }
        }

        private string FormatLog()
        {
            StringBuilder formattedlog = new StringBuilder();

            formattedlog.AppendLine("Log Entry : ");
            formattedlog.AppendLine(DateTime.Now.ToLongTimeString() + "\r\n");

            foreach (object line in logList)
            {
                if (line is IList<Exception>)
                {
                    int i = 1;
                    formattedlog.AppendLine("Exceptions:\r\n");
                    foreach (Exception ex in (IList<Exception>)line)
                    {
                        formattedlog.AppendLine(i + ". " + ex.ToString().Replace("\n","\n    ") + "\n");
                        i++;
                    }
                }
                else
                    formattedlog.AppendLine(line.ToString() + "\n");
                formattedlog.AppendLine("-------------------------------");
            }
            formattedlog.Append("\r\n\r\n");
            return formattedlog.ToString();
        }
    }
}
