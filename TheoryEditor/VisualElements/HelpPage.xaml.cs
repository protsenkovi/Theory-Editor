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

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public event Core.Events.Accepted Accepted;
        private double dx = 0;
        private double lastX = 0;
        private bool mouseNavigate = false;


        public HelpPage()
        {
            InitializeComponent();
            Accepted += new Protsenko.TheoryEditor.Core.Events.Accepted(HelpPage_Accepted);
        }

        public HelpPage(Uri uri)
        {
            InitializeComponent();
            Accepted += new Protsenko.TheoryEditor.Core.Events.Accepted(HelpPage_Accepted);
            FramePage.Navigate(uri);
            //this.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(FramePage_MouseRightButtonDown);
            //this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(FramePage_MouseRightButtonUp);
            //this.PreviewMouseMove += new MouseEventHandler(FramePage_MouseMove);
            this.KeyDown += new KeyEventHandler(HelpPage_KeyDown);
        }

        void HelpPage_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left) && (e.Key == Key.Return))
                FramePage.GoBack();
        }

        //void FramePage_MouseMove(object sender, MouseEventArgs e)
        //{
        //    dx = lastX + e.GetPosition(this).X;
        //}

        //void FramePage_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (dx > 0)
        //    {
        //        FramePage.GoForward();
        //    }
        //    else
        //    {
        //        FramePage.GoBack();
        //    }
        //    mouseNavigate = false;
        //    dx = 0;
        //}

        //void FramePage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    lastX = e.GetPosition(this).X;
        //    mouseNavigate = true;
        //}

        void HelpPage_Accepted()
        {
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Accepted();
        }
    }
}
