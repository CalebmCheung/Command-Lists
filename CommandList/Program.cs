using FinchAPI;
using System;
using System.Collections.Generic;

namespace CommandArray
{
    // *************************************************************
    // add comment block here
    // *************************************************************

    /// <summary>
    /// control commands for the finch robot
    /// </summary>
    public enum FinchCommand
    {
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF
    }

    class Program
    {
        static void Main(string[] args)
        {
            Finch myFinch = new Finch();

            DisplayOpeningScreen();
            DisplayInitializeFinch(myFinch);
            DisplayMainMenu(myFinch);
            DisplayClosingScreen(myFinch);
        }

        /// <summary>
        /// display the main menu
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayMainMenu(Finch myFinch)
        {
            string menuChoice;
            bool exiting = false;

            int delayDuration = 0;
            int numberOfCommands = 0;
            int motorSpeed = 0;
            int LEDBrightness = 0;
            //FinchCommand[] commands = null;
            List<FinchCommand> commands = new List<FinchCommand>();

            while (!exiting)
            {
                //
                // display menu
                //
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("\t1) Get Command Parameters");
                Console.WriteLine("\t2) Get Finch Robot Commands");
                Console.WriteLine("\t3) Display Finch Robot Commands");
                Console.WriteLine("\t4) Execute Commands");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                //
                // process menu
                //
                switch (menuChoice)
                {
                    case "1":
                        numberOfCommands = DisplayGetNumberOfCommands();
                        delayDuration = DisplayGetDelayDuration();
                        motorSpeed = DisplayGetMotorSpeed();
                        LEDBrightness = DisplayGetLEDBrightness();
                        break;
                    case "2":
                        DisplayGetFinchCommands(commands,numberOfCommands);
                        break;
                    case "3":
                        if (commands.Count == 0)
                        {
                            Console.WriteLine("There are no commands entered");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            DisplayFinchCommands( commands);
                        }
                        break;
                    case "4":
                        if (commands.Count == 0)
                        {
                            Console.WriteLine("There are no commands entered");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            DisplayExecuteFinchCommands(myFinch, commands, motorSpeed, LEDBrightness, delayDuration);
                        }                     
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        Console.WriteLine("Please choose a menu option");
                        DisplayContinuePrompt();
                        break;
                }
            }
        }

