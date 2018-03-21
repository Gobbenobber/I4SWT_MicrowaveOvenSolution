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
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    class MicrowaveIntegrationTest5
    {
        private IOutput _output;
        private IDoor _door;
        private CookController _cookController;
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
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;
        }

        [Test]
        public void StartCancelPressedAfterSetup_OutputLineIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Received.InOrder(() =>
            {
                _output.OutputLine("Display shows: 50 W");
                _output.OutputLine("Display shows: 01:00");
                _output.OutputLine("PowerTube works with 50 %");
            });
        }

        [Test]
        public void StartCancelPressedWhileCooking_OutputLineIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            Received.InOrder(() =>
            {
                _output.OutputLine("Display shows: 50 W");
                _output.OutputLine("Display shows: 01:00");
                _output.OutputLine("PowerTube works with 50 %");
                _output.OutputLine("PowerTube turned off");
                _output.OutputLine("Light is turned off");
                _output.OutputLine("Display cleared");
            });
        }

        /// <summary>
        /// This test also discovered an error in Timer.cs, line 46 (TimeRemaining must be -1, not -1000)
        /// </summary>

        [Test]
        public void StartCancelPressedAfterSetup_OutputCalledAfter1Min()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(61000); //sleep 61 secs
            Received.InOrder(() =>
            {
                _output.OutputLine("PowerTube turned off");
                _output.OutputLine("Display cleared");
                _output.OutputLine("Light is turned off");
            });
        }
        
        [Test]
        public void StartCancelPressedDuringSetup_OutputCalledFromDisplay()
        {
            _powerButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("Display cleared");
        }
    }
}
