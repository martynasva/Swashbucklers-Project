﻿using PSI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizAnswerPage : ContentPage
    {
        public QuizAnswerPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new QuizzesAnswersViewModel();
        }

        QuizzesAnswersViewModel _viewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}