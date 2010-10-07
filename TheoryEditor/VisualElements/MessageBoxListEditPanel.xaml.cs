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

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для MessageBoxListEditPanel.xaml
    /// </summary>
    public partial class MessageBoxListEditPanel : UserControl
    {
        public MessageBoxListEditPanel(List<ITheoryElement> elements)
        {
            InitializeComponent();
            ListBoxSource.ItemsSource = elements;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!ListBoxRecepient.Items.Contains(ListBoxSource.SelectedItem))
                ListBoxRecepient.Items.Add(ListBoxSource.SelectedItem);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBoxRecepient.Items.Remove(ListBoxRecepient.SelectedItem);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public List<ITheoryElement> GetResultedList()
        {
            List<ITheoryElement> resultedList = new List<ITheoryElement>();
            foreach(ITheoryElement element in ListBoxRecepient.Items)
            {
                if (element != null)
                    resultedList.Add(element);
                else
                    return null;
            }
            return resultedList;
        }
    }
}
