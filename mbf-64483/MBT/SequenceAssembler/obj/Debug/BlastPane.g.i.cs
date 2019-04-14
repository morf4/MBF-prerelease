﻿#pragma checksum "..\..\BlastPane.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2633C7E09BB69C3827F62FA40274501"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SequenceAssembler;
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
using System.Windows.Shell;


namespace SequenceAssembler {
    
    
    /// <summary>
    /// BlastPane
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class BlastPane : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 21 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl webServiceReportTab;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtVersion;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDate;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDataBaseName;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstSingleLineReport;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBlastHeader;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\BlastPane.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SequenceAssembler.SilverMapController silverMapControl;
        
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
            System.Uri resourceLocater = new System.Uri("/SequenceAssembler;component/blastpane.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BlastPane.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.webServiceReportTab = ((System.Windows.Controls.TabControl)(target));
            return;
            case 2:
            this.txtVersion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtDataBaseName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.lstSingleLineReport = ((System.Windows.Controls.ListView)(target));
            
            #line 46 "..\..\BlastPane.xaml"
            this.lstSingleLineReport.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.OnSingleLineReportHeaderClick));
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnBlastHeader = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.silverMapControl = ((SequenceAssembler.SilverMapController)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 6:
            
            #line 124 "..\..\BlastPane.xaml"
            ((System.Windows.Controls.TextBlock)(target)).AddHandler(System.Windows.Documents.Hyperlink.RequestNavigateEvent, new System.Windows.Navigation.RequestNavigateEventHandler(this.OnSubjectIDRequestNavigate));
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
