﻿#pragma checksum "..\..\..\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D6A807110AD3AE6C44E405AE6E415D63"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
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
using System.Windows.Shell;


namespace KbRevitstore {
    
    
    /// <summary>
    /// WebBrowser
    /// </summary>
    public partial class WebBrowser : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KbRevitstore.WebBrowser DockableDialogs;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAddressBar;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNavigate;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser webBrowser;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KbRevitstore;component/mainpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DockableDialogs = ((KbRevitstore.WebBrowser)(target));
            
            #line 8 "..\..\..\MainPage.xaml"
            this.DockableDialogs.Loaded += new System.Windows.RoutedEventHandler(this.DockableDialogs_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtAddressBar = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\MainPage.xaml"
            this.txtAddressBar.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtAddressBar_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnNavigate = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\MainPage.xaml"
            this.btnNavigate.Click += new System.Windows.RoutedEventHandler(this.btnNavigate_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.webBrowser = ((System.Windows.Controls.WebBrowser)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

