﻿#pragma checksum "..\..\..\visualelements\TheoryImporterPanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "579D41445EB7260DF5E372D45232722B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:2.0.50727.4952
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Protsenko.TheoryEditor.VisualElements {
    
    
    /// <summary>
    /// TheoryImporterPanel
    /// </summary>
    public partial class TheoryImporterPanel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\visualelements\TheoryImporterPanel.xaml"
        internal System.Windows.Controls.Button Open;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\visualelements\TheoryImporterPanel.xaml"
        internal System.Windows.Controls.Button Browse;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\visualelements\TheoryImporterPanel.xaml"
        internal System.Windows.Controls.TextBox textBoxPath;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\visualelements\TheoryImporterPanel.xaml"
        internal System.Windows.Controls.TextBox textBoxRedactor;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\visualelements\TheoryImporterPanel.xaml"
        internal System.Windows.Controls.ContextMenu cm;
        
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
            System.Uri resourceLocater = new System.Uri("/TheoryEditor;component/visualelements/theoryimporterpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\visualelements\TheoryImporterPanel.xaml"
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
            this.Open = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\visualelements\TheoryImporterPanel.xaml"
            this.Open.Click += new System.Windows.RoutedEventHandler(this.Open_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Browse = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\visualelements\TheoryImporterPanel.xaml"
            this.Browse.Click += new System.Windows.RoutedEventHandler(this.Browse_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textBoxPath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBoxRedactor = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cm = ((System.Windows.Controls.ContextMenu)(target));
            
            #line 20 "..\..\..\visualelements\TheoryImporterPanel.xaml"
            this.cm.Opened += new System.Windows.RoutedEventHandler(this.cm_Opened);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\visualelements\TheoryImporterPanel.xaml"
            this.cm.Closed += new System.Windows.RoutedEventHandler(this.cm_Closed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}