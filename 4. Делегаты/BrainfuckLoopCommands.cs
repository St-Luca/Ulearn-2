using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static Dictionary<int, int> BracketsPositions = new Dictionary<int, int>();
		public static Stack<int> StackBrackets = new Stack<int>();//—так дл€ вложенности скобок

		public static void RegisterTo(IVirtualMachine vm)
		{
			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
					StackBrackets.Push(i);
				else if (vm.Instructions[i] == ']')//ѕопарно св€зываем индексы
				{
					BracketsPositions[i] = StackBrackets.Peek();
					BracketsPositions[StackBrackets.Pop()] = i;
				}
			}
			vm.RegisterCommand('[', execute => //благодар€ парам быстро перескакиваем
			{
				if (execute.Memory[execute.MemoryPointer] == 0)
				{
					execute.InstructionPointer = BracketsPositions[execute.InstructionPointer];
				}
			});
			vm.RegisterCommand(']', execute =>
			{
				if (execute.Memory[execute.MemoryPointer] != 0)
				{
					execute.InstructionPointer = BracketsPositions[execute.InstructionPointer];
				}
			});
		}
	}
}