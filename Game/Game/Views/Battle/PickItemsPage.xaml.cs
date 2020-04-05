using Game.Models;
using Game.Services;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {
        public List<ItemModel> DroppedItemsList = new List<ItemModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            InitializeComponent();
            LoadItems();
            ItemsListView.ItemsSource = DroppedItemsList; // Binding Dropped items to pick items page
        }
        /// <summary>
        /// Loading dropped items in the battle
        /// </summary>
        private void LoadItems()
        {
            foreach (ItemModel data in BattleEngineViewModel.Instance.Engine.BattleScore.ItemModelDropList)
            {
                DroppedItemsList.Add(data);
            }
        }

        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Auto assign items to characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AutoPickUpButton_Clicked(object sender, EventArgs e)
        {
            // Distribute the Items
            BattleEngineViewModel.Instance.Engine.PickupItemsForAllCharacters();
            await Navigation.PopModalAsync();
        }
    }
}