using Game.Services;
using SQLite;
using Game.Helpers;

namespace Game.Models
{
    public class CharacterModel : CharacterMonsterBaseModel<CharacterModel>
    {
        


        // Items to be added

        /// <summary>
        /// Default CharacterModel
        /// Initialize Values
        /// </summary>

        public CharacterModel()
        {
            this.PlayerType = PlayerTypeEnum.Character;
            ImageURI = ItemService.DefaultImageURI;
            this.Name = "This is a Character";
            this.Description = "Character Description";
            this.ImageURI = "default_character.png";
            this.Level = 1;
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;

        }

        /// <summary>
        /// Constructor to create character based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public CharacterModel(CharacterModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update character
        /// </summary>
        /// <param name="newdata"></param>
        public override bool Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return false;
            }
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            SpecialAbility = newData.SpecialAbility;
            Alive = newData.Alive;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ExperienceRemaining = newData.ExperienceRemaining;
            Speed = newData.Speed;
            Attack = newData.Attack;
            Defense = newData.Defense;
            CurrentHealth = newData.CurrentHealth;
            MaxHealth = newData.MaxHealth;
            Head = newData.Head;
            Necklace = newData.Necklace;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;

            return true;
        }

        [Ignore]
        // Return the Attack with SpecialAbility Bonus
        public int GetAttackSpecialAbilityBonus { get { return GetSpecialAbilityBonus(this.SpecialAbility); } }

        // Return the specialAbility value
        public int GetSpecialAbilityBonus(SpecialAbilityEnum specialAbilityEnum)
        {
            return (int)specialAbilityEnum;
        }
        /// <summary>
        /// Return the Total Attack Value
        /// </summary>
        /// <returns></returns>
        public override int GetAttack(bool SpecialAbilityToBeUsedInAttack)
        {
            AttackWithGoSUItem = false;
            // Base Attack
            var myReturn = Attack;

            // Attack Bonus from Level
            myReturn += GetAttackLevelBonus;

            
            if (SpecialAbilityToBeUsedInAttack && ISSpecialAbilityNotUsed)
            {
                // Get Attack bonus from Special Ability
                myReturn += GetAttackSpecialAbilityBonus;
                ISSpecialAbilityNotUsed = false;

                return myReturn;
            }

            // Get Attack bonus from Items
            myReturn += GetAttackItemBonus;

            return myReturn;

        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public override string FormatOutput()
        {
            var myReturn = Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Total Experience : " + ExperienceTotal;
            myReturn += " , Damage : " + GetDamageTotalString;

            return myReturn;
        }
    }
}
