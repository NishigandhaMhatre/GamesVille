using Game.Models;
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
	public partial class PickCharactersPage : ContentPage
	{
		readonly CharacterIndexViewModel ViewModel;

		public List<CharacterModel> CharacterSelectedList = new List<CharacterModel>();
		/// <summary>
		/// Constructor
		/// </summary>
		public PickCharactersPage()
		{
			InitializeComponent ();
			BindingContext = ViewModel = CharacterIndexViewModel.Instance;
		}

		/// <summary>
		/// The row selected from the list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnCharacter_Checked(object sender, CheckedChangedEventArgs e)
		{
            var checkBoxItem = (CheckBox)sender;
            CharacterModel selectedCharacter = (CharacterModel)checkBoxItem.BindingContext;
            if (checkBoxItem.IsChecked)
            {
                if (CharacterSelectedList.Count == 6)
                {
                    CountValidationLabel.IsVisible = true;
                    checkBoxItem.IsChecked = false;
                    return;
                }
                CharacterSelectedList.Add(selectedCharacter);
                OKButton.IsEnabled = true;
                return;
            }
            CharacterSelectedList.Remove(selectedCharacter);
            if (CharacterSelectedList.Count < 6)
            {
                CountValidationLabel.IsVisible = false;
            }
            if (CharacterSelectedList.Count== 0)
            {
                OKButton.IsEnabled = false;
            }
        }
		/// <summary>
		/// Jump to the Battle
		/// 
		/// Its Modal because don't want user to come back...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void Ok_Clicked(object sender, EventArgs e)
		{
            MessagingCenter.Send(this, "PickCharacters", CharacterSelectedList);
			await Navigation.PopAsync();
		}
	}
}