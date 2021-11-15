﻿using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Route _selectedItem;

        private EncounterProcessor _encounterProcessor;
        public ObservableCollection<Route> Routes { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command RouteEditCommand { get; }
        public Command RouteInfoCommand { get; }
        public Command RouteDeleteCommand { get; }
        public Command<Route> ItemTapped { get; set; }

        private Route _selectedRoute;

        public ItemsViewModel()
        {
            Title = "Routes";

            Routes = new ObservableCollection<Route>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Route>(OnItemSelected);

            RouteEditCommand = new Command(OnRouteEditClicked);

            RouteInfoCommand = new Command(OnAboutRouteClicked);

            RouteDeleteCommand = new Command(OnRouteDeleteClicked);

            AddItemCommand = new Command(OnAddItem);

            _encounterProcessor = EncounterProcessor.Instanse;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Routes.Clear();
                var items = await _encounterProcessor.GetAllRoutes();
                foreach (var item in items)
                {
                    Routes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Route SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                _selectedRoute = value;
                //OnItemSelected(value);
                OnRouteSelected(value);
            }
        }
        private async void OnRouteSelected(Route route)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(WaypointsViewModel.RoutesId)}={route.Id}");
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRoutePage));
        }

        public async void OnItemSelected(Route item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.RouteId)}={item.Id}");
        }
        private async void OnRouteEditClicked(object sender)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await PopupNavigation.Instance.PushAsync(new RouteEditPopup());
        }

        private async void OnAboutRouteClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync(nameof(AboutRoute));
            await Shell.Current.GoToAsync($"{nameof(AboutRoute)}?{nameof(ItemDetailViewModel.RouteId)}={2}");
        }

        private async void OnRouteDeleteClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync("..");
            await _encounterProcessor.DeleteRoute(SelectedRoute.Id);
        }
    }
}