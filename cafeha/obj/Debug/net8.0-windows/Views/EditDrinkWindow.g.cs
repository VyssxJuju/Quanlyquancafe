﻿#pragma checksum "..\..\..\..\Views\EditDrinkWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "93AEFD0AEDB405DA97C5464A4112BE1556F4CF13"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace cafeha {
    
    
    /// <summary>
    /// EditDrinkWindow
    /// </summary>
    public partial class EditDrinkWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Views\EditDrinkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DrinkInfoTextBlock;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\EditDrinkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTextBox;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Views\EditDrinkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\EditDrinkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Views\EditDrinkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ImageUrlTextBox;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\Views\EditDrinkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image DrinkImage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/cafeha;component/views/editdrinkwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\EditDrinkWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DrinkInfoTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.NameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.PriceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.CategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 52 "..\..\..\..\Views\EditDrinkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectImage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ImageUrlTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.DrinkImage = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            
            #line 64 "..\..\..\..\Views\EditDrinkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

