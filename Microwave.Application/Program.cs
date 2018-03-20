using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;

namespace Microwave.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup all the objects, 
            var door = new Door();
            var _timeButton = new Button();
            var _powerButton = new Button();
            var _cancelButton = new Button();

            // etc.

            // Simulate user activities
            _powerButton.Press();
            // ...
            // etc. 



            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}
