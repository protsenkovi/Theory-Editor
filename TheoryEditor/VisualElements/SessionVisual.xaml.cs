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

using Protsenko.TheoryEditor.Core.Types;
using Protsenko.TheoryEditor.Core;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для SessionVisual.xaml
    /// </summary>
    public partial class SessionVisual : UserControl
    {
        ElementSpace canvas;
        TheoryTree tree;
        Session session;
        Session.Mods mod;

        private TheoryImporterPanel theoryImporterPanel = null;

        public event Core.Events.Updated Updated;


        public SessionVisual(ElementSpace space, TheoryTree tree, Session session) 
        {
            mod = Session.Mods.Basic;

            InitializeComponent();
            canvas = space;
            this.tree = tree;
            this.session = session;

            space.ElementsFocused += new Protsenko.TheoryEditor.Core.Events.Focused(space_ElementFocused);
            Grid.SetColumn(canvas, 0); Grid.SetColumnSpan(canvas,2);

            maingrid.Children.Add(canvas);

            Updated += new Protsenko.TheoryEditor.Core.Events.Updated(SessionVisual_Updated);
        }

        void SessionVisual_Updated(object sender)
        {
            if (mod == Session.Mods.Basic)
            {
                LabelRightPanelContainer.Content = TheoryBox;
                BorderRightPanelContainer.BorderBrush = Brushes.White;
            }
            else if (mod == Session.Mods.QuickRedactor)
            {
                if (theoryImporterPanel == null)
                    theoryImporterPanel = new TheoryImporterPanel(tree, session);
                LabelRightPanelContainer.Content = theoryImporterPanel;
                BorderRightPanelContainer.BorderBrush = Brushes.IndianRed;
            }
        }

        void space_ElementFocused(List<ITheoryElement> elements)
        {
            if (mod == Session.Mods.Basic)
            {
                if (elements != null)
                {
                    TheoryBox.Text = "";
                    foreach (ITheoryElement element in elements)
                        TheoryBox.Text += InfromationPresenter.MakeFormattedText(element) + "\n";
                }
                else
                    TheoryBox.Text = "";
            }
        }

        public void ChangeMode(Session.Mods mod)
        {
            this.mod = mod;
            Updated(this);
        }

        public ElementSpace GetElementSpace()
        {
            return this.canvas;
        }
    }
}
