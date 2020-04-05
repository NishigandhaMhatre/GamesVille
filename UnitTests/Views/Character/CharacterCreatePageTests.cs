using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Image = Game.Models.Image;
//using Xamarin.Forms.Mocks;

namespace UnitTests.Views.Character
{
    [TestFixture]
    public class CharacterCreatePageTests : CharacterCreatePage
    {
        App app;
        CharacterCreatePage page;

        public CharacterCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            Xamarin.Forms.Mocks.MockForms.Init();

            //This is App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CharacterCreatePage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void CharacterCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void CharacterCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_Level_OnStepperAttackChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel();
            var ViewModel = new GenericViewModel<CharacterModel>(data);

            page = new CharacterCreatePage(ViewModel);
            double oldLevel = 0.0;
            double newLevel = 1.0;

            var args = new ValueChangedEventArgs(oldLevel, newLevel);

            // Act
            page.Level_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterCreatePage_OnCharacterImageSelected_Default_Should_Pass()
        {
            // Arrange
            var data = new CharacterModel();
            var ViewModel = new GenericViewModel<CharacterModel>(data);

            var args = new SelectedItemChangedEventArgs(new Image() { Url = "Blossum.png" }, 0);

            // Act
            page.OnCharacterImageSelected(null, args);

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
