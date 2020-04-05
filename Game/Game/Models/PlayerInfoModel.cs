
namespace Game.Models
{
    /// <summary>
    /// Player for the game.
    /// 
    /// Either Monster or Character
    /// 
    /// Constructor Player to Player used a T in Round
    /// </summary>
    public class PlayerInfoModel : CharacterMonsterBaseModel<PlayerInfoModel>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlayerInfoModel(){}
        
        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(PlayerInfoModel data) 
        {
            
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperienceTotal = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            Attack = data.Attack;
            Defense = data.Defense;
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealthTotal;
            MaxHealth = data.GetMaxHealthTotal;
            SpecialAbility = data.SpecialAbility;
            ISSpecialAbilityNotUsed = data.ISSpecialAbilityNotUsed;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklace = data.Necklace;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            PrimaryHand = data.PrimaryHand;
            OffHand = data.OffHand;
            
        }

        /// <summary>
        /// Create PlayerInfoModel from character
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(CharacterModel data)
        {
            
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperienceTotal = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            Attack = data.Attack;
            Defense = data.Defense;
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealthTotal;
            MaxHealth = data.GetMaxHealthTotal;
            SpecialAbility = data.SpecialAbility;
            ISSpecialAbilityNotUsed = data.ISSpecialAbilityNotUsed;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklace = data.Necklace;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;
            PrimaryHand = data.PrimaryHand;
            OffHand = data.OffHand;

            // Set current experience to be 1 above minimum.
            ExperienceTotal = LevelTableHelper.Instance.LevelDetailsList[Level - 1].Experience + 1;

        }

        /// <summary>
        /// Crate PlayerInfoModel from Monster
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(MonsterModel data)
        {
            
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperienceTotal = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            Attack = data.Attack;
            Defense = data.Defense;
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealthTotal;
            MaxHealth = data.GetMaxHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklace = data.Necklace;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;
            PrimaryHand = data.PrimaryHand;
            OffHand = data.OffHand;

            // Set amount to give to be 1 below max for that level.
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;

        }
    }
}