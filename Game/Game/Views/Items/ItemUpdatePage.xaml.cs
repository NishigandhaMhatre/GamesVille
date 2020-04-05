using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;
using System.Collections.ObjectModel;
using Image = Game.Models.Image;
using Game.Services;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemUpdatePage : ContentPage
    {

        // The image list holding all the Image objects
        ObservableCollection<Image> imageList = new ObservableCollection<Image>();

        // View Model for Item
        readonly GenericViewModel<ItemModel> ViewModel;

        // Constructor for Unit Testing
        public ItemUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;
            
            //Adding images for updating the item image 
            foreach (Image image in DefaultData.LoadItemImages())
            {
                imageList.Add(image);
            }

            ImageView.ItemsSource = imageList;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update
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

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = String.Format("{0}", e.NewValue);
        }

        void OnItemImageSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var image = args.SelectedItem as Image;

            ViewModel.Data.ImageURI = image.Url;
            ItemImage.Source = image.Url;
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