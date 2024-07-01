using System;
using SFML.Graphics;
using SFML.Window;

namespace Solis
{
    class Program
    {
        public static RenderWindow Window;
        static void Main(string[] args)
        {
            // Specify the path to your .bin file
            Console.WriteLine("Enter the path to the .bin file:");
            var inputstring = Console.ReadLine();
            if (inputstring == null)
            {
                Console.WriteLine("Error: Invalid input.");
                return;
            }
            string binFilePath = inputstring;

            // Check if the file exists
            if (!System.IO.File.Exists(binFilePath))
            {
                Console.WriteLine($"Error: File '{binFilePath}' not found.");
                return;
            }

            // Read the bytecode from the .bin file
            byte[] program = System.IO.File.ReadAllBytes(binFilePath);

            // Initialize CPU
            CPU cpu = new CPU(1024);

            // Load the program into CPU memory
            cpu.LoadProgram(program);

                        // Initialize SFML window for graphical output
            Window = new RenderWindow(new VideoMode(800, 600), "Solis Emulator");

            // Run the program
            cpu.Run();

            // Output final value in accumulator
            Console.WriteLine("Final value in accumulator: " + cpu.Accumulator);

        }
    }
}
