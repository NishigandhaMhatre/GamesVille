using NUnit.Framework;
using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    class CharacterMonsterBaseModelTests
    {
        [TearDown]
        public async Task TearDown()
        {
            await Game.Helpers.DataSetsHelper.WipeData();
        }

        [Test]
        public void CharacterMonsterBaseModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new CharacterMonsterBaseModel<CharacterModel>();

            // Reset

            // Assert
            Assert.AreEqual("This is an Item", result.Name);
        }

        [Test]
        public void CharacterMonsterBaseModel_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new CharacterMonsterBaseModel<CharacterModel>();

            // Reset

            // Assert
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(PlayerTypeEnum.Unknown, result.PlayerType);
            Assert.AreEqual(true, result.Alive);
            Assert.AreEqual(0, result.Order);
            Assert.AreEqual(result.Id, result.Guid);
            Assert.AreEqual(0, result.ListOrder);
            Assert.AreEqual(0, result.Speed);
            Assert.AreEqual(1, result.Level);
            Assert.AreEqual(1, result.CurrentHealth);
            Assert.AreEqual(1, result.MaxHealth);
            Assert.AreEqual(300, result.ExperienceTotal);
            Assert.AreEqual(0, result.Defense);
            Assert.AreEqual(0, result.Attack);
            Assert.AreEqual(null, result.Head);
            Assert.AreEqual(null, result.Feet);
            Assert.AreEqual(null, result.Necklace);
            Assert.AreEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);
        }

        [Test]
        public void CharacterMonsterBaseModel_Set_Get_Default_Should_Pass()
        {
            // Arrange
            var result = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            result.Id = "bogus";
            result.ImageURI = "uri";
            result.PlayerType = PlayerTypeEnum.Monster;
            result.Alive = false;
            result.Order = 100;
            result.Guid = "guid";
            result.ListOrder = 200;
            result.Speed = 300;
            result.Level = 400;
            result.CurrentHealth = 600;
            result.MaxHealth = 700;
            result.ExperienceTotal = 800;
            result.Defense = 900;
            result.Attack = 123;
            result.Head = "head";
            result.Feet = "feet";
            result.Necklace = "necklass";
            result.PrimaryHand = "primaryhand";
            result.OffHand = "offhand";
            result.RightFinger = "rightfinger";
            result.LeftFinger = "leftfinger";

            // Reset

            // Assert
            Assert.AreEqual("bogus", result.Id);
            Assert.AreEqual("uri", result.ImageURI);
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
            Assert.AreEqual(false, result.Alive);
            Assert.AreEqual(100, result.Order);
            Assert.AreEqual("guid", result.Guid);
            Assert.AreEqual(200, result.ListOrder);
            Assert.AreEqual(300, result.Speed);
            Assert.AreEqual(400, result.Level);
            Assert.AreEqual(600, result.CurrentHealth);
            Assert.AreEqual(700, result.MaxHealth);
            Assert.AreEqual(800, result.ExperienceTotal);
            Assert.AreEqual(900, result.Defense);
            Assert.AreEqual(123, result.Attack);
            Assert.AreEqual("head", result.Head);
            Assert.AreEqual("feet", result.Feet);
            Assert.AreEqual("necklass", result.Necklace);
            Assert.AreEqual("primaryhand", result.PrimaryHand);
            Assert.AreEqual("offhand", result.OffHand);
            Assert.AreEqual("rightfinger", result.RightFinger);
            Assert.AreEqual("leftfinger", result.LeftFinger);
        }



        [Test]
        public void CharacterMonsterBaseModel_Update_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.Update(null);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetAttack_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetAttack();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetDefense_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetDefense();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetSpeed_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetSpeed();

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetHealthCurrent_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetCurrentHealthTotal;

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetHealthMax_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetMaxHealthTotal;

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetDamageRollValue_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();
            data.Level = 1;

            // Act
            var result = data.GetDamageRollValue(false);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_TakeDamage_Valid_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.TakeDamage(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_TakeDamage_InValid_Should_Fail()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.TakeDamage(0);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_CauseDeath_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.CauseDeath();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_FormatOutput_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.FormatOutput();

            // Reset

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void CharacterMonsterBaseModel_AddExperience_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.AddExperience(0);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_CalculateExperienceEarned_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.CalculateExperienceEarned(0);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_LevelUp__neg_exp_Should_Fail()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            data.Level = 1;
            data.ExperienceTotal = -1;
            var result = data.LevelUp();

            // Reset
            data.Level = 1; //default value
            data.ExperienceTotal = 300; // default value
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_LevelUp_Should_Fail()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            data.Level = 1;
            data.ExperienceTotal = 300;
            var result = data.LevelUp();

            // Reset
            data.Level = 1; // default value
            data.ExperienceTotal = 300; // default value
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_HighestLevelReached_Should_Fail()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            data.Level = 29;
            data.ExperienceTotal = 355001;
            var result = data.LevelUp();

            // Reset
            data.Level = 1; // default value
            data.ExperienceTotal = 300; // default value
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_Head_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_Feet_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_Necklass_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Necklass);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_PrimaryHand_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.PrimaryHand);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_OffHand_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.OffHand);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_RightFinger_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.RightFinger);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_LeftFinger_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.LeftFinger);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_GetItemByLocation_Unknown_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.GetItemByLocation(ItemLocationEnum.Unknown);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_DropAllItems_Default_Should_Pass()
        {
            var item = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>
            {
                Head = item.Id,
                Necklace = item.Id,
                PrimaryHand = item.Id,
                OffHand = item.Id,
                RightFinger = item.Id,
                LeftFinger = item.Id,
                Feet = item.Id,
            };

            // Act
            var result = data.DropAllItems();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterMonsterBaseModel_AddItem_Unknown_Should_Fail()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Act
            var result = data.AddItem(ItemLocationEnum.Unknown, "bogus");

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void CharacterMonsterBaseModel_AddItem_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();
            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();

            // Act

            // Add the second item, this will return the first item as the one replaced which is null
            var result = data.AddItem(ItemLocationEnum.Head, itemOld.Id);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_AddItem_Default_Replace_Should_Pass()
        {
            // Arrange
            var data = new CharacterMonsterBaseModel<CharacterModel>();
            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, itemOld.Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.AddItem(ItemLocationEnum.Head, itemNew.Id);

            // Reset

            // Assert
            Assert.AreEqual(itemOld.Id, result.Id);
        }


        [Test]
        public async Task CharacterMonsterBaseModel_GetItemBonus_Default_Attack_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });
            _ = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetItemBonus(AttributeEnum.Attack);

            // Reset

            // Assert
            Assert.AreEqual(7654321, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetAttackTotal_Default_Attack_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });


            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetAttackTotal;

            // Reset

            // Assert
            Assert.AreEqual(7654322, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetDefenseTotal_Default_Defense_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Defense, Value = 7000000, Id = "feet" });


            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDefenseTotal;

            // Reset

            // Assert
            Assert.AreEqual(7654322, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetSpeedTotal_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Speed, Value = 7000000, Id = "feet" });


            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetSpeedTotal;

            // Reset

            // Assert
            Assert.AreEqual(7654322, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetDamageRollValue_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Id = "head" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Id = "necklass" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 4000, Id = "OffHand" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 50000, Id = "RightFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 600000, Id = "LeftFinger" });
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 7000000, Id = "feet" });


            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.Head, (await ItemIndexViewModel.Instance.ReadAsync("head")).Id);
            data.AddItem(ItemLocationEnum.Necklass, (await ItemIndexViewModel.Instance.ReadAsync("necklass")).Id);
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);
            data.AddItem(ItemLocationEnum.OffHand, (await ItemIndexViewModel.Instance.ReadAsync("OffHand")).Id);
            data.AddItem(ItemLocationEnum.RightFinger, (await ItemIndexViewModel.Instance.ReadAsync("RightFinger")).Id);
            data.AddItem(ItemLocationEnum.LeftFinger, (await ItemIndexViewModel.Instance.ReadAsync("LeftFinger")).Id);
            data.AddItem(ItemLocationEnum.Feet, (await ItemIndexViewModel.Instance.ReadAsync("feet")).Id);

            Game.Helpers.DiceHelper.EnableRandomValues();
            Game.Helpers.DiceHelper.SetForcedRandomValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageRollValue(false);

            // Reset
            Game.Helpers.DiceHelper.DisableRandomValues();

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetDamageItemBonus_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand", Damage = 1 });

            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);

            Game.Helpers.DiceHelper.EnableRandomValues();
            Game.Helpers.DiceHelper.SetForcedRandomValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageItemBonus;

            // Reset
            Game.Helpers.DiceHelper.DisableRandomValues();

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetDamageItemBonusString_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand", Damage = 1 });

            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);

            Game.Helpers.DiceHelper.EnableRandomValues();
            Game.Helpers.DiceHelper.SetForcedRandomValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageItemBonusString;

            // Reset
            Game.Helpers.DiceHelper.DisableRandomValues();

            // Assert
            Assert.AreEqual("1D 1", result);
        }

        [Test]
        public async Task CharacterMonsterBaseModel_GetDamageTotalString_Default_Speed_Should_Pass()
        {
            // Arrange
            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel { Attribute = AttributeEnum.Attack, Value = 300, Id = "PrimaryHand", Damage = 1 });

            var itemOld = ItemIndexViewModel.Instance.Dataset.FirstOrDefault();
            var itemNew = ItemIndexViewModel.Instance.Dataset.LastOrDefault();

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            data.Level = 1;

            // Add the first item
            data.AddItem(ItemLocationEnum.PrimaryHand, (await ItemIndexViewModel.Instance.ReadAsync("PrimaryHand")).Id);

            Game.Helpers.DiceHelper.EnableRandomValues();
            Game.Helpers.DiceHelper.SetForcedRandomValue(1);

            // Act

            // Add the second item, this will return the first item as the one replaced
            var result = data.GetDamageTotalString;

            // Reset
            Game.Helpers.DiceHelper.DisableRandomValues();

            // Assert
            Assert.AreEqual("1 + 1D 1", result);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocation_Head_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.Head = "Helmet";
            data.RemoveItemFromLocation(ItemLocationEnum.Head);
            var newHead = data.Head;
            // Reset
            data.Head = null;
            // Assert
            Assert.AreEqual(null, newHead);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocation_Feet_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.Feet = "Absorption Shoes";
            data.RemoveItemFromLocation(ItemLocationEnum.Feet);
            var newFeet = data.Feet;
            // Reset
            data.Feet = null;
            // Assert
            Assert.AreEqual(null, newFeet);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocation_LeftFinger_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.LeftFinger = "Ring";
            data.RemoveItemFromLocation(ItemLocationEnum.LeftFinger);
            var newLeftFinger = data.LeftFinger;
            // Reset
            data.LeftFinger = null;
            // Assert
            Assert.AreEqual(null, newLeftFinger);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocatio_RightFinger_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.RightFinger = "Ring";
            data.RemoveItemFromLocation(ItemLocationEnum.RightFinger);
            var newRightFinger = data.RightFinger;
            // Reset
            data.RightFinger = null;
            // Assert
            Assert.AreEqual(null, newRightFinger);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocatio_PrimaryHand_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.PrimaryHand = "Flaming Sword";
            data.RemoveItemFromLocation(ItemLocationEnum.PrimaryHand);
            var newPrimaryHand = data.PrimaryHand;
            // Reset
            data.RightFinger = null;
            // Assert
            Assert.AreEqual(null, newPrimaryHand);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocatio_NeckLass_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.Necklace = "Posinous pearls";
            data.RemoveItemFromLocation(ItemLocationEnum.Necklass);
            var newNecklace = data.Necklace;
            // Reset
            data.Necklace = null;
            // Assert
            Assert.AreEqual(null, newNecklace);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocatio_OffHand_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.OffHand = "Thunder bolt";
            data.RemoveItemFromLocation(ItemLocationEnum.OffHand);
            var newOffHand = data.OffHand;
            // Reset
            data.OffHand = null;
            // Assert
            Assert.AreEqual(null, newOffHand);
        }

        [Test]
        public void CharacterMonsterBaseModel_RemoveItemFromLocatio_default_Should_Pass()
        {
            // Arrange

            var data = new CharacterMonsterBaseModel<CharacterModel>();
            // Act
            data.RemoveItemFromLocation(ItemLocationEnum.Unknown);
            // Reset
       
            // Assert
            Assert.AreEqual(0, (int)ItemLocationEnum.Unknown);
        }

    }
}
