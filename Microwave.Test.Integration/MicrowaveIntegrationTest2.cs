﻿using System;
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
    public class MicrowaveIntegrationTest2
    {

        private IDoor _uut;
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
            _uut = new Door();
            _cookController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _timeButton = new Button();
            _powerButton = new Button();        //Buttons can be included; assumed unit tested prior to integration test.
            _startCancelButton = new Button();
            _userInterface = new UserInterface(_powerButton,_timeButton,_startCancelButton,_uut,_display,_light,_cookController);
        }

        [Test]
        public void DoorOpened_UITurnsOnLight()
        {
            _uut.Open();
            _light.Received().TurnOn();
        }

        [Test]
        public void DoorOpenedThenClosed_UITurnsLightOnOff()
        {
            _uut.Open();
            _uut.Close();
            Received.InOrder(() =>
            {
                _light.TurnOn();
                _light.TurnOff();
            });
            
        }

        [Test]
        public void DoorOpenedWhileCooking_StopCookControllerDisplayCleared()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _uut.Open();

            Received.InOrder(() =>
            {
                _cookController.Stop();
                _display.Clear();
            });

        }

        [Test]
        public void DoorOpenedDuringSetup_DisplayClearedLightTurnedOn()
        {
            _powerButton.Press();
            _timeButton.Press();
            _uut.Open();
            Received.InOrder(() =>
                {
                    _light.TurnOn();
                    _display.Clear();
                }
            );
        }

    }
}

