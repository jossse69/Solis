using System;

namespace Solis
{
    public class CPU
    {
        private byte[] memory;
        public byte Accumulator { get; private set; }
        private byte programCounter;
        private bool running;
        private bool CompareFlag { get; set; }
        private List<byte> stack = new List<byte>();
        public CPU(int memorySize)
        {
            memory = new byte[memorySize];
            Accumulator = 0;
            programCounter = 0;
            running = true;
        }

        public void LoadProgram(byte[] program)
        {
            Array.Copy(program, memory, program.Length);
        }

        public void Run()
        {
            while (running)
            {
                Console.WriteLine($"PC: {programCounter}, Accumulator: {Accumulator}");
                byte instruction = memory[programCounter++];
                switch (instruction)
                {
                    case 0x00: // NOP
                        break;
                    case 0x01: // LOAD
                        Accumulator = memory[programCounter++];
                        break;
                    case 0x02: // ADD
                        Accumulator += memory[programCounter++];
                        break;
                    case 0x03: // SUB
                        Accumulator -= memory[programCounter++];
                        break;
                    case 0x04: // MUL
                        Accumulator *= memory[programCounter++];
                        break;
                    case 0x05: // JMP
                        programCounter = memory[programCounter];
                        break;
                    case 0x06: // DIV
                        Accumulator /= memory[programCounter++];
                        break;
                    case 0x07: // JMPZ (Jump if Zero)
                        if (Accumulator == 0)
                        {
                            programCounter = memory[programCounter];
                        }
                        else
                        {
                            programCounter++;
                        }
                        break;
                    case 0x08: // JMPNZ (Jump if Not Zero)
                        if (Accumulator != 0)
                        {
                            programCounter = memory[programCounter];
                        }
                        else
                        {
                            programCounter++;
                        }
                        break;
                    case 0x09: // AND
                        Accumulator &= memory[programCounter++];
                        break;
                    case 0x0A: // OR
                        Accumulator |= memory[programCounter++];
                        break;
                    case 0x0B: // XOR
                        Accumulator ^= memory[programCounter++];
                        break;
                    case 0x0C: // POKE (Set memory address)
                        byte address = memory[programCounter++];
                        byte value = memory[programCounter++];
                        memory[address] = value;
                        break;
                    case 0x0D: // READ (Read memory address)
                        address = memory[programCounter++];
                        Accumulator = memory[address];
                        break;
                    case 0x0E: // INC
                        Accumulator++;
                        break;
                    case 0x0F: // DEC
                        Accumulator--;
                        break;
                    case 0x10: // CMP
                        CompareFlag = Accumulator == memory[programCounter++];
                        break;
                    case 0x11: // JMPF (Jump if Flag)
                        if (CompareFlag)
                        {
                            programCounter = memory[programCounter];
                        }
                        else
                        {
                            programCounter++;
                        }
                        break;
                    case 0x12: // CALL (Run subroutine)
                        stack.Add(programCounter);
                        programCounter = memory[programCounter];
                        break;
                    case 0x13: // RET (Return from subroutine)
                        programCounter = stack[stack.Count - 1];
                        stack.RemoveAt(stack.Count - 1);
                        break;
                    case 0x14: // POKEA (Poke accumulator to memory address)
                        address = memory[programCounter++];
                        memory[address] = Accumulator;
                        break;
                    case 0xFF: // HALT
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Unknown instruction: " + instruction);
                        running = false;
                        break;
                }
            }
        }

    }
}
