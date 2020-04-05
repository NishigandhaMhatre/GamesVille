using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;

namespace UnitTests.Engine
{
    [TestFixture]
    public class AutoBattleEngineTests
    {
        AutoBattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new AutoBattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_GetScoreObject_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.GetScoreObject();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

       [Test]
        public void AutoBattleEngine_RunAutoBattle_Default_Should_Pass()
        {
            //Arrange

            DiceHelper.EnableRandomValues();
            DiceHelper.SetForcedRandomValue(3);

            //Act
            var result = Engine.RunAutoBattle();

            //Reset
            DiceHelper.DisableRandomValues();

            //Assert
            Assert.IsNotNull(result);
        }

       [Test]
        public void AutoBattleEngine_RunAutoBattle_Monsters_1_Should_Pass()
        {
            //Arrange

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            Engine.MaxNumberPartyMonsters = 1;
            Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            Engine.CharacterList.Add(CharacterPlayerMike);

            //Act
            var result = Engine.RunAutoBattle();

            //Reset
            Engine.CharacterList.Clear();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_RunAutoBattle_Character_Reincarnate_in_Round2_should_Pass()
        {
            //Arrange

           
            //Act
            var result = Engine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, Engine.WasReincarnated);
        }

        [Test]
        public void AutoBattleEngine_RunAutoBattle_Character_UsedSpecialAblity_Pass()
        {
            //Arrange

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });
            var MonsterPlayer = new PlayerInfoModel(
                          new CharacterModel
                          {
                              Speed = -1,
                              Level = 10,
                              CurrentHealth = 11,
                              ExperienceTotal = 1,
                              Name = "Monster",
                              ListOrder = 1,
                          });

            Engine.CharacterList.Add(CharacterPlayerMike);
            Engine.MonsterList.Add(MonsterPlayer);
            Engine.UseSpecialAbility = true;


            //Act
            // Have dice rull 19
            DiceHelper.EnableRandomValues();
            DiceHelper.SetForcedRandomValue(19);
            var result = Engine.Attack(CharacterPlayerMike);

            //Reset
            DiceHelper.DisableRandomValues();
            //Assert
            Assert.AreEqual(false, CharacterPlayerMike.ISSpecialAbilityNotUsed);
        }
    }
}