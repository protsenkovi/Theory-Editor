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
using System.Windows.Shapes;

using Protsenko.TheoryEditor.Core.Languages;
using Protsenko.TheoryEditor.Core.Interfaces;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для MessageListBox.xaml
    /// </summary>
    public partial class CreateSessionBox : Window, IMultiLanguage
    {
        public string Name { get { return Textbox.Text; } private set { Textbox.Text = value;  } }
        public bool notcanceled = false;

        protected CreateSessionBox()
        {
            InitializeComponent();
            Textbox.Text = "Default";
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            LanguageResource_Updated("");
        }

        public static CreateSessionBox Show()
        {
            CreateSessionBox box = new CreateSessionBox();
            box.ShowDialog();
            return box;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            notcanceled = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            Title = LanguageResource.currentDictionary["createSession"];
            Ok.Content = LanguageResource.currentDictionary["ok"];
            Cancel.Content = LanguageResource.currentDictionary["cancel"];
        }

        #endregion
    }
}
