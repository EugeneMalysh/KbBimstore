﻿#pragma checksum "..\..\WebBrowser.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EE3759EE836BDD38B247FAB65D24482ACE50F042"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using System.Windows.Forms.Integration;
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


namespace KbBimstore {
    
    
    /// <summary>
    /// KBRevitWebBrowser
    /// </summary>
    public partial class KBRevitWebBrowser : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\WebBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KbBimstore.KBRevitWebBrowser DockableDialogs;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\WebBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAddressBar;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\WebBrowser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNavigate;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\WebBrowser.xaml"
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
            System.Uri resourceLocater = new System.Uri("/KbBimstore;component/webbrowser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WebBrowser.xaml"
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
            this.DockableDialogs = ((KbBimstore.KBRevitWebBrowser)(target));
            
            #line 8 "..\..\WebBrowser.xaml"
            this.DockableDialogs.Loaded += new System.Windows.RoutedEventHandler(this.DockableDialogs_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtAddressBar = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\WebBrowser.xaml"
            this.txtAddressBar.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtAddressBar_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnNavigate = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\WebBrowser.xaml"
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
