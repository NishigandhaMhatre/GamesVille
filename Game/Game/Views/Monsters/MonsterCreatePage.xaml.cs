using Game.Models;
using Game.Services;
using Game.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = Game.Models.Image;

namespace Game.Views
{
    /// <summary>
    /// Monster create page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        GenericViewModel<MonsterModel> ViewModel { get; set; }
      
        // The image list holding all the Image objects
        ObservableCollection<Image> imageList = new ObservableCollection<Image>();
        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

        // Constructor
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();
            data.Data = new MonsterModel();

            foreach (Image image in DefaultData.LoadMonsterImages())
            {
                imageList.Add(image);
            }
            ImageView.ItemsSource = imageList;

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            //Need to make the SelectedItem a string, so it can select the correct item.
            AttackPicker.SelectedItem = data.Data.Attack.ToString();
            DefensePicker.SelectedItem = data.Data.Defense.ToString();
            SpeedPicker.SelectedItem = data.Data.Speed.ToString();
        }

        /// <summary>
        /// Triggers when a monster image is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnMonsterImageSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var image = args.SelectedItem as Image;

            ViewModel.Data.ImageURI = image.Url;
            MonsterImage.Source = image.Url;

        }

        /// <summary>
        /// Method inviked when stepper value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Level_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);
            int SelectedLevel = (int)e.NewValue;
            var max_health = CharacterIndexViewModel.Instance.GetPlayerMaxHealth(SelectedLevel);
            MaxLevelLabel.Text = max_health.ToString();
        }

        /// <summary>
        /// Function that is invoked when save button was clicked in the UI. 
        /// The method sends the Monster object with the Create message through messaging center publishing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = ItemService.DefaultImageURI;
            }

            ViewModel.Data.CurrentHealth = ViewModel.Data.MaxHealth;

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();

        }

        /// <summary>
        /// Function that is invoked when cancel button was clicked in the UI.
        /// It pops the current page from the stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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