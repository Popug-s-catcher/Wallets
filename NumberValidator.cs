using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallets
{
	/// <summary>
	/// Валидатор числовых значений.
	/// </summary>
	public static class NumberValidator
	{
		/// <summary>
		/// Проверка, что разность чисел <paramref name="leftNumber"/> 
		/// не <paramref name="rightNumber"/> неотрицательна.
		/// </summary>
		/// <param name="leftNumber"> Левое число. </param>
		/// <param name="rightNumber"> Правое число. </param>
		/// <exception cref="ArgumentException">
		/// Выбрасывается, если <paramref name="leftNumber"/> меньше <paramref name="rightNumber"/>.
		/// </exception>
		public static void IsNotNegativeDifference(double leftNumber, double rightNumber)
		{
			if (leftNumber < rightNumber)
			{
				throw new ArgumentException($"Разность чисел не может быть отрицательной!");
			}
		}
	}
}
