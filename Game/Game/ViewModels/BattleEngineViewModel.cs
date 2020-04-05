using Game.Models;
using Game.Views;
using System;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using Game.Services;
using System.Threading.Tasks;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class BattleEngineViewModel
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel instance;
        private static readonly object syncRoot = new Object();
        public List<CharacterModel> SelectedCharacters = new List<CharacterModel>();
        public int AvgCharacterLevel = 1;
        public List<MonsterModel> SelectedMonsters = new List<MonsterModel>();

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new BattleEngineViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton


        /// <summary>
        /// The Battle Engine
        /// </summary>
        public Engine.BattleEngine Engine = new Engine.BattleEngine();
        public bool IFeelGood { get; set; } = false;
        public bool ExtraLife { get; set; } = false;
        /// <summary>
        /// Auto Battle Engine (used for scneario testing)
        /// </summary>
        public Engine.AutoBattleEngine AutoBattleEngine = new Engine.AutoBattleEngine();

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
        {
            // Register the Character pick up message
            MessagingCenter.Subscribe<PickCharactersPage, List<CharacterModel>>(this, "PickCharacters", async (obj, data) =>
            {
                await PickCharactersAsync(data as List<CharacterModel>);
            });

            MessagingCenter.Subscribe<AboutPage, bool>(this, "IFeelGood", async (obj, data) =>
            {
                IFeelGood = true;
            });
            MessagingCenter.Subscribe<AboutPage, bool>(this, "ExtraLife", async (obj, data) =>
            {
                ExtraLife = true;
            });

        }
        

        
        #endregion Constructor

        /// <summary>
        /// Registering the selected characters
        /// </summary>
        /// <param name="SelectedCharacterList"></param>
        /// <returns></returns>
        private async Task<bool> PickCharactersAsync(List<CharacterModel> SelectedCharacterList)
        {
            SelectedCharacters.Clear();
            foreach (CharacterModel Character in SelectedCharacterList)
            {
                SelectedCharacters.Add(Character);
                
            }
            return await Task.FromResult(true);
        }
    }
}