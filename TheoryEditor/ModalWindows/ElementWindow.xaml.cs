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

using Protsenko.TheoryEditor.VisualElements;
using Protsenko.TheoryEditor.Core.Types;
using Protsenko.TheoryEditor.Core.Languages;
using Protsenko.TheoryEditor.Core.Interfaces;

namespace Protsenko.TheoryEditor.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для CreateElementWindow.xaml
    /// </summary>
    public partial class ElementWindow : Window, IMultiLanguage
    {
        public ElementWindow(Core.Session session)
        {
            InitializeComponent();
            CreateElementPanel panel = new CreateElementPanel(session);
            panel.Accepted += new Protsenko.TheoryEditor.Core.Events.Accepted(panel_Accepted);
            this.Name = "createElement";
            LanguageResource_Updated("");
            this.Content = panel;
            LanguageResource.Updated +=new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            this.ShowDialog();
        }

        public ElementWindow(ITheoryElement element, List<ITheoryElement> basis, Core.Session session, Core.TheoryTree tree)
        {
            InitializeComponent();
            CreateElementPanel panel = new CreateElementPanel(element, basis, session, tree);
            panel.Accepted += new Protsenko.TheoryEditor.Core.Events.Accepted(panel_Accepted);
            this.Name = "createElement";
            LanguageResource_Updated("");
            this.Content = panel;
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            this.ShowDialog();
        }

        public ElementWindow(HelpPage page)
        {
            page.Accepted +=new Protsenko.TheoryEditor.Core.Events.Accepted(panel_Accepted);
            this.Name = "help";
            LanguageResource_Updated("");
            this.Content = page;
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            this.ShowDialog();
        }

        public ElementWindow(OptionsControl page)
        {
            page.Accepted += new Protsenko.TheoryEditor.Core.Events.Accepted(panel_Accepted);
            this.Name = "options";
            LanguageResource_Updated("");
            this.Content = page;
            LanguageResource.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            this.Width = 200; this.Height = 60;
            this.ShowDialog();
        }

        void panel_Accepted()
        {
            this.Close();
        }

        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            this.Title = LanguageResource.currentDictionary[this.Name];
        }

        #endregion
    }
}
