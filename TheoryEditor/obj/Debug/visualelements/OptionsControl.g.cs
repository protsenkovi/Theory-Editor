﻿#pragma checksum "..\..\..\VisualElements\OptionsControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "21E7F38ADC90508D66360CA58F865310"
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
    /// OptionsControl
    /// </summary>
    public partial class OptionsControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\VisualElements\OptionsControl.xaml"
        internal System.Windows.Controls.Button OK;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\VisualElements\OptionsControl.xaml"
        internal System.Windows.Controls.ComboBox Languages;
        
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
            System.Uri resourceLocater = new System.Uri("/TheoryEditor;component/visualelements/optionscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\VisualElements\OptionsControl.xaml"
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
            this.OK = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\VisualElements\OptionsControl.xaml"
            this.OK.Click += new System.Windows.RoutedEventHandler(this.OK_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Languages = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\VisualElements\OptionsControl.xaml"
            this.Languages.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Languages_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
