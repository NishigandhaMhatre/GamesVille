using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    [TestFixture]
    class ItemLocationPickerConverterTests
    {
        [Test]
        public void ItemLocationPickerConverter_Convert_Invalid_Should_Fail()
        {
            // Arrange
            var ItemLocationConverter = new ItemLocationPickerConverter();

            // Act
            var result = ItemLocationConverter.Convert("0", null, null, null);

            // Assert
            Assert.AreEqual("None", result);
        }

        [Test]
        public async Task ItemLocationPickerConverter_Convert_valid_Should_Pass()
        {
            // Arrange
            var ItemLocationConverter = new ItemLocationPickerConverter();
            var ViewModel = ItemIndexViewModel.Instance;
            var dataTest = new ItemModel { Name = "test" };
            await ViewModel.CreateAsync(dataTest);

            // Act
            var result = ItemLocationConverter.Convert(dataTest.Id, null, null, null);

            // Assert
            Assert.AreEqual(dataTest.Name, result);
        }

        [Test]
        public void ItemLocationPickerConverter_ConvertBack_Should_Pass()
        {
            // Arrange
            var ItemLocationConverter = new ItemLocationPickerConverter();
            

            // Act
            var result = ItemLocationConverter.ConvertBack(null, null, null, null);

            // Assert
            Assert.IsNull(result);
        }
    }
}
