using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class MicrowaveIntegrationTest1
    {

        private IDoor _door;
        private ICookController _cookController;
        private ILight _light;
        private IDisplay _display;
        private IButton _timeButton;
        private IButton _powerButton;
        private IButton _cancelButton;
        private IUserInterface _userInterface;
        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _cookController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _timeButton = new Button();
            _powerButton = new Button();
            _cancelButton = new Button();
            _userInterface = new UserInterface(_powerButton,_timeButton,_cancelButton,_door,_display,_light,_cookController);
        }

        [Test]
        public void PowerPressed_UICallsShowPower50InDisplay()
        {
            _powerButton.Press();
            
            _display.Received().ShowPower(50);
        }

        [Test]
        public void PowerPressedTwice_UICallsShowPower100InDisplay()
        {
            _powerButton.Press();
            _powerButton.Press();
            _display.Received().ShowPower(100);
        }

        [Test]
        public void PowerPressed_ThenCancelPressed_UIClearsDisplay()
        {
            _powerButton.Press();

            _cancelButton.Press();

            _display.Received().Clear();
        }
    }
}
