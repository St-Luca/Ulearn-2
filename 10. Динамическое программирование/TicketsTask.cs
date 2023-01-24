using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace Tickets
{
	public class TicketsTask
	{
		public static BigInteger Solve(int totalLen, int sum)
		{
			int halfOfSum;

			if (sum % 2 != 0) //Сумма должна быть четной, 
			{                 //т.к половины цифр должны давать одинаковые суммы
				return 0;
			}
			else
			{
				halfOfSum = sum / 2;
			}

			BigInteger[,] res = new BigInteger[totalLen + 1, sum + 1]; //Каждый элемент - количество билетов с n цифр с s суммой

			for (int i = 0; i <= halfOfSum; i++)
			{
				res[0, i] = 0; //Граничный случай, таких билетов не существует
			}

			for (int i = 0; i <= totalLen; i++)
			{
				res[i, 0] = 1; //Граничный случай, билет вида 0000
			}

			for (int n = 1; n <= totalLen; n++) // n - количество цифр
			{
				for (int s = 0; s <= halfOfSum; s++) // s - сумма цифр
				{
					if (s > n * 9)  //Проверка на то,что сумма s вообще существует
					{
						res[n, s] = 0;
					}
					else res[n, s] = GetN(res, n - 1, s); //количество n-значных чисел с суммой 
														  //цифр s можно выразить через количество (n−1)-значных чисел, добавляя к ним количество билетов с n-нной цифрой
				}
			}
			return BigInteger.Pow(res[totalLen, halfOfSum], 2);
		}

		public static BigInteger GetN(BigInteger[,] res, int n, int s)
		{
			BigInteger resSum = 0;
			int resN = 0;
			while (resN < 10 && s - resN >= 0)
			{
				resSum += res[n, s - resN];
				resN++;
			}

			return resSum;
		}
	}
}
