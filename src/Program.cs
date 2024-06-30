using System;
using System.IO;

namespace Solis
{
    class Program
    {
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
            string binFilePath =  inputstring;

            // Check if the file exists
            if (!File.Exists(binFilePath))
            {
                Console.WriteLine($"Error: File '{binFilePath}' not found.");
                return;
            }

            // Read the bytecode from the .bin file
            byte[] program = File.ReadAllBytes(binFilePath);

            // Initialize CPU with 256 bytes of memory
            CPU cpu = new CPU(256);
            
            // Load the program into CPU memory
            cpu.LoadProgram(program);
            
            // Run the program
            cpu.Run();

            // Output final value in accumulator
            Console.WriteLine("Final value in accumulator: " + cpu.Accumulator);
        }
    }
}
