﻿#pragma checksum "..\..\..\mainwindow\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D4A43AC64C2A8B404B07A4AA1B2FF63"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:2.0.50727.4952
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Protsenko.TheoryEditor;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Protsenko.TheoryEditor {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.DockPanel maindock;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.Menu Menu;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem _New;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem _Add;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem _Save;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem _Load;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem _Exit;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem Mod;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem BasicMod;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem QuickRedactorMod;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem Options;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem Help;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\mainwindow\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem Close;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TheoryEditor;component/mainwindow/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\mainwindow\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.maindock = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 2:
            this.Menu = ((System.Windows.Controls.Menu)(target));
            return;
            case 3:
            this._New = ((System.Windows.Controls.MenuItem)(target));
            
            #line 9 "..\..\..\mainwindow\MainWindow.xaml"
            this._New.Click += new System.Windows.RoutedEventHandler(this.New_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this._Add = ((System.Windows.Controls.MenuItem)(target));
            
            #line 10 "..\..\..\mainwindow\MainWindow.xaml"
            this._Add.Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this._Save = ((System.Windows.Controls.MenuItem)(target));
            
            #line 11 "..\..\..\mainwindow\MainWindow.xaml"
            this._Save.Click += new System.Windows.RoutedEventHandler(this._Save_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this._Load = ((System.Windows.Controls.MenuItem)(target));
            
            #line 12 "..\..\..\mainwindow\MainWindow.xaml"
            this._Load.Click += new System.Windows.RoutedEventHandler(this._Load_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this._Exit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 13 "..\..\..\mainwindow\MainWindow.xaml"
            this._Exit.Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Mod = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 9:
            this.BasicMod = ((System.Windows.Controls.MenuItem)(target));
            
            #line 16 "..\..\..\mainwindow\MainWindow.xaml"
            this.BasicMod.Click += new System.Windows.RoutedEventHandler(this.BasicMod_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.QuickRedactorMod = ((System.Windows.Controls.MenuItem)(target));
            
            #line 17 "..\..\..\mainwindow\MainWindow.xaml"
            this.QuickRedactorMod.Click += new System.Windows.RoutedEventHandler(this.QuickRedactorMod_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Options = ((System.Windows.Controls.MenuItem)(target));
            
            #line 19 "..\..\..\mainwindow\MainWindow.xaml"
            this.Options.Click += new System.Windows.RoutedEventHandler(this.Options_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Help = ((System.Windows.Controls.MenuItem)(target));
            
            #line 20 "..\..\..\mainwindow\MainWindow.xaml"
            this.Help.Click += new System.Windows.RoutedEventHandler(this.Help_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Close = ((System.Windows.Controls.MenuItem)(target));
            
            #line 21 "..\..\..\mainwindow\MainWindow.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.CloseMenuItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
