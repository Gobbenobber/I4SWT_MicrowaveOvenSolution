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
        //public static void Menu()
        //{
        //    Console.WriteLine("\t\tMicrowave oven");
        //    Console.WriteLine();
        //    Console.WriteLine("<P>. \tPower button");
        //    Console.WriteLine("<T>. \tDelete a team");
        //    Console.WriteLine("<S>. \tStart/Cancel button");
        //    Console.WriteLine("<D>. \tOpen door");
        //}

        private static IUserInterface ui;

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
            var _cookController = new CookController(_timer, _display, _powerTube,ui);
            ui = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);


            Console.WriteLine("=============IT7===========");
            Console.WriteLine("=============Testíng normal use case===========");
            //_door.Open();
            //_door.Close();
            //_powerButton.Press();
            //_timeButton.Press();
            //_startCancelButton.Press();
            //Thread.Sleep(61000);
            Console.WriteLine("=============Testíng cancelled use case===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            Console.WriteLine("=============IT8===========");
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();            

            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}
