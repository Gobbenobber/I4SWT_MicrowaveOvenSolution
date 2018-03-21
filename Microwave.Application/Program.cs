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

            Console.WriteLine("============= IT7 - BUTTON IS DRIVER ===========");

            Console.WriteLine("=============Testíng normal use case===========");
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(62000);

            Console.WriteLine("\n=============Testíng StartCancelPressed while cooking===========");
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(1000);

            Console.WriteLine("\n=============Testíng StartCancelPressed during setup===========");

            // Setup all the objects, 
            _output = new Output();
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _cookController.UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _powerButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(1500);

            Console.WriteLine("\n\n============= IT8 - DOOR IS DRIVER ===========");

            Console.WriteLine("\n=============DoorOpenedWhileCooking===========");
            // Setup all the objects, 
            _output = new Output();
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _cookController.UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Thread.Sleep(10000);
            _door.Open();

            Console.WriteLine("\n=============DoorOpenedDuringSetup===========");

            // Setup all the objects, 
            _output = new Output();
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _cookController.UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _powerButton.Press();
            _door.Open();
            
            Console.WriteLine("\n=============DoorOpenedThenClosed===========");

            // Setup all the objects, 
            _output = new Output();
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _cookController.UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _door.Open();
            _door.Close();

            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}
