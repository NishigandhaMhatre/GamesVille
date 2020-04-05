using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Helpers;
using System;
using Game.ViewModels;

namespace Game.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// </summary>
    public class TurnEngine : BaseEngine
    {
        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            var result = Attack(Attacker);

            BattleScore.TurnCount++;

            return result;
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(PlayerInfoModel Attacker)
        {
            // For Attack, Choose Who
            var Target = AttackChoice(Attacker);

            if (Target == null)
            {
                return false;
            }

            // Do Attack
            UseSpecialAbility = RollToUseSpecialAbilityOnTarget();
            TurnAsAttack(Attacker,Target, UseSpecialAbility);

            CurrentAttacker = new PlayerInfoModel(Attacker);
            CurrentDefender = new PlayerInfoModel(Target);

            return true;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            switch (data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();

                case PlayerTypeEnum.Character:
                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectCharacterToAttack()
        {
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // Select first in the list
            var Defender = CharacterList
                .Where(m => m.Alive)
                .OrderBy(m => m.ListOrder).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectMonsterToAttack()
        {
            if (MonsterList == null)
            {
                return null;
            }

            if (MonsterList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 
            var Defender = MonsterList
                .Where(m => m.Alive)
                .OrderBy(m => m.CurrentHealth).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target, bool UseSpecialAbility)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleMessagesModel.ClearMessages();

            //TODO
            BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;
            var AttackScore = 0;

            // Choose who to attack

            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            CurrentAttacker = Attacker;
            CurrentDefender = Target;

            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
               // bool UseSpecialAbility = RollToUseSpecialAbilityOnTarget();
                if(UseSpecialAbility)
                {
                    if (Attacker.ISSpecialAbilityNotUsed == true)
                    {
                        Debug.WriteLine(BattleMessagesModel.GetSpecialAbilityMessage());
                        AttackScore = Attacker.Level + Attacker.GetAttack(UseSpecialAbility);
                    }
                    
                }
               
                UseSpecialAbility = false;
            }

            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                AttackScore = Attacker.Level + Attacker.GetAttack();
                
            }

            // Hack #43
            if (Attacker.AttackWithGoSUItem)
            {
                BattleMessagesModel.SpecialMessage = "Go SU!";
                Debug.WriteLine(BattleMessagesModel.SpecialMessage);
            }

            // Hack #27
            if(Attacker.BrokenItems.Count > 0)
            {
                BattleMessagesModel.ItemsBroken = string.Empty;
                foreach (ItemModel BrokenItem in Attacker.BrokenItems)
                {
                    BattleMessagesModel.ItemsBroken += "Item " + BrokenItem.Name + " broke" + "\n";
                    
                }

                Debug.WriteLine(BattleMessagesModel.ItemsBroken);

                // reset
                Attacker.BrokenItems = new List<ItemModel>();
                
            }

            var DefenseScore = Target.GetDefense() + Target.Level;

            
            // Hackathon
            // Hackathon Scenario 2, Bob alwasys misses
            if (Attacker.Name.Equals("Bob"))
            {
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.TurnMessage = "Bob always Misses";
                Debug.WriteLine(BattleMessagesModel.TurnMessage);
                return true;
            }

            // Hack #47
            var AttributeSumPrimeCheck = CheckIfAttributesSumtoPrime(Attacker);
            BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            if (!AttributeSumPrimeCheck)
            {
                BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);
                
                // Player has died due to Hack #48, i.e., player roll was same as secret
                if(BattleMessagesModel.HitStatus == HitStatusEnum.Unknown)
                {
                    RemoveIfDead(Attacker);
                    BattleMessagesModel.SpecialMessage = "The CIA regrets to inform you that your character died.";
                    Debug.WriteLine(BattleMessagesModel.SpecialMessage);
                    return true;
                }
            }

            Debug.WriteLine(BattleMessagesModel.GetTurnMessage());

            // It's a Miss
            if (BattleMessagesModel.HitStatus == HitStatusEnum.Miss)
            {
                BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial.Replace("\n", Environment.NewLine);
                Debug.WriteLine(BattleMessagesModel.TurnMessage);
                return true;
            }

            // It's a Hit
            if (BattleMessagesModel.HitStatus == HitStatusEnum.Hit)
            {
                
                //Calculate Damage
                BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue(AttributeSumPrimeCheck);

                //Hack #31, When Round Count exceeds 100, Monster Damage power becomes 10x
                if (BattleScore.RoundCount > 100 && Attacker.PlayerType == PlayerTypeEnum.Monster)
                {
                    BattleMessagesModel.DamageAmount *= 10;
                }
                Target.TakeDamage(BattleMessagesModel.DamageAmount);
            }

            BattleMessagesModel.CurrentHealth = Target.CurrentHealth;
            BattleMessagesModel.TurnMessageSpecial = BattleMessagesModel.GetCurrentHealthMessage();

            //Hack #33, Character in the turn Loose its current health when it is Round 13.
            if (BattleScore.RoundCount == 13)
            {
                if (Attacker.PlayerType == PlayerTypeEnum.Character)
                {
                    Attacker.CurrentHealth = 0;
                }
                if (Target.PlayerType == PlayerTypeEnum.Character)
                {
                    Target.CurrentHealth = 0;
                }
                Debug.WriteLine("Its Lucky 13 round, so Character Loose its Health");
            }
            RemoveIfDead(Target);

            // If it is a character apply the experience earned
            CalculateExperience(Attacker, Target);

            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial.Replace("\n", Environment.NewLine);
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
        }

        bool CheckIfAttributesSumtoPrime(PlayerInfoModel Attacker)
        {
            var AttributeSum = Attacker.Speed + Attacker.Defense 
                + Attacker.Attack + Attacker.CurrentHealth 
                + Attacker.MaxHealth;

   
            return IsPrime(AttributeSum);
        }

        /// <summary>
        /// Checks if given number is prime 
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private bool IsPrime(int Num)
        {
            for (var i = 2; i <= Num / 2; i++)
            {
                if(Num%i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        public bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var experienceEarned = Target.CalculateExperienceEarned(BattleMessagesModel.DamageAmount);
                BattleMessagesModel.ExperienceEarned = " Earned " + experienceEarned + " points";

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    BattleMessagesModel.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetMaxHealthTotal;
                    Debug.WriteLine(BattleMessagesModel.LevelUpMessage);
                }

                // Add Experinece to the Score
                BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            return true;
        }

        /// <summary>
        /// If Dead process Targed Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                TargedDied(Target);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public bool TargedDied(PlayerInfoModel Target)
        {
            bool found;
            // Mark Status in output
            BattleMessagesModel.TurnMessageSpecial = " and causes death";

            // Remove target from list...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    CharacterList.Remove(Target);

                    // Add the MonsterModel to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = CharacterList.Remove(CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = MonsterList.Remove(MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";
            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                BattleMessagesModel.TurnMessageSpecial += "\n ItemModel " + ItemModel.Name + " dropped";
            }

            ItemPool.AddRange(myItemList);
            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            BattleMessagesModel.TurnMessageSpecial += DroppedMessage;

            BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);
            //Hack #3, Sets the attaker with a HitValue
            
            if (CurrentAttacker.PlayerType == PlayerTypeEnum.Character && this.CharacterHitValue != 0)
            {
                d20 = this.CharacterHitValue;
            }
            if (CurrentAttacker.PlayerType == PlayerTypeEnum.Monster && this.MonsterHitValue != 0)
            {
                d20 = this.MonsterHitValue;
            }

            // Hack #48 When roll matches secret number, Character should die
            /*
            if (d20 == SecretNumber)
            {
                CurrentAttacker.Alive = false;
                BattleMessagesModel.AttackStatus = " rolls " + d20;
                return HitStatusEnum.Unknown;

            }
            */
            // Hack changes ended

            if (d20 == 1)
            {
                BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";

                // Force Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                return BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {

                BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";

                // Force Hit
                BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
                return BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.DamageAmount = 0;
                return BattleMessagesModel.HitStatus;
            }

            BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";
            // Hit
            BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return BattleMessagesModel.HitStatus;
        }
        /// <summary>
        /// Roll To Use SpecialAbility
        /// </summary>
        /// <returns></returns>
        public bool RollToUseSpecialAbilityOnTarget()
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 < 10)
            {
                //Do not Use Special Ability
                return false;
            }
            //Use Special Ability
            return true;
        }
        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // You decide how to drop monster items, level, etc.

            var NumberToDrop = DiceHelper.RollDice(1, round);

            var myList = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                myList.Add(new ItemModel());
            }
            return myList;
        }
    }
}