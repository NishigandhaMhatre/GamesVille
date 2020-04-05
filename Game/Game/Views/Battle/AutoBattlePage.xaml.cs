using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage ()
		{
			InitializeComponent ();
		}
		/// <summary>
		/// Auto battle button to call auto battle engine and display the results
		/// </summary>
		public async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Call into Auto Battle from here to do the Battle...

			var Engine = new Game.Engine.AutoBattleEngine();
            
			var result = await Engine.RunAutoBattle();
			
			var Score = Engine.GetScoreObject();

            string RoundMessage = string.Format("No of Rounds: {0}\n", Score.RoundCount);
            string TurnMessage = string.Format("No of Turns: {0}\n", Score.TurnCount);
            string BattleDetails = "Game Over\n"+ RoundMessage + TurnMessage;
            BattleDetails = BattleDetails.Replace("\n", Environment.NewLine);
            BattleMessageValue.Text = BattleDetails;
		}
	}
}