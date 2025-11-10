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
	public static class CommonHelper
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

		/// <summary>
		/// Ввод числовых данных из консоли.
		/// </summary>
		/// <param name="message"> Строковое сообщение для вывода в консоль. </param>
		/// <returns> Вещественные числа из консоли. </returns>
		public static double ReadDoubleFromConsole(string message)
		{
			Console.WriteLine(message);

			return double.Parse(Console.ReadLine());
		}

		/// <summary>
		/// Ввод данных даты из консоли.
		/// </summary>
		/// <param name="message"> Строковое сообщение для вывода в консоль. </param>
		/// <returns> Дату. </returns>
		public static DateTime ReadDateFromConsole(string message)
		{
			Console.WriteLine(message);

			return DateTime.Parse(Console.ReadLine());
		}
	}
}
