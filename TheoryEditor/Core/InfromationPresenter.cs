using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text;

using Protsenko.TheoryEditor.Core.Types;
using Protsenko.TheoryEditor.VisualElements;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core
{
    sealed class InfromationPresenter
    {
        class TheoryStackPanel : StackPanel, Core.Interfaces.ITheoryContainer
        {
            #region Члены ITheoryContainer

            public ITheoryElement GetTheoryElement()
            {
                return ((TheoryVisualElement)this.Children[0]).GetTheoryElement();
            }

            public List<ITheoryElement> GetTheoryElements()
            {
                List<ITheoryElement> list = new List<ITheoryElement>(this.Children.Count);
                foreach (TheoryVisualElement element in this.Children)
                    list.Add(element.GetTheoryElement());
                return list;
            }

            #endregion
        }

        public static UIElement MakeVisual(ITheoryElement implementation)
        {
            return new TheoryVisualElement(implementation);
        }

        public static UIElement MakeVisual(List<ITheoryElement> implementations)
        {
            TheoryStackPanel stackpanel = new TheoryStackPanel();
            foreach (ITheoryElement implementation in implementations)
            {
                stackpanel.Children.Add(MakeVisual(implementation));
            }
            return stackpanel;
        }

        public static string MakeFormattedText(ITheoryElement element)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine(LanguageResource.currentDictionary[element.GetTheoryType().ToString()]);
            strBuilder.AppendLine("   " + element.Formulation.Replace("\n", "\n   "));
            if (element is Theorem)
            {
                strBuilder.AppendLine(LanguageResource.currentDictionary["proof"]);
                strBuilder.AppendLine("  " + ((Theorem)element).Proof.Replace("\n", "\n   "));
            }
            strBuilder.AppendLine();
            return strBuilder.ToString();
        }
    }
}
