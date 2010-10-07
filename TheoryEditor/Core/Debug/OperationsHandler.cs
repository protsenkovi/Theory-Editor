using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protsenko.TheoryEditor.Core.Debug
{
    public class OperationsHandler
    {
        static Stack<Operation> operationStack = new Stack<Operation>();

        public static void OperationComlete(Operation operation)
        {
            try
            {
                Core.Debug.ExceptionHandler.exceptionHandler.ShowInMessageBox();
                Core.Debug.ExceptionHandler.exceptionHandler.Clear();
            }
            catch (Exception e)
            {
                Core.Debug.Log.log.Add(e);
            }

            Core.Debug.Log.log.Add(DateTime.Now.ToShortDateString() + " - Operation complete: " + operation.Name);

            if (operationStack.Peek() != operation)
                Core.Debug.Log.log.Add("Something went wrong. Operation: " + operationStack.Peek().Name + " was not complete or was not registered.");

            operationStack.Pop();
        }

        public static void OperationStarts(Operation operation)
        {
            Core.Debug.Log.log.Add(DateTime.Now.ToShortDateString() + " - Operation starts: " + operation.Name);
            operationStack.Push(operation);
        }
    }
}
