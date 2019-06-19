using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class SampleMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public SampleMenu() : base()
        {
            this.Title = "*** Main Menu ***";
            this.menuOptions.Add("1", "Option One");
            this.menuOptions.Add("2", "Add two numbers");
            this.menuOptions.Add("Q", "Quit");
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    // This is just a sample, does nothing
                    return true;
                case "2":
                    // Get some input form the user, and then do something
                    int someNumber = GetInteger("Please enter a whole number:");
                    int anotherNumber = GetInteger("Please enter another whole number:");
                    Console.WriteLine($"{someNumber} + {anotherNumber} = {someNumber + anotherNumber}.");
                    Pause("");
                    return true;
            }
            return true;
        }

        protected override void DisplayBeforeMenu()
        {
            Console.WriteLine("*** Use the override DisplayBeforeMenu to display information");
            Console.WriteLine("*** here, at the top of the screen.  It will be called by the CLI");
            Console.WriteLine("*** Run method.  Here you might include park information, for example.");
            Console.WriteLine();
        }

        protected override void DisplayAfterMenu()
        {
            Console.WriteLine("*** You can also display your own text here by overriding DisplayAfterMenu. ***");
        }

    }
}
