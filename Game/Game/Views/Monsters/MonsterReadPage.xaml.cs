using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;
using Game.Helpers;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterReadPage : ContentPage
    {
        // View Model for Monster
        readonly GenericViewModel<MonsterModel> ViewModel;

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            LoadValues(data);
        }
        /// <summary>
        /// Loading Item values to the labels
        /// </summary>
        /// <param name="data"></param>
        public void LoadValues(GenericViewModel<MonsterModel> data)
        {
            HeadItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.Head);
            NecklaceItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.Necklace);
            PrimaryHandItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.PrimaryHand);
            OffHandItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.OffHand);
            RightFingerItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.RightFinger);
            LeftFingerItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.LeftFinger);
            FeetItem.Text = ItemModelHelper.GetItemModelNameFromGuid(data.Data.Feet);
        }
        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(new GenericViewModel<MonsterModel>(ViewModel.Data))));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(new GenericViewModel<MonsterModel>(ViewModel.Data))));
            await Navigation.PopAsync();
        }
    }
}