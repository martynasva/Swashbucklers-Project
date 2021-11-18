﻿using DataLibrary.Models;
using PSI.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteEditPopup : PopupPage
    {
        public Route Item { get; set; }
        public RouteEditPopup()
        {
            InitializeComponent();
            BindingContext = new EditRouteViewModel();
        }
    }
}