using Game.Models;
using Game.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ScoreCreatePage : ContentPage
    {
        // The item to create
        GenericViewModel<ScoreModel> ViewModel { get; set; }

        // Constructor for Unit Testing
        public ScoreCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ScoreCreatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            data.Data = new ScoreModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            
            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Triggers when entry changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryChange(object sender, TextChangedEventArgs e)
        {
            ViewModel.Data.ScoreTotal = ScoreCalculation();
            ScoreLabel.Text = ViewModel.Data.ScoreTotal.ToString();
        }

        /// <summary>
        /// Calculates the score based on other attributes
        /// </summary>
        /// <returns></returns>
        int ScoreCalculation()
        {
            // TODO: To be changed if this is not acceptable
            int score = (ViewModel.Data.MonsterSlainNumber * 100) + ViewModel.Data.ExperienceGainedTotal
                - (ViewModel.Data.RoundCount * 10) - ViewModel.Data.TurnCount;
            return score;
        }
        /// <summary>
        /// Validation check on Name field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameEntry_Changed(object sender, TextChangedEventArgs e)
        {
            if (NameEntry.Text.Equals(""))
            {
                SaveButton.IsEnabled = false;
                NameValidationLabel.IsVisible = true;
                return;
            }
            SaveButton.IsEnabled = true;
            NameValidationLabel.IsVisible = false;
        }

    }
}