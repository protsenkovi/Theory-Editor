using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Protsenko.TheoryEditor.Core.Debug
{
    public class ExceptionHandler
    {
        public static readonly ExceptionHandler exceptionHandler;
        Stack<Exception> exceptionslog = new Stack<Exception>();

        static ExceptionHandler()
        {
            exceptionHandler = new ExceptionHandler();
        }

        public void Add(Exception e)
        {
            exceptionslog.Push(e);
        }

        public void Clear()
        {
            Log.log.Add(CopyList<Exception>(exceptionslog));
            exceptionslog.Clear();
        }

        public void ShowInMessageBox()
        {
            if (exceptionslog.Count != 0)
                MessageBox.Show(FormatExcLog(), Languages.LanguageResource.currentDictionary["warning"]);
        }

        private string FormatExcLog()
        {
            StringBuilder formattedlog = new StringBuilder();
            foreach (Exception e in exceptionslog)
            {
                formattedlog.AppendLine(e.Message + "\n\n"); // + "\nStack trace:\n" + e.StackTrace + "\n");
            }
            return formattedlog.ToString();
        }

        private List<T> CopyList<T>(Stack<T> listToCopy)
        {
            List<T> list = new List<T>();
            foreach (T something in listToCopy)
                list.Add(something);
            return list;
        }
    }
}
