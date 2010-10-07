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
using Protsenko.TheoryEditor.Core.Interfaces;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class TheoryVisualElement : UserControl, ITheoryContainer, IMultiLanguage
    {
        Core.Types.ITheoryElement element;

        public ITheoryElement Element { get { return element; } private set { } }

        public TheoryVisualElement()
        {
            InitializeComponent();
        }

        public TheoryVisualElement(ITheoryElement element)
        {
            InitializeComponent();
            this.element = element;
            Title.Text = element.GetTheoryType().ToString();
            formulation.Text = element.Formulation;
            element.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(element_Updated);
            LanguageResource.Updated +=new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
            element_Updated(element);
            LanguageResource_Updated(null);
        }

        void element_Updated(object sender)
        {
            ITheoryElement element = (ITheoryElement)sender;
            formulation.Text = element.Formulation;
            if (sender is Theorem) 
            {
                formulation.Text += "\r\n\r\n" + LanguageResource.currentDictionary["proof"] 
                                    + "\r\n  " + ((Theorem)sender).Proof;
            }
            LanguageResource_Updated(null);
        }

        #region Члены ITheoryContainer

        public Protsenko.TheoryEditor.Core.Types.ITheoryElement GetTheoryElement()
        {
            return element;
        }

        public List<Protsenko.TheoryEditor.Core.Types.ITheoryElement> GetTheoryElements()
        {
            List<Protsenko.TheoryEditor.Core.Types.ITheoryElement> list = new List<Protsenko.TheoryEditor.Core.Types.ITheoryElement>();
            list.Add(element);
            return list;
        }

        #endregion

        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            Title.Text = LanguageResource.currentDictionary[element.GetTheoryType().ToString()];
        }

        #endregion
    }
}
