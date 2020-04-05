using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InstructionsPage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public InstructionsPage()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Back to the HomePage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        async void HomeButton_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
		}
	}
}