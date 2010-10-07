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
using Protsenko.TheoryEditor.Core.Events;
using Protsenko.TheoryEditor.ModalWindows;
using Protsenko.TheoryEditor.Core;
using Protsenko.TheoryEditor.Core.Interfaces;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для CreateElementPanel.xaml
    /// </summary>
    public partial class CreateElementPanel : UserControl, IMultiLanguage
    {
        protected Core.Session currentSession;
        private TheoryTree tree;
        TheoryTypes type;
        List<ITheoryElement> basis = null;
        bool typechoosen = false;
        public event Accepted Accepted;
        private ITheoryElement element = null;
        private Mods mod;

        enum Mods
        {
            Create,
            Edit
        }

        public CreateElementPanel(Core.Session session)
        {
            InitializeComponent();
            this.currentSession = session;
            basis = new List<ITheoryElement>();
            TheoryTypeBox.Text = type.ToString();
            LinksItemBox.ItemsSource = basis;
            mod = Mods.Create;
            LanguageResource.Updated += new Updated(LanguageResource_Updated);
            FormulationTextBox.Focus();
            LanguageResource_Updated(null);
        }

        public CreateElementPanel(ITheoryElement element, List<ITheoryElement> basis, Core.Session session, TheoryTree tree)
        {
            InitializeComponent();
            this.element = element;
            this.basis = basis;
            this.currentSession = session;
            this.type = element.GetTheoryType();
            this.tree = tree;

            TheoryTypeBox.Text = type.ToString();
            LinksItemBox.ItemsSource = basis;
            FormulationTextBox.Text = element.Formulation;
            TemplateTextBox.Text = element.Template;
            if (element is Theorem)
                ProofTextBox.Text = ((Theorem)element).Proof;
            mod = Mods.Edit;
            LanguageResource.Updated += new Updated(LanguageResource_Updated);
            Update();
            LanguageResource_Updated(null);
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (mod == Mods.Create)
            {
                MessageListBox b = MessageListBox.Show(currentSession.GetAllTheoryElements(), MessageListBox.Mods.Choose);
                if (b.notcanceled)
                {
                    if (!basis.Contains((Core.Types.ITheoryElement)b.choosenElement))  //?
                        basis.Add((Core.Types.ITheoryElement)b.choosenElement);
                }
                LinksItemBox.Items.Refresh();
            }
            else if (mod == Mods.Edit)
            {
                List<ITheoryElement> links = currentSession.GetAllTheoryElements();
                links.Remove(element);
                MessageListBox b = MessageListBox.Show(links, MessageListBox.Mods.Edit);
                if (b.notcanceled)
                {
                    LinksItemBox.ItemsSource = (List<ITheoryElement>)b.choosenElement;
                }
                LinksItemBox.Items.Refresh();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mod == Mods.Create)
            {
                if (currentSession.AddTheoryElement(type, 0, FormulationTextBox.Text, TemplateTextBox.Text, ProofTextBox.Text, basis))
                    Accepted();
            }
            else
            {
                tree.SetBasis(element,(List<ITheoryElement>)LinksItemBox.ItemsSource);
                Accepted();
            }
        }

        /// <summary>
        /// Метод обрабатывающий нажатие кнопки выбора типа теории.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseTypeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageListBox b = MessageListBox.Show(Core.TheoryTypesClass.GetTypes(), MessageListBox.Mods.Choose);
            if (b.notcanceled)
            {
                type = TheoryTypesClass.GetType((string)b.choosenElement);
                typechoosen = true;

                Update();
                LanguageResource_Updated(null);
            }
        }

        /// <summary>
        /// Метод вызывающийся при изменении текста в TextBox(Текстовая Коробка).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (this.element != null)
            {
                if (box == FormulationTextBox)
                {
                    this.element.Formulation = box.Text;
                }
                else if (box == TemplateTextBox)
                {
                    this.element.Template = box.Text;
                }
                else if (box == ProofTextBox)
                {
                    if (element is Theorem)
                        ((Theorem)element).Proof = ProofTextBox.Text;
                }
            }
        }

        public void Update()
        {
            if (type == TheoryTypes.Theorem)
                ProofPanel.Visibility = Visibility.Visible;
            else
                ProofPanel.Visibility = Visibility.Collapsed;
        }

        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            TheoryTypeBox.Text = LanguageResource.currentDictionary[type.ToString()];
            BlockBasis.Text = LanguageResource.currentDictionary["basis"] + ":";
            BlockFormulation.Text = LanguageResource.currentDictionary["formulation"];
            BlockProof.Text = LanguageResource.currentDictionary["proof"];
            BlockTemplate.Text = LanguageResource.currentDictionary["template"];
            BlockType.Text = LanguageResource.currentDictionary["type"] + ": ";
        }

        #endregion
    }
}
