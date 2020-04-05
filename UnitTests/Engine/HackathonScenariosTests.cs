using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        AutoBattleEngine AutoBattleEngine;
        BattleEngine BattleEngine;

        [SetUp]
        public void Setup()
        {
            AutoBattleEngine = EngineViewModel.AutoBattleEngine;
            BattleEngine = EngineViewModel.Engine;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert
           

            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            AutoBattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                //ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            AutoBattleEngine.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters


            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, AutoBattleEngine.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, AutoBattleEngine.BattleScore.RoundCount);
        }

        [Test]
        public async Task HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        {
            /* 
             * Scenario Number:  
             *  2
             *  
             * Description: 
             *      Make a Character called Bob
             *      Bob Always Misses
             *      Other Characters Always Hit
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character named Bob
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Named Bob
             *  Test with Character of any other name
             * 
             * Validation:
             *      Verify Enum is Miss
             *  
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                //ExperienceRemaining = 1,
                                Name = "Bob",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    //ExperienceRemaining = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableRandomValues();
            DiceHelper.SetForcedRandomValue(19);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer,false);

            //Reset
            DiceHelper.DisableRandomValues();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, BattleEngine.BattleMessagesModel.HitStatus);
        }
        [Test]
        public async Task HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *      2
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create Character named Mike
             *      Create Monster
             *      Call TurnAsAttack so Mike can attack Monster
             * 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                //ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    //ExperienceRemaining = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableRandomValues();
            DiceHelper.SetForcedRandomValue(20);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer,false);

            //Reset
            DiceHelper.DisableRandomValues();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessagesModel.HitStatus);
        }
        [Test]
        public async Task HackathonScenario_Scenario_3_Monster_Hit_Status_Should_SetToGivenValue()
        {
            /* 
             * Scenario Number:  
             *  3
             *  
             * Description: 
             *      Set Monsters to hit as per given hitStatus value
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for CharacterHitStatus
             *      Change to BaseEngine
             *      Added CharacterHitValue
             *      Change in AboutPage
             *      Added Handlers for Debug Setting
             *      Set CharacterHitValue as per input values
             *                 
             *                 
             * Test Algrorithm:
             *  Create character
             *  Set Monster Hit Status
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with MonsterHitStatus set to 20
             * 
             * Validation:
             *      Verify the AttackStatus, it should have 20
             *  
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 1,
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                Name = "Character",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 2,
                    Level = 1,
                    CurrentHealth = 100,
                    ExperienceTotal = 100,
                    Name = "SetHitMonster",
                });

            BattleEngine.MonsterList.Add(MonsterPlayer);

            BattleEngine.MonsterHitValue = 20;
            //Act
            var result = BattleEngine.TurnAsAttack(MonsterPlayer, CharacterPlayer, false);
            //Reset
            BattleEngine.MonsterHitValue = 0;
            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, BattleEngine.BattleMessagesModel.AttackStatus.Contains("20"));
        }
        [Test]
        public async Task HackathonScenario_Scenario_3_Character_Hit_Status_Should_SetToGivenValue()
        {
            /* 
             * Scenario Number:  
             *  3
             *  
             * Description: 
             *      Set characters to hit as per given hitStatus value
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for CharacterHitStatus
             *      Change to BaseEngine
             *      Added CharacterHitValue
             *      Change in AboutPage
             *      Added Handlers for Debug Setting
             *      Set CharacterHitValue as per input values
             *                 
             * Test Algrorithm:
             *  Create Character
             *  Set Character Hit Status
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with CharacterHitStatus set to 15
             *  
             * 
             * Validation:
             *      Verify that the Attack Status has 15 
             *  
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 2,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                //ExperienceRemaining = 1,
                                Name = "SetHitCharacter",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    //ExperienceRemaining = 1,
                    Name = "Monster",
                });

            BattleEngine.MonsterList.Add(MonsterPlayer);

            BattleEngine.CharacterHitValue = 15;
            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);
            //Reset
            BattleEngine.CharacterHitValue = 0;
            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true,BattleEngine.BattleMessagesModel.AttackStatus.Contains("15"));
            
        }
        [Test]
        public async Task HackathonScenario_Scenario_33_Character_Loose_Health_On_Round13()
        {
            /* 
            * Scenario Number:  
            *      33
            *      
            * Description: 
            *      Make a Character, who loose health in the round 13 and health is set to 0
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Change to Turn Engine
            *      Changed TurnAsAttack method
            *      Check for Round Number 
            * 
            * Test Algrorithm:
            *      Create Character and monster
            *      Set Current Health of character to 400 so he is strong 
            *      Set the current battle round count to 13
            *  
            *      Startup Battle
            *      Call TurnAsAttack with character as attacker
            * 
            * Test Conditions:
            *      Test with round 13 with character as attacker
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify the current attacker health is set to 0
            *  
            */

            //Arrange

            // Set Character Conditions
            BattleEngine.CharacterList.Clear();
            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 1,
                                Level = 1,
                                CurrentHealth = 400,
                                ExperienceTotal = 1,
                                //ExperienceRemaining = 1,
                                Name = "Character",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);
            BattleEngine.MonsterList.Clear();
            // Set Monster Conditions
            BattleEngine.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    //ExperienceRemaining = 1,
                    Name = "Monster",
                });

            BattleEngine.MonsterList.Add(MonsterPlayer);
            BattleEngine.BattleScore.RoundCount = 13;
            
            //Act
            //var result = await AutoBattleEngine.RunAutoBattle();
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);
            //Reset

            BattleEngine.BattleScore.RoundCount = 0;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(0, BattleEngine.CurrentAttacker.CurrentHealth);
        }

        [Test]
        public async Task HackathonScenario_Scenario_43_Item_With_GoSU_get_2X_Value_Should_Output_SpecialMessage()
        {
            /* 
             * Scenario Number:  
             *  43
             *  
             * Description: 
             *      Add an item with desciption as 'Go SU RedHawks'
             *      Add the item to a character before starting battle
             *      ItemBonus for that Item doubles
             *      Battle output shows GoSU! when this character attacks
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to CharacterMonsterBaseModel
             *      Changed GetItemBonus method
             *      Change to TurnEngine
             *      Changed TurnAsAttack method
             *      Changed BattleMessagesModel
             *      Added new attribute called specialMessage to BattleMessagesModel
             *                 
             * Test Algrorithm:
             *  Create Item with description 'Go SU Redhawks'
             *  Add the item to a character
             *  Call GetItemBonus method
             *  Call TurnAsAttack
             * 
             * 
             * Test Conditions:
             *  Test with item with description as needed
             *  Test the character with this item gives double the value from getItemBonus
             *  Test with Battle played and output as required
             *  
             * 
             * Validation:
             *      Verify GetItemBonus is doubled
             *      Verify BattleMessagesModel has SpecialMessage as GoSU!
             *  
             */

            // Arrange

            var SUItem = new ItemModel();
            SUItem.Description = "Go SU RedHawks";
            SUItem.Attribute = AttributeEnum.Attack;
            SUItem.Value = 10;
            SUItem.Location = ItemLocationEnum.Head;

            await ItemIndexViewModel.Instance.CreateAsync(SUItem);

            var testCharacter = new CharacterModel();
            testCharacter.AddItem(ItemLocationEnum.Head, SUItem.Id);

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(testCharacter);

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    Attack = 5,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Act

            var result = testCharacter.GetAttackItemBonus;
            var attackResult = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);

            // Assert

            Assert.AreEqual(20, result);
            Assert.AreEqual("Go SU!", BattleEngine.BattleMessagesModel.SpecialMessage);
        }


        [Test]
        public async Task HackathonScenario_Scenario_43_Item_Without_GoSU_get_1X_Value_Should_Not_Output_SpecialMessage()
        {
            /* 
             * Scenario Number:  
             *  43
             *  
             * Description: 
             *      Add an item with desciption as 'Go SU RedHawks'
             *      Add the item to a character before starting battle
             *      ItemBonus for that Item doubles
             *      Battle output shows GoSU! when this character attacks
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to CharacterMonsterBaseModel
             *      Changed GetItemBonus method
             *      Change to TurnEngine
             *      Changed TurnAsAttack method
             *      Changed BattleMessagesModel
             *      Added new attribute called specialMessage to BattleMessagesModel
             *                 
             * Test Algrorithm:
             *  Create Item with description 'Description'
             *  Add the item to a character
             *  Call GetItemBonus method
             *  Call TurnAsAttack
             * 
             * 
             * Test Conditions:
             *  Test with item with description not as 'Go SU RedHawks'
             *  Test the character with this item gives normal value(1X) from getItemBonus
             *  Test with Battle played and output does not have GoSU!
             *  
             * 
             * Validation:
             *      Verify GetItemBonus is not doubled
             *      Verify BattleMessagesModel has SpecialMessage EMPTY
             *  
             */

            // Arrange

            var SUItem = new ItemModel();
            SUItem.Description = "Description";
            SUItem.Attribute = AttributeEnum.Attack;
            SUItem.Value = 10;
            SUItem.Location = ItemLocationEnum.Head;

            await ItemIndexViewModel.Instance.CreateAsync(SUItem);

            var testCharacter = new CharacterModel();
            testCharacter.AddItem(ItemLocationEnum.Head, SUItem.Id);

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(testCharacter);

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    Attack = 5,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Act

            var result = testCharacter.GetAttackItemBonus;
            var attackResult = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);

            // Assert

            Assert.AreEqual(10, result);
            Assert.IsEmpty(BattleEngine.BattleMessagesModel.SpecialMessage);
        }



        [Test]
        public async Task HackathonScenario_Scenario_32__Sort_order_to_change_round5()

        {
            /* 
             * Scenario Number:  
             *  32
             *  
             * Description: 
             *      Added a parameter round number to the OrderPlayerListByTurnOrder and GetNextPlayerTurn
             *      The parameter take the value from RoundCount variable which is incremented during the battle
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to RoundEngine
             *      Changed OrderPlayerListByTurnOrder method
             *      Change to GetNextPlayerTurn method
             *      Changed Unit test cases in RoundEngine test
             *      Changed BattleMessagesModel
             *      Added new attribute called specialMessage to BattleMessagesModel
             *                 
             * Test Algrorithm:
             *  Pass round number as 5 check the sort order
             *  Pass round number as 1 check the sort order
             * 
             * 
             * 
             * Test Conditions:
             *  Round number as 5 , test if character with lowest speed and health is first in list.
             *  Round number as 1 , test if character/monster with highest speed is first in list
             *  
             *  
             * 
             * Validation:
             *      Verify for Round number as 5 ,character with lowest speed and health is first in list.
             *      Verify for Round number as 1 ,character/monster with highest speed is first in list
             *  
             */

            BattleEngine.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new MonsterModel
                                    {
                                        Speed = 300,
                                        Level = 1,
                                        CurrentHealth = 1,
                                        ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            BattleEngine.CharacterList.Clear();

            BattleEngine.CharacterList.Add(CharacterPlayerMike);
            BattleEngine.CharacterList.Add(CharacterPlayerDoug);
            BattleEngine.CharacterList.Add(CharacterPlayerSue);

            BattleEngine.MonsterList.Clear();
            BattleEngine.MonsterList.Add(MonsterPlayer);

            // Make the List
            BattleEngine.PlayerList = BattleEngine.MakePlayerList();

            // List is Mike, Doug, Monster, Sue




            // Act

            var result1 = BattleEngine.OrderPlayerListByTurnOrder(1);
            var result2 = BattleEngine.OrderPlayerListByTurnOrder(5);
            

            // Assert
            Assert.AreNotEqual(result1[0], result2[0]);
            Assert.AreEqual("Mike", result1[0].Name);
            Assert.AreEqual("Sue", result2[0].Name);

        }


        [Test]
        public async Task HackathonScenario_Scenario_27_Item_ItemUsedUpToLimit_Should_Pass()
        {
            /* 
             * Scenario Number:  
             *  43
             *  
             * Description: 
             *      Add an item with ItemCount to 1
             *      Add the item to a character before starting battle
             *      Start battle by using the Item in the turn
             *      Item dropped when used up to limit
             *      Output shows dropped Item
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to CharacterMonsterBaseModel
             *      Changed GetItemBonus method
             *      Change to TurnEngine
             *      Changed TurnAsAttack method
             *      Changed BattleMessagesModel
             *      Added new attribute called ItemsBroken to BattleMessagesModel
             *                 
             * Test Algrorithm:
             *  Create Item with ItemUseCount = 1
             *  Add the item to a character
             *  Call TurnAsAttack
             *  Check if item was dropped
             *  Check the ItemsBrokenMessage to say 'Item <name> broken'
             * 
             * 
             * Test Conditions:
             *  Test with item with ItemUseCount as 1
             *  Do Turn as attack and test the item to be null after the attack
             *  Test the output is as expected
             *  
             * 
             * Validation:
             *      Verify Item is null 
             *      Verify BattleMessagesModel has ItemsBroken in the right format
             *  
             */

            // Arrange

            var TestItem = new ItemModel() { 
                Name = "Test",
                Attribute = AttributeEnum.Attack,
                Location = ItemLocationEnum.Head,
                ItemUseCount = 1,

            };
            

            await ItemIndexViewModel.Instance.CreateAsync(TestItem);

            var testCharacter = new CharacterModel();
            testCharacter.AddItem(ItemLocationEnum.Head, TestItem.Id);

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(testCharacter);

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    Attack = 5,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Act

            var attackResult = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);

            // Assert

            Assert.IsNull(CharacterPlayer.Head);
            Assert.AreEqual("Item Test broke\n", BattleEngine.BattleMessagesModel.ItemsBroken);
        }

        [Test]
        public async Task HackathonScenario_Scenario_31_Monster_Get_10x_Power_On_Round100()
        {
            /* 
            * Scenario Number:  
            *      31
            *      
            * Description: 
            *      Make a player list
            *      Check that the Moster list powers are modified after 100th round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Change to Turn Engine
            *      Changed TurnAsAttack method
            *      Check for Round Number and increase damage for monster
            *      Change to RoundEngine
            *      Changed AddMonstersToRound
            *      Added check for Round Count and increased the power(Attack, Speed, Defense, Current Health and MaxHealth) of Monsters
            * 
            * Test Algrorithm:
            *      Create Character
            *      Startup Battle with RoundCount set as 100
            *      Call TurnAsAttack with Monster as attacker
            *      Monsters are added by battle Engine 
            *      Collect all properties of Monster for Round 100
            *  
            * 
            * Test Conditions:
            *      Test all properties of Monster, by calling newRound 101 
            * 
            * Validation:
            *      Verify NewRound is true
            *      Verify TurnsAsAttack is true
            *      Verify the Monster properties are 10x buffed
            *  
            */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 1,
                                Level = 1,
                                CurrentHealth = 400,
                                ExperienceTotal = 1,
                                //ExperienceRemaining = 1,
                                Attack = 4,


                                Name = "Character",
                            });

            var MonsterPlayer = new PlayerInfoModel(
                         new MonsterModel
                         {
                             Speed = 1,
                             Level = 1,
                             CurrentHealth = 400,
                             ExperienceTotal = 1,
                                //ExperienceRemaining = 1,
                                Attack = 4,


                             Name = "Character",
                         });
            BattleEngine.CharacterList.Clear();
            BattleEngine.MonsterList.Clear();
            BattleEngine.CharacterList.Add(CharacterPlayer);
            BattleEngine.MonsterList.Add(MonsterPlayer);

            // Set Monster Conditions, Monsters are added directly by engine
            BattleEngine.MaxNumberPartyMonsters = 1;

            // Set Round Count for Battle
            BattleEngine.BattleScore.RoundCount = 99;

            var round100Result = BattleEngine.NewRound();
            var result100 = BattleEngine.TurnAsAttack(BattleEngine.MonsterList[0], BattleEngine.CharacterList[0], false);

            int round100CurrentHealth = BattleEngine.MonsterList[0].CurrentHealth;
            int round100MaxHealth = BattleEngine.MonsterList[0].MaxHealth;
            int round100Attack = BattleEngine.MonsterList[0].Attack;
            int round100Speed = BattleEngine.MonsterList[0].Speed;
            int round100Defense = BattleEngine.MonsterList[0].Defense;
            int round100Damage = BattleEngine.BattleMessagesModel.DamageAmount;
            BattleEngine.BattleScore.RoundCount++;

            //Act
            var round101Result = BattleEngine.NewRound();
            var result101 = BattleEngine.TurnAsAttack(BattleEngine.MonsterList[0], BattleEngine.CharacterList[0], false);

            //Assert
            Assert.AreEqual(true, round101Result);
            Assert.AreEqual(true, round101Result);

            Assert.AreEqual(10 * round100CurrentHealth, BattleEngine.MonsterList[0].CurrentHealth);
            Assert.AreEqual(10 * round100MaxHealth, BattleEngine.MonsterList[0].MaxHealth);
            Assert.AreEqual(10 * round100Attack, BattleEngine.MonsterList[0].Attack);
            Assert.AreEqual(10 * round100Speed, BattleEngine.MonsterList[0].Speed);
            Assert.AreEqual(10 * round100Defense, BattleEngine.MonsterList[0].Defense);
            Assert.AreNotEqual(10 * round100Damage, BattleEngine.BattleMessagesModel.DamageAmount);
        }

        [Test]
        public async Task HackathonScenario_Scenario_47_Prime_AttributeTotal_Should_Cause_Max_Damage()
        {
            /* 
             * Scenario Number:  
             *  47
             *  
             * Description: 
             *      Add Character whose attribute sum is a prime number
             *      Character will always hit in such case
             *      Character causes maximum damage
             *      
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to CharacterMonsterBaseModel
             *      Changed GetDamageRollValue method
             *      Change to TurnEngine
             *      Changed TurnAsAttack method
             *      Added helper methods to check for prime in TurnEngine
             *      Added new character to default data that sums to prime
             *                 
             * Test Algrorithm:
             *  Create Character with attributes total to a prime number
             *  Start battle by calling turn as attack
             *  Check BattleMessageModel's HitStatus
             *  Check BattleMessagesModel.DamageAmount
             * 
             * 
             * Test Conditions:
             *  Test with Character with attributes total to a prime number
             *  Do Turn as attack and BattleMessageModel's HitStatus and DamageAmount
             *  Test the output is as expected
             *  
             * 
             * Validation:
             *      Verify Hit
             *      Verify DamageAmount on BattleMessagesModel 
             *  
             */
            var TestItem = new ItemModel()
            {
                Name = "Test",
                Attribute = AttributeEnum.Attack,
                Location = ItemLocationEnum.PrimaryHand,
                Damage = 10

            };

            await ItemIndexViewModel.Instance.CreateAsync(TestItem);

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(new CharacterModel()
            {
                Attack = 10,
                Defense = 0,
                MaxHealth = 30,
                CurrentHealth = 30,
                Speed = 18,
                Level = 2,
                PrimaryHand = TestItem.Id
            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    Attack = 5,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Act

            var attackResult = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessagesModel.HitStatus);
            Assert.AreEqual(11, BattleEngine.BattleMessagesModel.DamageAmount);
        }
        
        //[Test]
        //public void HackathonScenario_Scenario_48_AttackRoll_Equal_To_SecretNumber_Character_Dies()
        //{
        //    /* 
        //     * Scenario Number:  
        //     *  48
        //     *  
        //     * Description: 
        //     *      Add Character and monster and start battle
        //     *      Generate secret number before round starts
        //     *      Make attack roll same as secret number
        //     *      Character dies and outputs the appropriate message
        //     *      
        //     * 
        //     * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
        //     *      Change to BaseEngine -- added secret number
        //     *      Change to RoundEngine
        //     *      Added GenerateSecretNumber method
        //     *      Change to TurnEngine
        //     *      Changed TurnAsAttack method
        //     *      Changed RollToHitTarget method
        //     *                 
        //     * Test Algrorithm:
        //     *  Start battle 
        //     *  Call NewRound
        //     *  Set SecretNumber to 5
        //     *  Set Character CharacterHitValue to 5
        //     *  Call TurnAsAttack
        //     *  Check HitStatus to be Unknown
        //     *  Check if character died
        //     *  
        //     * 
        //     * 
        //     * Test Conditions:
        //     *  Test with secret number and characterHitValue equal
        //     *  Test if character died and output message is as expected.
        //     *  
        //     * 
        //     * Validation:
        //     *      Verify Character alive status
        //     *      Verify SpecialMessage on BattleMessagesModel to be "The CIA regrets to inform you that your character died."
        //     *      Verify HitStatus is Unknown
        //     */
            

            
        //    BattleEngine.MaxNumberPartyCharacters = 1;

        //    var CharacterPlayer = new PlayerInfoModel(new CharacterModel()
        //    {
        //        Attack = 10,
        //        Defense = 0,
        //        MaxHealth = 20,
        //        CurrentHealth = 20,
        //        Speed = 15,
        //        Level = 1,
                
        //    });

        //    BattleEngine.CharacterList.Add(CharacterPlayer);

        //    // Set Monster Conditions

        //    // Add a monster to attack
        //    BattleEngine.MaxNumberPartyCharacters = 1;

        //    var MonsterPlayer = new PlayerInfoModel(
        //        new MonsterModel
        //        {
        //            Speed = 1,
        //            Level = 1,
        //            Attack = 5,
        //            CurrentHealth = 1,
        //            ExperienceTotal = 1,
        //            Name = "Monster",
        //        });

        //    BattleEngine.CharacterList.Add(MonsterPlayer);

        //    // Act
        //    var roundResult = BattleEngine.NewRound();
        //    var secretNumber = BattleEngine.SecretNumber;
        //    BattleEngine.CharacterHitValue = secretNumber;

        //    var attackResult = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer, false);

        //    // Assert
        //    Assert.AreEqual(HitStatusEnum.Unknown, BattleEngine.BattleMessagesModel.HitStatus);
        //    Assert.IsFalse(BattleEngine.CurrentAttacker.Alive);
        //    Assert.AreEqual("The CIA regrets to inform you that your character died.", BattleEngine.BattleMessagesModel.SpecialMessage);
        //}
    
        [Test]
        public async Task HackathonScenario_Scenario_30_First_Character_In_PlayersList_Get_2x()

        {
            /* 
             * Scenario Number:  
             *  32
             *  
             * Description: 
             *      Added check to the OrderPlayerListByTurnOrder
             *      Checks for First Player in the List, if it is character then we do 2x
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to RoundEngine
             *      Changed OrderPlayerListByTurnOrder method
             *      Added check for first player in the list
             *                 
             * Test Algrorithm:
             *  Add characters and Monsters to the PlayerList
             *  Sort the List based on List Order
             *  Find the First player in the list and do 2x
             * 
             * 
             * 
             * Test Conditions:
             *  Pass 2x Speed, Attack and Defense when first player is character
             * 
             * Validation:
             *      Verify the first character Speed, Attack and Defense. Check for 2x.
             *  
             */

            BattleEngine.MonsterList.Clear();

            // Arrange
            var CharacterPlayerBlossum = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Blossum",
                                            Attack = 5,
                                            Defense = 5,
                                        });

            var CharacterPlayerButterCup = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "ButterCup",
                                            Attack = 5,
                                            Defense = 5,
                                        });  

            var CharacterPlayerBubbles = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Bubbles",
                                            Attack = 5,
                                            Defense = 5,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new MonsterModel
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrentHealth = 1,
                                        ExperienceTotal = 1,
                                        Name = "Monster",
                                        Attack = 5,
                                        Defense = 5,
                                    });

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            BattleEngine.CharacterList.Clear();

            BattleEngine.CharacterList.Add(CharacterPlayerBlossum);
            BattleEngine.CharacterList.Add(CharacterPlayerButterCup);
            BattleEngine.CharacterList.Add(CharacterPlayerBubbles);

            BattleEngine.MonsterList.Clear();
            BattleEngine.MonsterList.Add(MonsterPlayer);

            // Make the List
            BattleEngine.PlayerList = BattleEngine.MakePlayerList();


            // Act

            var result = BattleEngine.OrderPlayerListByTurnOrder(1);

            // Assert
            Assert.AreEqual(404, result[0].Speed);
            Assert.AreEqual(10, result[0].Attack);
            Assert.AreEqual(10, result[0].Defense);

        }

        [Test]
        public async Task HackathonScenario_Scenario_19_Iffeelgood_Charcters_attack_D20_monster_reduces_d20()
        {
            /* 
             * Scenario Number:  
             *  19
             *  
             * Description: 
             *      When I feeel good switch is toggled character attack increases by 20
             *      When I feeel good switch is toggled character attack reduces by 20
             *      
             *      
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to AboutPage.xaml, AboutPage.xaml.cs
             *      Changed AutoBattleEngine
             *      Change to BattleEngineViewModel
             *      Change to BattlePage.xaml.cs
             *              
             *                 
             * Test Algrorithm:
             *  Create Switch in the About page
             *  Added Subscriber in battleengine view model
             *  Update I feel good on toggle
             *  Update attack value for charcter and monster based on I feel ggod
             *  In autobattle add roll die to use the feature
             * 
             * 
             * Test Conditions:
             *  Pass I feel good as true
             *  Check the updated values
             *  
             *  
             * 
             * Validation:
             *      Verify updated values 
             *  
             */
            //Arrange
            AutoBattleEngine auto = new AutoBattleEngine();

            var CharacterPlayerMike = new PlayerInfoModel(
                                       new CharacterModel
                                       {
                                           Speed = 200,
                                           Level = 1,
                                           CurrentHealth = 1,
                                           ExperienceTotal = 1,
                                           Name = "Mike",
                                           ListOrder = 1,
                                           Attack = 20,
                                       });
            auto.CharacterList.Add(CharacterPlayerMike);
            var MonsterPlayer = new PlayerInfoModel(
                                   new MonsterModel
                                   {
                                       Speed = 300,
                                       Level = 1,
                                       CurrentHealth = 1,
                                       ExperienceTotal = 1,
                                       Name = "Monster",
                                       ListOrder = 4,
                                       Attack = 10,
                                   });
            auto.MonsterList.Add(MonsterPlayer);

            // Act


            auto.CreateCharacterParty(true);



            // Assert
            Assert.AreEqual(40, auto.CharacterList[0].Attack);
            Assert.AreEqual(0, auto.MonsterList[0].Attack);

        }
    }
}