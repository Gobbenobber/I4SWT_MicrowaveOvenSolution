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
        private IButton _startCancelButton;
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
            _startCancelButton = new Button();
            _userInterface = new UserInterface(_powerButton,_timeButton,_startCancelButton,_door,_display,_light,_cookController);
        }
        
        [Test]
        public void PowerPressed_UICallsShowPower50InDisplay()
        {
            _powerButton.Press();
            
            _display.Received().ShowPower(50);
        }

        [Test]
        public void TimePressed_UICallsShowTime1minInDisplay()
        {

            _powerButton.Press();
            _timeButton.Press();

            _display.Received().ShowTime(1,0);
        }

        [Test]
        public void StartCancelPressedAfterSetUp_CookingSequenceStarted()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            Received.InOrder(() =>
            {
                _light.TurnOn();
                _cookController.StartCooking(50,60);
            });

        }

        [Test]
        public void PowerPressed_ThenCancelPressed_UIClearsDisplay()
        {
            _powerButton.Press();

            _startCancelButton.Press();

            _display.Received().Clear();
        }

        [Test]
        public void StartCancelPressedTwiceAfterSetUp_CookingSequenceStartedAndStopped()
        {
            _powerButton.Press();
            
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            Received.InOrder(() =>
            {
                _cookController.Stop();
                _light.TurnOff();
                _display.Clear();
            });

        }
    }
}
