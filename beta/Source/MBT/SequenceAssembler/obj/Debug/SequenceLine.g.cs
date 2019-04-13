﻿#pragma checksum "..\..\SequenceLine.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "07FED8B12D59517F1BE51D79B9B1AE30"
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
    /// SequenceLine
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class SequenceLine : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 97 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid containerGrid;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition metadataRow;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition sequenceRow;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock metadataBlock;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border SequenceItemsHighlight;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border sequenceItemsBorder;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel sequenceItemsPanel;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\SequenceLine.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeSequenceButton;
        
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
            System.Uri resourceLocater = new System.Uri("/SequenceAssembler;component/sequenceline.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SequenceLine.xaml"
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
            this.containerGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.metadataRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 3:
            this.sequenceRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 4:
            this.metadataBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.SequenceItemsHighlight = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.sequenceItemsBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.sequenceItemsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.closeSequenceButton = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\SequenceLine.xaml"
            this.closeSequenceButton.Click += new System.Windows.RoutedEventHandler(this.OnCloseSequenceButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

