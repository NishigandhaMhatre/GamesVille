using Game.Models;
using Game.Services;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        // Constructor for Unit Testing
        public AboutPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();

            // Hide the Debug Settings
            DatabaseSettingsFrame.IsVisible = false;

            // Turn off the Settings Frame
            DebugSettingsFrame.IsVisible = false;

            // Set to the curent date and time
            CurrentDateTime.Text = DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        }

        /// <summary>
        /// Show or hide the Database Frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Database Section
            DatabaseSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DebugSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
           // Show or hide the Debug Settings
           DebugSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Data Source Toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataSource_Toggled(object sender, EventArgs e)
        {
            // Flip the settings
            if (DataSourceValue.IsToggled == true)
            {
                MessagingCenter.Send(this, "SetDataSource", 1);
            }
            else
            {
                MessagingCenter.Send(this, "SetDataSource", 0);
            }
        }

        /// <summary>
        /// Button to delete the data store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void WipeDataList_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Data", "Are you sure you want to delete all data?", "Yes", "No");

            if (answer)
            {
                MessagingCenter.Send(this, "WipeDataList", true);
            }
        }
        /// <summary>
        /// Character Hit Value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterHitValue_Changed(object sender, TextChangedEventArgs e)
        {
            // Set character Hit Value
            BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
            EngineViewModel.Engine.CharacterHitValue = Int32.Parse(CharacterHitValueEntry.Text);
        }
        /// <summary>
        /// Set Character to Force Miss
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterForceMiss_OnToggled(object sender, ToggledEventArgs e)
        {
            // Set character to force Miss
            BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
            EngineViewModel.Engine.CharacterHitValue = 1;
        }

        /// <summary>
        /// Set Character to Force Hit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterForceHit_OnToggled(object sender, ToggledEventArgs e)
        {
            // Set character to force Hit
            BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
            EngineViewModel.Engine.CharacterHitValue = 20;
        }
        /// <summary>
        /// Monster Hit Value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterHitValue_Changed(object sender, TextChangedEventArgs e)
        {
            // Set character Hit Value
            BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
            EngineViewModel.Engine.MonsterHitValue = Int32.Parse(MonsterHitValueEntry.Text);
        }
        /// <summary>
        /// Set Monster to Force Miss
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterForceMiss_OnToggled(object sender, ToggledEventArgs e)
        {
            // Set Monster to force Miss
            BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
            EngineViewModel.Engine.MonsterHitValue = 1;
        }

        /// <summary>
        /// Set Monster to Force Hit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterForceHit_OnToggled(object sender, ToggledEventArgs e)
        {
            // Set Monster to force Hit
            BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
            EngineViewModel.Engine.MonsterHitValue = 20;
        }
        /// <summary>
        /// Send message to set IFeelGood 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IFeelGood_OnToggled(object sender, ToggledEventArgs e)
        {
            MessagingCenter.Send(this, "IFeelGood", true);
        }
        /// <summary>
        /// Send message to set ExtraLife 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtraLife_OnToggled(object sender, ToggledEventArgs e)
        {
            MessagingCenter.Send(this, "ExtraLife", true);
        }
        /// <summary>
        /// Example of how to call for Items using HttpGet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void GetItemsGet_Command(object sender, EventArgs e)
        {
            var result = await GetItemsGet();
            await DisplayAlert("Returned List", result, "OK");
        }

        /// <summary>
        /// Call the server call for Get Items using HTTP Get
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetItemsGet()
        {
            // Call to the ItemModel Service and have it Get the Items
            // The ServerItemValue Code stands for the batch of items to get
            // as the group to request.  1, 2, 3, 100 (All), or if not specified All

            var result = "No Results";

            var value = Convert.ToInt32(ServerItemValue.Text);
            var dataList = await Services.ItemService.GetItemsFromServerGetAsync(value);

            if (dataList == null)
            {
                return result;
            }

            if (dataList.Count == 0)
            {
                return result;
            }

            // Reset the output
            result = "";

            foreach (var ItemModel in dataList)
            {
                // Add them line by one, use \n to force new line for output display.
                // Build up the output string by adding formatted ItemModel Output
                result += ItemModel.FormatOutput() + "\n";
            }

            return result;
        }

        /// <summary>
        /// Example of how to call for Items using Http Post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void GetItemsPost_Command(object sender, EventArgs e)
        {
            var result = await GetItemsPost();
            await DisplayAlert("Returned List", result, "OK");
        }

        /// <summary>
        /// Get Items using the HTTP Post command
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetItemsPost()
        {
            var result = "No Results";
            var dataList = new List<ItemModel>();

            var number = Convert.ToInt32(ServerItemValue.Text);
            var level = 6;  // Max Value of 6
            var attribute = AttributeEnum.Unknown;  // Any Attribute
            var location = ItemLocationEnum.Unknown;    // Any Location
            var random = true;  // Random between 1 and Level
            var updateDataBase = true;  // Add them to the DB
            var category = 0;   // What category to filter down to, 0 is all

            // will return shoes value 10 of speed.
            // Example  result = await ItemsController.Instance.GetItemsFromGame(1, 10, AttributeEnum.Speed, ItemLocationEnum.Feet, false, true);
            dataList = await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category, random, updateDataBase);

            if (dataList == null)
            {
                return result;
            }

            if (dataList.Count == 0)
            {
                return result;
            }

            // Reset the output
            result = "";

            foreach (var ItemModel in dataList)
            {
                // Add them line by one, use \n to force new line for output display.
                result += ItemModel.FormatOutput() + "\n";
            }

            return result;
        }
    }
}