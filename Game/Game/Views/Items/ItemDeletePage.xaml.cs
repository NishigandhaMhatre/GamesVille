using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Delete Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemDeletePage : ContentPage
    {
        // View Model for Item
        readonly GenericViewModel<ItemModel> viewModel;

        // Constructor for Unit Testing
        public ItemDeletePage(bool UnitTest) { }

        // Constructor for Delete takes a view model of what to delete
        public ItemDeletePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;

            this.viewModel.Title = "Delete " + data.Title;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Yes_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void No_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}