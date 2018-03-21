using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class MicrowaveIntegrationTest3
    {

        private IDoor _door;
        private ICookController _uut;
        private ILight _light;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private IButton _timeButton;
        private IButton _powerButton;
        private IButton _startCancelButton;
        private IUserInterface _userInterface;

        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _timeButton = new Button();
            _powerButton = new Button();       
            _startCancelButton = new Button();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _uut = new CookController(_timer,_display,_powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uut);

        }

        [Test]
        public void StartCancelPressedAfterSetUp_TurnOnPowerTubeAndTimer()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Received.InOrder(() =>
            {
                _timer.Start(60);
                _powerTube.TurnOn(50);
            });
        }

        [Test]
        public void StartCancelPressedDuringSetUp_DisplayIsCleared()
        {
            _powerButton.Press();
            _startCancelButton.Press();
            _display.Received().Clear();
        }

        [Test]
        public void StartCancelPressedWhileCookinng_TurnOffPowerTubeAndTimer()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            Received.InOrder(() =>
            {
                _timer.Start(60);
                _powerTube.TurnOn(50);
                _powerTube.TurnOff();
                _timer.Stop();
            });
        }
    }
}
