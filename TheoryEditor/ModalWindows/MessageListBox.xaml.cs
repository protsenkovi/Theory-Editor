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
using Protsenko.TheoryEditor.Core;
using Protsenko.TheoryEditor.Core.Languages;
using Protsenko.TheoryEditor.Core.Interfaces;

namespace Protsenko.TheoryEditor.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для MessageListBox.xaml
    /// </summary>
    public partial class MessageListBox : Window, IMultiLanguage
    {
        public object choosenElement;
        public Type typeOfElement { get; private set; }
        public bool notcanceled = false;
        private Mods mod;

        public enum Mods
        {
            Choose,
            Edit
        }

        protected MessageListBox(List<ITheoryElement> items, Mods mod)
        {
            InitializeComponent();
            
            this.mod = mod;
            if (mod == Mods.Edit)
            {
                MessageBoxListEditPanel panel = new MessageBoxListEditPanel(items);
                Box.Content = panel;
            }
            else if (mod == Mods.Choose)
            {
                typeOfElement = typeof(ITheoryElement);
                ListBox listbox = new ListBox();
                ListBoxItem item;
                foreach (ITheoryElement element in items)
                {
                    item = new ListBoxItem();
                    item.Content = element;
                    //item.Content = type;
                    listbox.Items.Add(item);
                }
                listbox.SelectionChanged += new SelectionChangedEventHandler(Listbox_SelectionChanged);
                Box.Content = listbox;
            }
        }

        protected MessageListBox(List<TheoryTypes> items, Mods mod)
        {
            InitializeComponent();
            this.mod = mod;
            if (mod == Mods.Edit)
            {
                throw new NotImplementedException("Так вот.");
            }
            else if (mod == Mods.Choose)
            {
                typeOfElement = typeof(TheoryTypes);
                ListBox listbox = new ListBox();
                ListBoxItem item;
                foreach (TheoryTypes type in items)
                {
                    item = new ListBoxItem();
                    item.Name = type.ToString();
                    item.Content = type;
                    listbox.Items.Add(item);
                }
                listbox.SelectionChanged += new SelectionChangedEventHandler(Listbox_SelectionChanged);
                Box.Content = listbox;
            }
            LanguageResource_Updated(null);
        }

        public static MessageListBox Show(List<ITheoryElement> items, Mods mod)
        {
            MessageListBox box = new MessageListBox(items, mod);
            box.ShowDialog();
            return box;
        }

        public static MessageListBox Show(List<TheoryTypes> items, Mods mod)
        {
            MessageListBox box = new MessageListBox(items, mod);
            box.ShowDialog();
            return box;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (mod == Mods.Edit)
            {
                choosenElement = ((MessageBoxListEditPanel)Box.Content).GetResultedList();
                notcanceled = true;
            }
            else if (mod == Mods.Choose)
            {
                if (choosenElement != null) notcanceled = true;
            }                                                               
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeOfElement == typeof(ITheoryElement))
            {
                choosenElement = ((ListBoxItem)((ListBox)sender).SelectedItem).Content;
            }
            else
            {
                //choosenElement = TheoryTypesClass.GetType((string)(((ListBoxItem)((ListBox)sender).SelectedItem).Name));
                choosenElement = ((ListBoxItem)((ListBox)sender).SelectedItem).Content;
            }
        }

        #region Члены IMultiLanguage

        public void LanguageResource_Updated(object language)
        {
            if (mod == Mods.Choose)
            {
                foreach (ListBoxItem item in ((ListBox)Box.Content).Items)
                {
                    item.Content = LanguageResource.currentDictionary[item.Name];
                }
                ((ListBox)Box.Content).Items.Refresh();
            }
            Ok.Content = LanguageResource.currentDictionary["ok"];
            Cancel.Content = LanguageResource.currentDictionary["cancel"];
        }

        #endregion
    }
}
