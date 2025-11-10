using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallets
{
	/// <summary>
	/// Генератор случайных последовательностей данных разных типов и объектов.
	/// </summary>
	public static class DataGenerator
	{
		#region EdgeConstants
		public const int lowestIncome = 50;

		public const int highestIncome = 1000;

		public const int lowestExpense = 10;

		public const int highestExpense = 800;

		public const int lowestInitialBalance = 500;

		public const int highestInitialBalance = 5000;
		#endregion

		/// <summary>
		/// Генерирует список транзакций размера <paramref name="size"/> от заданной даты 
		/// <paramref name="referenceDate"/> до текущей.
		/// </summary>
		/// <param name="size"> Количество транзакций. </param>
		/// <param name="referenceDate"> Опорная дата. </param>
		/// <returns> Список транзакций. </returns>
		public static List<Transaction> GenerateTransactions(int size, DateTime referenceDate)
		{
			var random = new Random();
			
			var currentDate = DateTime.Now;
			var daysFromReference = (int)(currentDate - referenceDate).TotalDays;


			return Enumerable.Range(0, size)
				.Select(x =>
				{
					var isIncome = random.Next(0, 2) == 0;
					var amount = isIncome ? random.NextDouble() * highestIncome + lowestIncome 
						: random.NextDouble() * highestExpense + lowestExpense;

					var descriptions = isIncome ? 
						new string[] { "Пополнение счёта.", "Входящий перевод.", "Заработная плата", "Возврат средств." } :
						new string[] { "Оплата товаров.", "Перевод средств.", "Оплата услуг.", "Автоплатёж." };

					return new Transaction(
					   DateTime.Now.AddDays(-random.Next(0, daysFromReference)),
					   amount,
					   isIncome ? TransactionType.Income : TransactionType.Expense,
					   $"{descriptions[random.Next(descriptions.Length)]}"
					   );
				}
				).ToList();
		}

		/// <summary>
		/// Генерирует список кошельков размера <paramref name="size"/>.
		/// </summary>
		/// <param name="size"> Количество кошельков. </param>
		/// <returns> Список кошельков. </returns>
		public static List<Wallet> GenerateWallets(int size)
		{
			var random = new Random();

			var walletNames = new string[] { "Qiwi", "Yandex", "Payoneer", "WebMoney" };
			var currencies = new string[] { "Dollar", "Euro", "Ruble", "Lari" };

			return Enumerable.Range(0, size)
				.Select(x =>
					 new Wallet(
						$"{walletNames[random.Next(walletNames.Length)]}",
						$"{currencies[random.Next(currencies.Length)]}",
						random.NextDouble() * highestInitialBalance + lowestInitialBalance
						)
				).ToList();
		}
	}
}