        static void DisplayExecuteFinchCommands(Finch myFinch,List<FinchCommand> commands, int motorSpeed, int lEDBrightness, int delayDuration)
        {
            DisplayHeader("Execute Finch Commands");

            Console.WriteLine("Click any key when ready to initiate");
            DisplayContinuePrompt();

            for (int index = 0; index < commands.Count; index++)
            {
                Console.WriteLine($"Command: {commands[index]}");
                switch (commands[index])
                {
                    case FinchCommand.DONE:
                        break;
                    case FinchCommand.MOVEFORWARD:
                        myFinch.setMotors(motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.MOVEBACKWARD:
                        myFinch.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.STOPMOTORS:
                        myFinch.setMotors(0, 0);
                        break;
                    case FinchCommand.DELAY:
                        myFinch.wait(delayDuration);
                        break;
                    case FinchCommand.TURNRIGHT:
                        myFinch.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.TURNLEFT:
                        myFinch.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.LEDON:
                        myFinch.setLED(lEDBrightness, lEDBrightness, lEDBrightness);
                        break;
                    case FinchCommand.LEDOFF:
                        myFinch.setLED(0, 0, 0);
                        break;
                    default:
                        break;
                }

            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommands(List<FinchCommand> commands, int numberOfCommands)
        {
            FinchCommand command;
            //Variables
           
            DisplayHeader("Get Finch Commands");

            commands.Clear();

            for (int index = 0; index < numberOfCommands; index++)
            {
                Console.Write($"Command #{index + 1}:");
                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                     commands.Add(command);
                }
                else
                {
                    Console.WriteLine("Please enter valid command");
                    index = index - 1;
                }

            }

            Console.WriteLine();
            Console.WriteLine("The Commands:");
            foreach (FinchCommand finchcommand in commands)
            {
                Console.WriteLine(finchcommand);
            }

            DisplayContinuePrompt();


        }

        static void DisplayFinchCommands(List<FinchCommand> commands)
        {
            //Variables

            DisplayHeader("Finch Commands");

            if (commands != null)
            {
                Console.WriteLine("The Commands:");
                foreach (FinchCommand command in commands)
                {
                    Console.WriteLine(command);
                }
            }

            else
            {
                Console.WriteLine("Please go back and enter commands");
            }

            DisplayContinuePrompt();

            
        }

        static int DisplayGetDelayDuration()
        {
            int delayDuration = 0;
            bool valid;

            valid = false;

            DisplayHeader("Length of Delay");
            

            while (!valid)
            {
                Console.Write("Enter the Delay Length (milliseconds):");
                if (!int.TryParse(Console.ReadLine(), out delayDuration))
                {
                    Console.WriteLine("Please enter a number");
                }
                else
                {
                    valid = true;
                }
            }

            DisplayContinuePrompt();

            return delayDuration;
        }

        /// <summary>
        /// get the number of commands from the user
        /// </summary>
        /// <returns>number of commands</returns>
        static int DisplayGetNumberOfCommands()
        {
            int numberOfCommands = 0;
            bool valid;

            valid = false;

            DisplayHeader("Number of Commands");


            while (!valid)
            {
                Console.Write("Enter the number of commands:");
                if (!int.TryParse(Console.ReadLine(), out numberOfCommands)) 
                {
                    Console.WriteLine("Please enter a number of commands");
                }
                else
                {
                    valid = true;
                }
            }

            DisplayContinuePrompt();

            return numberOfCommands;
        }

        static int DisplayGetMotorSpeed()
        {
            int motorSpeed = 0;
            bool valid;

            valid = false;

            DisplayHeader("Motor Speed");

            while (!valid)
            {
                Console.Write("Enter the Motor Speed (1 - 255):");
                if (!int.TryParse(Console.ReadLine(), out motorSpeed))
                {
                    Console.WriteLine("Please enter a number");
                }
                else if (motorSpeed < 1 || motorSpeed > 255)
                {
                    Console.WriteLine("Please enter a number in the range of 1 - 255");
                }
                else
                {
                    valid = true;
                }
            }

            DisplayContinuePrompt();

            return motorSpeed;

        }

        static int DisplayGetLEDBrightness()
        {
            int brightness = 0;
            bool valid;

            valid = false;

            DisplayHeader("LED Brightness");

            while (!valid)
            {
                Console.Write("Enter the level of Brightness (1 - 255):");
                if (!int.TryParse(Console.ReadLine(), out brightness))
                {
                    Console.WriteLine("Please enter a number ");
                }
                else if (brightness < 1 || brightness > 255)
                {
                    Console.WriteLine("Please enter a number within the range of 1-255");
                }
                else
                {
                    valid = true;
                }
            }

            DisplayContinuePrompt();

            return brightness;
        }

        /// <summary>
        /// initialize and confirm the finch connects
        /// </summary>
        /// <param name="myFinch"></param>
        static void DisplayInitializeFinch(Finch myFinch)
        {
            DisplayHeader("Initialize the Finch");

            Console.WriteLine("Please plug your Finch Robot into the computer.");
            Console.WriteLine();
            DisplayContinuePrompt();

            while (!myFinch.connect())
            {
                Console.WriteLine("Please confirm the Finch Robot is connect");
                DisplayContinuePrompt();
            }

            FinchConnectedAlert(myFinch);
            Console.WriteLine("Your Finch Robot is now connected");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// audio notification that the finch is connected
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void FinchConnectedAlert(Finch myFinch)
        {
            myFinch.setLED(0, 255, 0);

            for (int frequency = 17000; frequency > 100; frequency -= 100)
            {
                myFinch.noteOn(frequency);
                myFinch.wait(10);
            }

            myFinch.noteOff();
        }

        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\tProgram Your Finch");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen and disconnect finch robot
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayClosingScreen(Finch myFinch)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tThank You!");
            Console.WriteLine();

            myFinch.disConnect();

            DisplayContinuePrompt();
        }

        #region HELPER  METHODS

        /// <summary>
        /// display header
        /// </summary>
        /// <param name="header"></param>
        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + header);
            Console.WriteLine();
        }

        /// <summary>
        /// display the continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        #endregion
    }
}
