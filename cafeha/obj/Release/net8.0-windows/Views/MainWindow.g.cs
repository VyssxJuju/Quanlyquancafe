﻿#pragma checksum "..\..\..\..\Views\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6847A7B306639A6E4BCFF667A54A29547FAB7D22"
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
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 15 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuItem_AddMenu;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuItem_CreateOrder;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuItem_AddRevenue;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuItem_AddStaff;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MenuItem_Logout;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl DrinkList;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox SelectedDrinksList;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox SpecialItemsList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/cafeha;component/views/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MenuItem_AddMenu = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\Views\MainWindow.xaml"
            this.MenuItem_AddMenu.Click += new System.Windows.RoutedEventHandler(this.MenuItem_AddMenu_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MenuItem_CreateOrder = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\Views\MainWindow.xaml"
            this.MenuItem_CreateOrder.Click += new System.Windows.RoutedEventHandler(this.MenuItem_CreateOrder_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MenuItem_AddRevenue = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\Views\MainWindow.xaml"
            this.MenuItem_AddRevenue.Click += new System.Windows.RoutedEventHandler(this.MenuItem_AddRevenue_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MenuItem_AddStaff = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\Views\MainWindow.xaml"
            this.MenuItem_AddStaff.Click += new System.Windows.RoutedEventHandler(this.MenuItem_AddStaff_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MenuItem_Logout = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\..\Views\MainWindow.xaml"
            this.MenuItem_Logout.Click += new System.Windows.RoutedEventHandler(this.MenuItem_Logout_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DrinkList = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 8:
            this.SelectedDrinksList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            
            #line 61 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateOrder_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SpecialItemsList = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 7:
            
            #line 36 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.DrinkCheckbox_Checked);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.DrinkCheckbox_Unchecked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

