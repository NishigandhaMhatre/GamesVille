using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterDeletePage : ContentPage
    {
        // View Model for Monster
        readonly GenericViewModel<MonsterModel> viewModel;
        // Empty Constructor for UTs
        public MonsterDeletePage(bool UnitTest) { }
        // Constructor for Delete takes a view model of what to delete
        public MonsterDeletePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;

            this.viewModel.Title = "Delete " + data.Title;
        }
        
        /// <summary>
        /// Yes calls to Delete
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