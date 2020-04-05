using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class ItemModelTests
    {
        [Test]
        public void ItemModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemModel();
            
            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemModel_CopyConstructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ItemModel();
            var newData = new ItemModel(result);
          
            // Reset

            // Assert
            Assert.AreEqual(result.Name,newData.Name);
            Assert.AreEqual(result.Description,newData.Description);
            Assert.AreEqual(result.Value,newData.Value);
            Assert.AreEqual(result.Attribute,newData.Attribute);
            Assert.AreEqual(result.Location,newData.Location);
            Assert.AreEqual(result.Name,newData.Name);
            Assert.AreEqual(result.Description,newData.Description);
            Assert.AreEqual(result.ImageURI,newData.ImageURI);
            Assert.AreEqual(result.Range,newData.Range);
            Assert.AreEqual(result.Damage,newData.Damage);
        }

        [Test]
        public void ItemModel_Update_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = new ItemModel();
            var data = result.Update(null);
            // Reset

            // Assert
            Assert.IsFalse(data);
        }

        [Test]
        public void ItemModel_Update_Default_Should_Pass()
        {
            // Arrange
            var dataOriginal = new ItemModel();
            dataOriginal.Value = 1;

            var dataNew = new ItemModel();
            dataNew.Value = 2;

            // Act
            var result = dataOriginal.Update(dataNew);

            // Reset

            // Assert 
            Assert.AreEqual(2, dataOriginal.Value);
        }

        [Test]
        public void ItemModel_FormatOuput_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();

            // Act
            var result = data.FormatOutput();

            // Reset

            // Assert 
            Assert.AreEqual("This is an Item , Item Description for Head with Attack+0 , Damage : 0 , Range : 0", result);
        }

    }
}
