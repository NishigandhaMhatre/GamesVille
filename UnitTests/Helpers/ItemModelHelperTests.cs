using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Helpers;
using Game.ViewModels;
using Game.Models;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    [TestFixture]
    class ItemModelHelperTests
    {
        [Test]
        public void ItemModelHelper_GetItemModelFromGuid_Null_Should_ReturnNull()
        {
            // act
            var result = ItemModelHelper.GetItemModelFromGuid(null);

            // assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ItemModelHelper_GetItemModelFromGuid_Valid_Should_Pass()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;
            var dataTest = new ItemModel { Name = "test" };
            await ViewModel.CreateAsync(dataTest);

            // act
            var result = ItemModelHelper.GetItemModelFromGuid(dataTest.Id);

            // assert
            Assert.AreEqual(dataTest, result);
        }

        [Test]
        public void ItemModelHelper_GetItemModelNameFromGuid_Null_Should_ReturnNull()
        {
            // act
            var result = ItemModelHelper.GetItemModelNameFromGuid(null);

            // assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ItemModelHelper_GetItemModelNameFromGuid_IdDoesNotExist_Should_ReturnNull()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;
            var dataTest = new ItemModel { Name = "test" };
            await ViewModel.CreateAsync(dataTest);

            // act
            var result = ItemModelHelper.GetItemModelNameFromGuid("abcd");

            // assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ItemModelHelper_GetItemModelNameFromGuid_ValidId_Should_Pass()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;
            var dataTest = new ItemModel { Name = "test" };
            await ViewModel.CreateAsync(dataTest);

            // act
            var result = ItemModelHelper.GetItemModelNameFromGuid(dataTest.Id);

            // assert
            Assert.AreEqual(dataTest.Name, result);
        }

        [Test]
        public void ItemModelHelper_GetHeadItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetHeadItemList;

            //Assert
            Assert.Greater(result.Count, 0);
            
        }

        [Test]
        public void ItemModelHelper_GetNecklaceItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetNecklaceItemList;

            //Assert
            Assert.Greater(result.Count, 0);

        }

        [Test]
        public void ItemModelHelper_GetPrimaryHandItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetPrimaryHandItemList;

            //Assert
            Assert.Greater(result.Count, 0);

        }

        [Test]
        public void ItemModelHelper_GetOffHandItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetOffHandItemList;

            //Assert
            Assert.Greater(result.Count, 0);

        }

        [Test]
        public void ItemModelHelper_GetRightFingerItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetRightFingerItemList;

            //Assert
            Assert.Greater(result.Count, 0);

        }

        [Test]
        public void ItemModelHelper_GetLeftFingerItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetLeftFingerItemList;

            //Assert
            Assert.Greater(result.Count, 0);

        }

        [Test]
        public void ItemModelHelper_GetFeetItemList_Returns_More_Than_Zero()
        {
            // Arrange
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            var result = ItemModelHelper.GetFeetItemList;

            //Assert
            Assert.Greater(result.Count, 0);

        }
    }
}
