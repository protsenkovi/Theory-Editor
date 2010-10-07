using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Protsenko.TheoryEditor.VisualElements;
using Protsenko.TheoryEditor.Core.Debug;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core
{
    public class TheoryEditor
    {
        private static Core.Session session = null;

        public static Core.Session CurrentSession
        {
            get
            {
                if (session != null)
                    return session;
                else
                    throw new Exception(LanguageResource.currentDictionary["errorNotInitializedSession"]);
            }
            set
            {
                session = value;
            }
        }

        public static event EventHandler SessionAdded;
        public static event EventHandler SessionDeleted;
        public static event EventHandler SessionSwitched;

        static TheoryEditor()
        {
            SessionAdded += new EventHandler(TheoryEditor_SessionAdded);
            SessionDeleted += new EventHandler(TheoryEditor_SessionDeleted);
            SessionSwitched += new EventHandler(TheoryEditor_SessionSwitched);
        }

        static void TheoryEditor_SessionSwitched(object sender, EventArgs e) {}
        static void TheoryEditor_SessionDeleted(object sender, EventArgs e) {}
        static void TheoryEditor_SessionAdded(object sender, EventArgs e) {}

        public static void NewSession(Panel parent)
        {
            Operation operation = new Operation("New session");
            OperationsHandler.OperationStarts(operation);
            try
            {
                if (session == null)
                    session = CreateSession(parent);
                else
                {
                    MessageBoxResult result = MessageBox.Show(LanguageResource.currentDictionary["requestSaveSessionAndCreateNew"],
                                                              LanguageResource.currentDictionary["message"], MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        session.Close();
                        session = CreateSession(parent);
                    }
                }
                SessionAdded(session, new EventArgs());
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void CloseSession()
        {
            Operation operation = new Operation("Close session");
            OperationsHandler.OperationStarts(operation);
            try
            {
                if (session != null)
                {
                    operation.Name += " " + session.Name;
                    MessageBoxResult result = MessageBox.Show(LanguageResource.currentDictionary["requestSaveSession"], "?", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        session.SaveData();
                    }
                    if (result != MessageBoxResult.Cancel)
                    {
                        session.Close();
                        session = null;
                        SessionDeleted(session, new EventArgs());
                    }
                }
                else
                    throw new Exception(LanguageResource.currentDictionary["errorNotInitializedSession"]);
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void AddElement()
        {
            Operation operation = new Operation("Add element");
            OperationsHandler.OperationStarts(operation);

            try
            {
                if (session != null)
                {
                    new ModalWindows.ElementWindow(session);
                }
                else
                    throw new Exception(LanguageResource.currentDictionary["errorNotInitializedSession"]);
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void SaveSession()
        {
            Operation operation = new Operation("Save session");
            OperationsHandler.OperationStarts(operation);

            try
            {
                if (session != null)
                {
                    session.SaveData();
                }
                else
                    throw new Exception(LanguageResource.currentDictionary["errorNotInitializedSession"]);
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void LoadSession(Panel parent)
        {
            Operation operation = new Operation("Load session");
            OperationsHandler.OperationStarts(operation);

            try
            {
                if (session != null)
                    session.LoadData();
                else
                {
                    session = new Protsenko.TheoryEditor.Core.Session(parent, "Default", Protsenko.TheoryEditor.Core.Session.ConstructorMods.Load);
                }
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void SwitchToBasicMode()
        {
            Operation operation = new Operation("BasicMod_Click");
            OperationsHandler.OperationStarts(operation);

            try
            {
                if (session != null)
                {
                    session.ChangeMode(Protsenko.TheoryEditor.Core.Session.Mods.Basic);
                }
                else
                    throw new Exception(LanguageResource.currentDictionary["errorNotInitializedSession"]);
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void SwitchToQuickMode()
        {
            Operation operation = new Operation("QuickRedactorMod_Click");
            OperationsHandler.OperationStarts(operation);

            try
            {
                if (session != null)
                {
                    session.ChangeMode(Protsenko.TheoryEditor.Core.Session.Mods.QuickRedactor);
                }
                else
                    throw new Exception(LanguageResource.currentDictionary["errorNotInitializedSession"]);  
            }
            catch (Exception exc)
            {
                Core.Debug.ExceptionHandler.exceptionHandler.Add(exc);
            }
            Core.Debug.OperationsHandler.OperationComlete(operation);
        }

        public static void Help()
        {
            new ModalWindows.ElementWindow(new HelpPage(new Uri("http://www.google.com")));
        }

        public static void Options()
        {
            new ModalWindows.ElementWindow(new VisualElements.OptionsControl());
        }

        private static Core.Session CreateSession(Panel parent)
        {
            Core.Session session = null;
            CreateSessionBox box = CreateSessionBox.Show();
            if (box.notcanceled)
            {
                session = new Core.Session(parent, box.Name, Protsenko.TheoryEditor.Core.Session.ConstructorMods.New);
            }
            else
            {
                throw new Exception(LanguageResource.currentDictionary["errorCreationAborted"]);
            }
            return session;
        }
    }
}
