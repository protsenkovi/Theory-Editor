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

using Protsenko.TheoryEditor.Core.Languages;
using Protsenko.TheoryEditor.Core.Interfaces;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для OptionsControl.xaml
    /// </summary>
    public partial class OptionsControl : UserControl, IMultiLanguage
    {
        public event Core.Events.Accepted Accepted;

        public OptionsControl()
        {
            InitializeComponent();

            Languages.ItemsSource = LanguageResource.GetAvailableLanguages();
            Accepted += new Protsenko.TheoryEditor.Core.Events.Accepted(OptionsControl_Accepted);
            LanguageResource.Updated +=new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
        }

        void OptionsControl_Accepted() { }

        private void Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageResource.SwitchLanguage((string)Languages.SelectedItem);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Accepted();
        }

        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            
        }

        #endregion
    }
}
