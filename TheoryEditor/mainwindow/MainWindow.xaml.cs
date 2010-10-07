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

using Protsenko.TheoryEditor.VisualElements;
using Protsenko.TheoryEditor.Core.Debug;
using Protsenko.TheoryEditor.Core; // what a??
using Protsenko.TheoryEditor.Core.Languages;
using Protsenko.TheoryEditor.Core.Interfaces;

namespace Protsenko.TheoryEditor
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window, IMultiLanguage
    {
        public MainWindow()
        {
            InitializeComponent();  
            Core.TheoryEditor.SessionAdded += new EventHandler(TheoryEditor_SessionAdded);
            Core.TheoryEditor.SessionDeleted += new EventHandler(TheoryEditor_SessionDeleted);
            Core.TheoryEditor.SessionSwitched += new EventHandler(TheoryEditor_SessionSwitched);

            LanguageResource.Updated +=new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            LanguageResource_Updated("");
        }

        public void LanguageResource_Updated(object language)
        {
            _New.Header = LanguageResource.currentDictionary["new"];
            _Save.Header = LanguageResource.currentDictionary["save"];
            _Load.Header = LanguageResource.currentDictionary["load"];
            _Add.Header = LanguageResource.currentDictionary["add"];
            _Exit.Header = LanguageResource.currentDictionary["exit"];
            BasicMod.Header = LanguageResource.currentDictionary["modBasic"];
            QuickRedactorMod.Header = LanguageResource.currentDictionary["modQuick"];
            Mod.Header = LanguageResource.currentDictionary["mod"];
            Help.Header = LanguageResource.currentDictionary["help"];
            Options.Header = LanguageResource.currentDictionary["options"];
            Close.Header = LanguageResource.currentDictionary["close"];
            Title = LanguageResource.currentDictionary["theoryeditor"];
        }

        void TheoryEditor_SessionSwitched(object sender, EventArgs e)
        {
            this.Title = LanguageResource.currentDictionary["theoryeditor"] + " " + Core.TheoryEditor.CurrentSession.Name;
        }

        void TheoryEditor_SessionDeleted(object sender, EventArgs e)
        {
            Close.Visibility = Visibility.Collapsed;
            this.Title = LanguageResource.currentDictionary["theoryeditor"];
        }

        void TheoryEditor_SessionAdded(object sender, EventArgs e)
        {
            Close.Visibility = Visibility.Visible;
            this.Title = LanguageResource.currentDictionary["theoryeditor"] + " " + ((Session)sender).Name;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.NewSession(maindock);
        }

        void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.CloseSession();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.AddElement();
        }

        private void _Save_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.SaveSession();
        }

        private void _Load_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.LoadSession(maindock);
        }

        private void BasicMod_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.SwitchToBasicMode();
        }

        private void QuickRedactorMod_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.SwitchToQuickMode();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Core.TheoryEditor.Help();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Core.Debug.Log.log.Dump();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            new ModalWindows.ElementWindow(new OptionsControl());
        }

        #region Члены IMultiLanguage

        public void UpdateLanguge()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
