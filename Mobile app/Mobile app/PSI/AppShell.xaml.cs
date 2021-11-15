﻿using PSI.ViewModels;
using PSI.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PSI
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewRoutePage), typeof(NewRoutePage));
            Routing.RegisterRoute(nameof(WaypointInfo), typeof(WaypointInfo));
            Routing.RegisterRoute(nameof(AboutRoute), typeof(AboutRoute));
            Routing.RegisterRoute(nameof(NewWaypointPage), typeof(NewWaypointPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
