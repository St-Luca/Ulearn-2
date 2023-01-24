using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public Dictionary<char, Action<IVirtualMachine>> MachineCommands = new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			MachineCommands.Add(symbol, execute);
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (MachineCommands.ContainsKey(Instructions[InstructionPointer]))
				{
					MachineCommands[Instructions[InstructionPointer]](this);//действующая инструкция, которую выполняет машина
					InstructionPointer++;
				}
				else
					InstructionPointer++;
			}
		}
	}
}