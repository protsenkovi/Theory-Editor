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

namespace TheoryEditor.visualelements
{
    /// <summary>
    /// Логика взаимодействия для MessageListBox.xaml
    /// </summary>
    public partial class MessageListBox : Window
    {
        public object choosenElement;
        public bool notcanceled = false;

        protected MessageListBox(List<core.Types.ITheoryElement> items)
        {
            InitializeComponent();
            Listbox.ItemsSource = items;
        }

        protected MessageListBox(List<core.TheoryTypes> items)
        {
            InitializeComponent();
            Listbox.ItemsSource = items;
        }

        public static MessageListBox Show(List<core.Types.ITheoryElement> items)
        {
            MessageListBox box = new MessageListBox(items);
            box.ShowDialog();
            return box;
        }

        public static MessageListBox Show(List<core.TheoryTypes> items)
        {
            MessageListBox box = new MessageListBox(items);
            box.ShowDialog();
            return box;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (choosenElement != null) notcanceled = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choosenElement = Listbox.SelectedItem; 
        }
    }
}
