using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

using Protsenko.TheoryEditor.Core;
using Protsenko.TheoryEditor.ModalWindows;
using Protsenko.TheoryEditor.Core.Languages;
using Protsenko.TheoryEditor.Core.Interfaces;
using Protsenko.TheoryEditor.Core.Debug;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для TheoryImporterPanel.xaml
    /// </summary>
    public partial class TheoryImporterPanel : UserControl, IMultiLanguage
    {
        private TheoryTree tree;
        private Session session;
        private WebBrowser browser;

        public TheoryImporterPanel(TheoryTree tree, Session session)
        {
            InitializeComponent();
            this.tree = tree;
            this.session = session;

            MenuItem item; 
            foreach(TheoryTypes type in TheoryTypesClass.GetTypes())
            {
                item = new MenuItem();
                item.Name = type.ToString();
                item.Click += new RoutedEventHandler(item_Click);
                cm.Items.Add(item);
            }

            browser = new WebBrowser();
            

            LanguageResource.Updated +=new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            LanguageResource_Updated("");
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;

            MessageListBox msg = MessageListBox.Show(tree.GetAllElements(), MessageListBox.Mods.Edit);

            session.AddTheoryElement(TheoryTypesClass.GetType((string)item.Name), 0, textBoxRedactor.SelectedText, "", "",(List<Core.Types.ITheoryElement>)msg.choosenElement);
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog filedialog = new Microsoft.Win32.OpenFileDialog();
            filedialog.InitialDirectory = SaveLoad.currentFoldierPath;
            filedialog.ShowDialog();
            textBoxPath.Text = filedialog.FileName;

            Operation operation = new Operation("Browse Click");
            OperationsHandler.OperationStarts(operation);
            try
            {
                if (filedialog.FileName != "")
                {
                    if (!(textBoxPath.Text.Contains(".pdf") || textBoxPath.Text.Contains(".doc") || textBoxPath.Text.Contains(".xls")))
                    {
                        Redactor.Content = textBoxRedactor;
                        textBoxRedactor.Text = SaveLoad.LoadData(textBoxPath.Text);
                    }
                    else
                    {
                        Redactor.Content = browser;
                        browser.Navigate(new Uri(textBoxPath.Text));
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionHandler.exceptionHandler.Add(exc);
            }
            OperationsHandler.OperationComlete(operation);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Operation operation = new Operation("Open Click");
            OperationsHandler.OperationStarts(operation);
            try
            {
                if (!(textBoxPath.Text.Contains(".pdf") || textBoxPath.Text.Contains(".doc") || textBoxPath.Text.Contains(".xls")))
                {
                    Redactor.Content = textBoxRedactor;
                    textBoxRedactor.Text = SaveLoad.LoadData(textBoxPath.Text);
                }
                else
                {
                    Redactor.Content = browser;
                    browser.Navigate(new Uri(textBoxPath.Text));
                }
            }
            catch (Exception exc)
            {
                ExceptionHandler.exceptionHandler.Add(exc);
            }
            OperationsHandler.OperationComlete(operation);
        }

        private void cm_Opened(object sender, RoutedEventArgs e)
        {

        }

        private void cm_Closed(object sender, RoutedEventArgs e)
        {

        }


        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            Browse.Content = LanguageResource.currentDictionary["browse"];
            Open.Content = LanguageResource.currentDictionary["open"];

            foreach (MenuItem item in cm.Items)
            {
                item.Header = LanguageResource.currentDictionary[item.Name];
            }  
        }

        #endregion
    }
}
