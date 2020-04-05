using Game.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Engine;
using Game.Services;
using System.Linq;
using Game.ViewModels;
using System.Diagnostics;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattlePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        public List<PlayerInfoModel> SelectedCharacterList;
        public Dictionary<String, int> SelectedCharacterMap = new Dictionary<String, int>();

        public List<PlayerInfoModel> SelectedMonsterList;
        public Dictionary<String, int> SelectedMonsterMap = new Dictionary<String, int>();

        public Dictionary<String, bool> SpecialAbilityMap = new Dictionary<String, bool>();

        public PlayerInfoModel CurrentPlayer;

        // public BattleEngine Battle;
        public bool isSpecialAbilityUsedForAttack = false;

        public int[] AttackerPosition = new int[2];
        public int[] DefenderPosition = new int[2];

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage(int ThemeIndex)
        {
            InitializeComponent();
            SetTheme(ThemeIndex);

            //Battle = new BattleEngine();
            foreach (CharacterModel Character in BattleEngineViewModel.Instance.SelectedCharacters)
            {
                EngineViewModel.Engine.PopulateCharacterList(Character);
            }
            EngineViewModel.Engine.StartBattle(false);

            SelectedCharacterList = EngineViewModel.Engine.CharacterList;
            SelectedMonsterList = EngineViewModel.Engine.MonsterList;

            if (EngineViewModel.IFeelGood == true)
            {
                foreach (PlayerInfoModel character in SelectedCharacterList)
                {
                    character.Attack += 20;
                }
                foreach (PlayerInfoModel monster in SelectedMonsterList)
                {
                    if (monster.Attack >= 20)
                    {
                        monster.Attack -= 20;
                    }
                    if (monster.Attack < 20)
                    {
                        monster.Attack = 0;
                    }
                }
            }
            

            LoadPlayers();

            PickPlayers();

        }
        /// <summary>
        /// Loading Characters and Monsters for the play
        /// </summary>
        private void LoadPlayers()
        {
            LoadCharacters();
            LoadMonsters();
        }
        /// <summary>
        /// Loading selected characters into the battle grid
        /// </summary>
        private void LoadCharacters()
        {
            SelectedCharacterMap.Clear();
            SpecialAbilityMap.Clear();
            for (int i = 0; i < SelectedCharacterList.Count && SelectedCharacterList[i].Alive; i++)
            {
                AddImage(i, 0, SelectedCharacterList[i].ImageURI);
                SelectedCharacterMap.Add(SelectedCharacterList[i].Guid, i);
               
                SpecialAbilityMap.Add(SelectedCharacterList[i].Guid, true);
            }
        }
        /// <summary>
        /// Loading Monsters into the battle grid
        /// </summary>
        private void LoadMonsters()
        {
            SelectedMonsterMap.Clear();
            for (int i = 0; i < SelectedMonsterList.Count; i++)
            {
                AddImage(i, 5, SelectedMonsterList[i].ImageURI);
                SelectedMonsterMap.Add(SelectedMonsterList[i].Guid, i);
            }
        }
        /// <summary>
        /// Reset UI elements on board before loading next round of players
        /// </summary>
        private void ResetBoard()
        {
            for (int i = 0; i < 6; i++)
            {
                RemoveImage(i, 0);
                RemoveImage(i, 5);
            }
        }
        /// <summary>
        /// Game logic method, called when attacker attacks defender
        /// </summary>
        public void playBattle(bool isSpecialAbilityUsedForAttack)
        {
            EngineViewModel.Engine.TurnAsAttack(EngineViewModel.Engine.CurrentAttacker, EngineViewModel.Engine.CurrentDefender, isSpecialAbilityUsedForAttack);
            GameMessage();
            //Check if Defender Died 
            if (EngineViewModel.Engine.CurrentDefender.Alive == false)
            {
                ShowPlayerIsDead();
            }
            //check if battle is over
            if (EngineViewModel.Engine.CharacterList.Count < 1)
            {
                ClearMessages();

                GameOver();

                return;
            }
            //check if round is over
            if (EngineViewModel.Engine.MonsterList.Count < 1)
            {
                ClearMessages();

                RoundOver();

                EngineViewModel.Engine.NewRound(); // new round begun
                //character reincarnates in round 2 if debug switch extra life is true 
                if (EngineViewModel.Engine.BattleScore.RoundCount == 2 && EngineViewModel.ExtraLife == true)
                {
                    if (EngineViewModel.Engine.BattleScore.CharacterModelDeathList.Count() > 0)
                    {
                        EngineViewModel.Engine.BattleScore.CharacterModelDeathList[0].Alive = true;
                        SelectedCharacterList.Add(EngineViewModel.Engine.BattleScore.CharacterModelDeathList[0]);
                        EngineViewModel.Engine.BattleMessagesModel.ReincarnatedCharName = EngineViewModel.Engine.BattleScore.CharacterModelDeathList[0].Name;
                        BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.GetReincarnatedPlayerMessage(), BattleMessages.Text);
                    }
                    

                }
                SelectedMonsterList = EngineViewModel.Engine.MonsterList; // initialize monsters based on alive characters

                ResetBoard();
                LoadPlayers();

            }
           
            PickPlayers(); // pick attacker and defender for next turn
        }
        /// <summary>
        /// Show the Round Over
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void RoundOver()
        {
            // Wrap up
            // Hide the Game Board
            GameUIDisplay.IsVisible = false;

            // Show the Round Over Display
            RoundOverDisplay.IsVisible = true;
        }
        /// <summary>
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Hide the Game Board
            GameUIDisplay.IsVisible = false;

            // Show the Game Over Display
            GameOverDisplay.IsVisible = true;
        }
        /// <summary>
        /// Pick the Attacker and Defender for the turn
        /// </summary>
        public void PickPlayers()
        {
            ResetCurrentPlayers();
            int RoundNumber = EngineViewModel.Engine.BattleScore.RoundCount;
            EngineViewModel.Engine.PlayerCurrent = EngineViewModel.Engine.GetNextPlayerTurn(RoundNumber); //get the attacker
            EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.PlayerCurrent;
            SetCurrentAttacker();

            CurrentPlayer = EngineViewModel.Engine.CurrentAttacker;
            CheckPlayerAbilities();

            EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker); // get the defender
            SetCurrentDefender();
        }

        #region UIOperations
        /// <summary>
        /// Setting Battle Field background
        /// </summary>
        /// <param name="themeIndex"></param>
        private void SetTheme(int themeIndex)
        {
            switch (themeIndex)
            {
                case 0:
                    BattleScreen.BackgroundImageSource = "sky_theme.png";
                    break;
                case 1:
                    BattleScreen.BackgroundImageSource = "lab_background.png";
                    break;
                case 2:
                    BattleScreen.BackgroundImageSource = "city_theme.png";
                    break;
                case 3:
                    BattleScreen.BackgroundImageSource = "ground_theme.png";
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Checking for player abilities and setting the UI elements
        /// </summary>
        private void CheckPlayerAbilities()
        {
            if (CurrentPlayer.PlayerType == PlayerTypeEnum.Monster)
            {
                BackwardButton.IsEnabled = false;
                ForwardButton.IsEnabled = false;
                UpButton.IsEnabled = false;
                DownButton.IsEnabled = false;
                SpecialAbility.IsEnabled = false;
                return;
            }
            BackwardButton.IsEnabled = true;
            ForwardButton.IsEnabled = true;
            UpButton.IsEnabled = true;
            DownButton.IsEnabled = true;

            if (EngineViewModel.Engine.CurrentAttacker.SpecialAbility != SpecialAbilityEnum.Unknown && SpecialAbilityMap[EngineViewModel.Engine.CurrentAttacker.Guid] == true)
            {
                SpecialAbility.IsEnabled = true;
                return;
            }
            SpecialAbility.IsEnabled = false;
        }
        /// <summary>
        /// Modify UI to show player is Dead
        /// </summary>
        private void ShowPlayerIsDead()
        {
            AddImageOpacity(DefenderPosition[0], DefenderPosition[1]);
            Frame DefenderFrame = GetFrame(DefenderPosition[0], DefenderPosition[1]);
            DefenderFrame.BackgroundColor = Color.Red;
            DefenderFrame.IsVisible = true;
        }
        /// <summary>
        /// Modify UI to show player is Dead
        /// </summary>
        private Frame GetFrame(int row, int column)
        {
            string FrameName = "Frame" + row + column;
            return (Frame)BattleGrid.FindByName(FrameName);
        }
        /// <summary>
        /// Removing Image from a given position
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void RemoveImage(int row, int column)
        {
            foreach (var child in BattleGrid.Children.Where(child => Grid.GetRow(child) == row && Grid.GetColumn(child) == column))
            {
                child.IsVisible = false;
            }
        }
        /// <summary>
        /// Adding opacity to Image at a given position
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void AddImageOpacity(int row, int column)
        {
            foreach (var child in BattleGrid.Children.Where(child => Grid.GetRow(child) == row && Grid.GetColumn(child) == column))
            {
                child.Opacity = 0.5;
            }
        }
        /// <summary>
        /// Adding Image at a position
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void AddImage(int row, int column, string imageURI)
        {
            Xamarin.Forms.Image Img = new Xamarin.Forms.Image();
            Img.Source = imageURI;
            Grid.SetRow(Img, row);
            Grid.SetColumn(Img, column);
            BattleGrid.Children.Add(Img);
        }
        /// <summary>
        /// Resetting UI elements for current attacker and defender
        /// </summary>
        private void ResetCurrentPlayers()
        {
            if (EngineViewModel.Engine.CurrentAttacker != null)
            {
                Frame AttackerFrame = GetFrame(AttackerPosition[0], AttackerPosition[1]);
                if (AttackerFrame == null)
                {
                    AddImage(SelectedCharacterMap[EngineViewModel.Engine.CurrentAttacker.Guid], 0, CurrentPlayer.ImageURI); //Column is always 0 because move ability is only for characters
                    RemoveImage(AttackerPosition[0], AttackerPosition[1]);
                }
                else
                {
                    AttackerFrame.IsVisible = false;
                }

            }
            if (EngineViewModel.Engine.CurrentDefender != null && EngineViewModel.Engine.CurrentDefender.Alive == true)
            {
                Frame DefenderFrame = GetFrame(DefenderPosition[0], DefenderPosition[1]);
                DefenderFrame.IsVisible = false;
            }
        }

        /// <summary>
        /// Setting the currentDefender position and UI elements
        /// </summary>
        private void SetCurrentDefender()
        {
            if (EngineViewModel.Engine.CurrentDefender.PlayerType == PlayerTypeEnum.Character)
            {
                DefenderPosition[1] = 0;
                DefenderPosition[0] = SelectedCharacterMap[EngineViewModel.Engine.CurrentDefender.Guid];
            }
            if (EngineViewModel.Engine.CurrentDefender.PlayerType == PlayerTypeEnum.Monster)
            {
                DefenderPosition[1] = 5;
                DefenderPosition[0] = SelectedMonsterMap[EngineViewModel.Engine.CurrentDefender.Guid];
            }
            Frame DefenderFrame = GetFrame(DefenderPosition[0], DefenderPosition[1]);
            DefenderFrame.BackgroundColor = Color.CornflowerBlue;
            DefenderFrame.IsVisible = true;
        }

        /// <summary>
        /// Setting the currentAttacker position and UI elements
        /// </summary>
        private void SetCurrentAttacker()
        {
            if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                AttackerPosition[1] = 0;
                AttackerPosition[0] = SelectedCharacterMap[EngineViewModel.Engine.CurrentAttacker.Guid];
            }
            if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Monster)
            {
                AttackerPosition[1] = 5;
                AttackerPosition[0] = SelectedMonsterMap[EngineViewModel.Engine.CurrentAttacker.Guid];
            }
            Frame AttackerFrame = GetFrame(AttackerPosition[0], AttackerPosition[1]);
            AttackerFrame.BackgroundColor = Color.CornflowerBlue;
            AttackerFrame.IsVisible = true;
        }
        #endregion UIOperations

        #region MoveHandlers
        /// <summary>
        /// Move character back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveBack_Clicked(object sender, EventArgs e)
        {
            if (AttackerPosition[1] - 1 < 1)
                return;
            AttackerPosition[1]--;
            AddImage(AttackerPosition[0], AttackerPosition[1], CurrentPlayer.ImageURI);
            RemoveImage(AttackerPosition[0], AttackerPosition[1] + 1);
        }
        /// <summary>
        /// Move character front
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveFront_Clicked(object sender, EventArgs e)
        {
            if (AttackerPosition[1] + 1 > 4)
                return;
            AttackerPosition[1]++;
            AddImage(AttackerPosition[0], AttackerPosition[1], CurrentPlayer.ImageURI);
            RemoveImage(AttackerPosition[0], AttackerPosition[1] - 1);
        }
        /// <summary>
        /// Move character Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUp_Clicked(object sender, EventArgs e)
        {
            if (AttackerPosition[1] == 0 || AttackerPosition[0] - 1 < 0)
                return;
            AttackerPosition[0]--;
            AddImage(AttackerPosition[0], AttackerPosition[1], CurrentPlayer.ImageURI);
            RemoveImage(AttackerPosition[0] + 1, AttackerPosition[1]);
        }
        /// <summary>
        /// Move character down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDown_Clicked(object sender, EventArgs e)
        {
            if (AttackerPosition[1] == 0 || AttackerPosition[0] + 1 > 5)
                return;
            AttackerPosition[0]++;
            AddImage(AttackerPosition[0], AttackerPosition[1], CurrentPlayer.ImageURI);
            RemoveImage(AttackerPosition[0] - 1, AttackerPosition[1]);
        }
        #endregion MoveHandlers

        #region PageHandlers
        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AttackButton_Clicked(object sender, EventArgs e)
        {
            isSpecialAbilityUsedForAttack = false;
            playBattle(isSpecialAbilityUsedForAttack);

        }
        /// <summary>
        /// Special Ability Action
        /// Update Special ability map 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SpecialAbilityButton_Clicked(object sender, EventArgs e)
        {
            isSpecialAbilityUsedForAttack = true;
            SpecialAbilityMap[EngineViewModel.Engine.CurrentAttacker.Guid] = false;
            playBattle(isSpecialAbilityUsedForAttack);
        }
        /// <summary>
        /// Navigate to pick items page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void PickItemsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PickItemsPage()));

            RoundOverDisplay.IsVisible = false;

            GameUIDisplay.IsVisible = true;

        }
        /// <summary>
        /// Close Round Over Display and Show Battle grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            RoundOverDisplay.IsVisible = false;

            GameUIDisplay.IsVisible = true;
        }
        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScorePage()));
        }
        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion PageHandlers

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            ClearMessages();
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.TurnMessage, BattleMessages.Text);

            if (isSpecialAbilityUsedForAttack)
            {
                BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.GetSpecialAbilityMessage(), BattleMessages.Text);
            }
            /*if (EngineViewModel.Engine.BattleScore.RoundCount == 2 && EngineViewModel.ExtraLife == true)
            { 
                BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.GetReincarnatedPlayerMessage(), BattleMessages.Text);
            }*/

            if (!string.IsNullOrEmpty(EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
            }

            //htmlSource.Html = EngineViewModel.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            //htmlSource.Html = EngineViewModel.Engine.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

    }
}