using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    class ImageModelTest
    {
        [Test]
        public void ImageModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new Image();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void ImageModel_getURL_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new Image();
            var data = result.Url;
            // Reset

            // Assert 
            Assert.IsNull(data);
        }

        [Test]
        public void ImageModel_setURL_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new Image();
            result.Url = "new url";
            var data = result.Url;
            // Reset
            result.Url = null;
            // Assert 
            Assert.AreEqual("new url",data);
        }
    }
}
