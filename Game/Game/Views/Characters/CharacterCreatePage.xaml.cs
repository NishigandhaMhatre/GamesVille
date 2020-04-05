using Game.Models;
using Game.ViewModels;
using Game.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Collections;
using Image = Game.Models.Image;

namespace Game.Views
{
    /// <summary>
    /// The Character create page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class CharacterCreatePage : ContentPage
    {

        
        // the character to create
        public GenericViewModel<CharacterModel> ViewModel { get; set; }

        // Empty Constructor for UTs
        public CharacterCreatePage(bool UnitTest) { }

        // The image list holding all the Image objects
        ObservableCollection<Image> imageList = new ObservableCollection<Image>();
        ArrayList itemNames = new ArrayList();


        /// <summary>
        /// Constructor that initializes the character create page with default values.
        /// </summary>
        /// <param name="data"></param>
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();
            data.Data = new CharacterModel();
            foreach(Image image in DefaultData.LoadCharacterImages())
            {
                imageList.Add(image);
            }

            ImageView.ItemsSource = imageList;


            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            SpecialAbilityPicker.SelectedItem = data.Data.SpecialAbility.ToString();
            AttackPicker.SelectedItem = data.Data.Attack.ToString();
            DefensePicker.SelectedItem = data.Data.Defense.ToString();

            var ItemViewModelInstance = ItemIndexViewModel.Instance;
            ObservableCollection<ItemModel> itemCollection = ItemViewModelInstance.Dataset;
            
            foreach(ItemModel item in itemCollection)
            {
                itemNames.Add(item.Name);
            }

        }

        /// <summary>
        /// Function that is invoked when save button was clicked in the UI. 
        /// The method sends the Character object with the Create message through messaging center publishing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
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
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }



        /// <summary>
        /// Method invoked when stepper value is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Level_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);
            int SelectedLevel = (int)e.NewValue;
            var x = CharacterIndexViewModel.Instance.GetPlayerMaxHealth(SelectedLevel);
            MaxLevelLabel.Text = x.ToString();
        }


        /// <summary>
        /// When the image is selected, assigns the image on UI source to the clicked image's URL
        /// and updates the ImageURI field on the Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnCharacterImageSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var image = args.SelectedItem as Image;

            ViewModel.Data.ImageURI = image.Url;
            CharacterImage.Source = image.Url;


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