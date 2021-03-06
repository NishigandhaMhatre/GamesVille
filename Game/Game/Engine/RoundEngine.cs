﻿using System;
using System.Collections.Generic;
using System.Linq;
using Game.Helpers;
using Game.Models;
using Game.Services;

namespace Game.Engine
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : TurnEngine
    {
        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        /// <returns></returns>
        private bool ClearLists()
        {
            ItemPool.Clear();
            MonsterList.Clear();
            return true;
        }

        // Call to make a new set of monsters...
        public bool NewRound()
        {
            // End the existing round
            EndRound();

            // Hack #48
            // GenerateSecretNumber();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Update Score for the RoundCount
            BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Hack #48
        /// Gets a number from roll dice of D20
        /// </summary>
        public void GenerateSecretNumber()
        {
            SecretNumber = DiceHelper.RollDice(1, 20);
            
            
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones...
            * If you have crudi monsters, then pick from the list

            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            */
        /// </summary>
        /// <returns></returns>
        public int AddMonstersToRound()
        {
            List<MonsterModel> SelectedMonsterList = DefaultData.LoadData(new MonsterModel());
            for (int i = 0; i < MaxNumberPartyMonsters; i++)
            {
                PlayerInfoModel CurrentMonster = new PlayerInfoModel(SelectedMonsterList[i]);
                CurrentMonster.ScaleLevel(GetAverageCharacterLevel());
                MonsterList.Add(CurrentMonster);
            }

            //Hack #31, When Round Count exceeds 100, Monster power becomes 10x
            if (BattleScore.RoundCount > 100)
            {
                foreach (PlayerInfoModel Monster in MonsterList)
                {
                    Monster.Attack = 10 * Monster.Attack;
                    Monster.Speed = 10 * Monster.Speed;
                    Monster.Defense = 10 * Monster.Defense;
                    Monster.CurrentHealth = 10 * Monster.CurrentHealth;
                    Monster.MaxHealth = 10 * Monster.MaxHealth;
                }
            }
            return MonsterList.Count;
        }

        /// <summary>
        /// Calculates the average level of the characters
        /// </summary>
        /// <returns></returns>
        public int GetAverageCharacterLevel()
        {
            int TotalLevel = 0;
            foreach (PlayerInfoModel Character in CharacterList)
            {
                TotalLevel += Character.Level;
            }
            return (int)Math.Ceiling((double)TotalLevel / (double)CharacterList.Count);
        }
        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public void PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public RoundEnum RoundNextTurn()
        {
            int roundNumber = BattleScore.RoundCount;
            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            // Decide Who gets next turn
            // Remember who just went...
            PlayerCurrent = GetNextPlayerTurn(roundNumber);

            // Do the turn....
            TakeTurn(PlayerCurrent);

            RoundStateEnum = RoundEnum.NextTurn;

            return RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerTurn(int roundNumber)
        {
            // Recalculate Order
            OrderPlayerListByTurnOrder(roundNumber);

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public List<PlayerInfoModel> OrderPlayerListByTurnOrder(int roundNumber)
        {
            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before MonsterModel (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            // Work with the Class variable PlayerList
            PlayerList = MakePlayerList();

            if (roundNumber == 5)
            {
                PlayerList = PlayerList.OrderBy(a => a.PlayerType)
                 .ThenBy(a => a.CurrentHealth)
                 .ThenBy(a => a.Speed)
                 .ToList();
                return PlayerList;
            }


            PlayerList = PlayerList.OrderByDescending(a => a.GetSpeed())
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperienceTotal)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();

            // Hack #30, Character Volunteering first is buffed by 2x
            if (PlayerList[0].PlayerType == PlayerTypeEnum.Character)
            {
                PlayerInfoModel Character = PlayerList[0];
                PlayerList.Remove(Character);
                PlayerInfoModel NewCharacter = new PlayerInfoModel(Character);
                NewCharacter.Speed = 2 * Character.Speed;
                NewCharacter.Defense = 2 * Character.Defense;
                NewCharacter.Attack = 2 * Character.Attack;
                PlayerList.Insert(0, NewCharacter);
            }
            return PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            PlayerList.Clear();

            // Remeber the Insert order, used for Sorting
            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            return PlayerList;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (PlayerList.Count ==0)
            {
                return null;
            }

            // No current player, so set the first one
            if (PlayerCurrent == null)
            {
                return PlayerList.FirstOrDefault();
            }

            // Find current player in the list
            var index = PlayerList.FindIndex(m => m.Guid.Equals(PlayerCurrent.Guid));

            // If at the end of the list, return the first element
            if (index == PlayerList.Count()-1)
            {
                return PlayerList.FirstOrDefault();
            }

            // Return the next element
            return PlayerList[index + 1];

            //// Else go and pick the next player in the list...
            //for (var i = 0; i < PlayerCount; i++)
            //{
            //    // Look for current Player in the list
            //    if (PlayerList[i].Guid.Equals(PlayerCurrent.Guid))
            //    {
            //        if (i < PlayerList.Count() - 1) // 0 based...
            //        {
            //            return PlayerList[i + 1];
            //        }
            //        else
            //        {
            //            // Return the first in the list...
            //            return PlayerList.FirstOrDefault();
            //        }
            //    }
            //}

//            return null;
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public bool PickupItemsFromPool(PlayerInfoModel character)
        {
            // Have the character, walk the items in the pool, and decide if any are better than current one.

            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);

            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            var myList = ItemPool.Where(a => a.Location == setLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return false;
            }

            var CharacterItem = character.GetItemByLocation(setLocation);
            if (CharacterItem == null)
            {
                // If no ItemModel in the slot then put on the first in the list
                character.AddItem(setLocation, myList.FirstOrDefault().Id);
                return true;
            }

            foreach (var PoolItem in myList)
            {
                if (PoolItem.Value > CharacterItem.Value)
                {
                    // Put on the new ItemModel, which drops the one back to the pool
                    var droppedItem = character.AddItem(setLocation, PoolItem.Id);

                    // Remove the ItemModel just put on from the pool
                    ItemPool.Remove(PoolItem);

                    if (droppedItem != null)
                    {
                        // Add the dropped ItemModel to the pool
                        ItemPool.Add(droppedItem);
                    }
                }
            }

            return true;
        }
    }
}