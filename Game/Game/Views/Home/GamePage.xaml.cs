using Game.Views.Battle;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GamePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Dungeon
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        async void DungeonButton_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new BattleThemePage());
		}

		/// <summary>
		/// Jump to the Village
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void VillageButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new VillagePage());
		}

		/// <summary>
		/// Jump to the Dungeon
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Run the Autobattle simulation from here
			await Navigation.PushAsync(new AutoBattlePage());
		}
	}
}