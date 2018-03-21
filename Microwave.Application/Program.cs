using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Setup all the objects, 
            var _output = new Output();
            var _door = new Door();
            var _timeButton = new Button();
            var _powerButton = new Button();
            var _startCancelButton = new Button();
            var _powerTube = new PowerTube(_output);
            var _display = new Display(_output);
            var _light = new Light(_output);
            var _timer = new Timer();
            var _cookController = new CookController(_timer, _display, _powerTube);
            _cookController.UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);


            Console.WriteLine("=============IT7===========");
            Console.WriteLine("=============Testíng normal use case===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(62000);

            Console.WriteLine("=============Testíng StartCancelPressed while cooking===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            Console.WriteLine("=============Testíng StartCancelPressed during setup===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _startCancelButton.Press();

            Console.WriteLine("=============IT8===========");
            Console.WriteLine("=============DoorOpenedWhileCooking===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(10000);
            _door.Open();

            Console.WriteLine("=============DoorOpenedDuringSetup===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _door.Open();

            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}
