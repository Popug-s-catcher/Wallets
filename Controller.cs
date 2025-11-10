using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallets
{
	/// <summary>
	/// Класс, управляющий кошельками
	/// </summary>
	public class Controller
	{
		#region Fields
		/// <summary>
		/// Количество кошельков.
		/// </summary>
		private int _walletsAmount { get; set; }

		/// <summary>
		/// Максимальное количество транзакций в кошельке.
		/// </summary>
		private int _transactionsPerWallet { get; set; }

		/// <summary>
		/// Дата, с которой генерируются транзакции в кошельке.
		/// </summary>
		private DateTime _startDate { get; set; }

		/// <summary>
		/// Список кошельков.
		/// </summary>
		private List<Wallet> _wallets;
		#endregion

		/// <summary>
		/// Инициализирует список кошельков.
		/// </summary>
		public Controller()
		{
			_wallets = new List<Wallet>();
		}

		/// <summary>
		/// Инициализирует все поля класса.
		/// </summary>
		private void InitializeWallets()
		{
			_wallets = DataGenerator.GenerateWallets(_walletsAmount);

			foreach (var wallet in _wallets)
			{
				var transactions = DataGenerator.GenerateTransactions(_transactionsPerWallet, _startDate);

				foreach (var transaction in transactions)
				{
					wallet.ConductTransaction(transaction);
				}

				Console.WriteLine(wallet.GetInfo());

				//Console.WriteLine($"Доходы за октябрь " +
				//	$"{wallet.CalculateMonthOperationsTotal(TransactionType.Income, 10):f2}");
				//Console.WriteLine($"Расходы за октябрь " +
				//	$"{wallet.CalculateMonthOperationsTotal(TransactionType.Expense, 10):f2}\n");
			}
		}

		/// <summary>
		/// Вводит данные и последовательно запускает все вычисления у кошельков. 
		/// </summary>
		public void Run()
		{
			_walletsAmount = (int)CommonHelper.ReadDoubleFromConsole("Введите количество кошельков: ");

			_transactionsPerWallet = (int)CommonHelper.ReadDoubleFromConsole("Введите максимальное количество транзакций для кошелька: ");

			_startDate = CommonHelper.ReadDateFromConsole("Введите дату (в формате dd.MM.yyyy), с которой будут сгенерированы транзакции: ");

			InitializeWallets();

			var targetMonth = (int)CommonHelper.ReadDoubleFromConsole("Введите целевой месяц: ");


			PrintWalletMonthStatistics(targetMonth);

			PrintWalletBiggestMonthExpenses(targetMonth);
		}

		/// <summary>
		/// Вызывает вычисление статистики всех транзакций у каждого из кошельков и выводит результат.
		/// </summary>
		/// <param name="targetMonth"> Целевой для статистики месяц. </param>
		private void PrintWalletMonthStatistics(int targetMonth)
		{
			var result = new StringBuilder();
			
			foreach (var wallet in _wallets)
			{
				result.AppendLine($"\nМесячная статистика доходов/расходов кошелька {wallet.WalletId}: \n");

				var transactionsStatistics = wallet.GetMonthTransactionsStatistics(targetMonth);

				foreach (var transaction in transactionsStatistics)
				{
					result.AppendLine($"\nТип [{transaction.Item1}] \t| Общая сумма [{transaction.Item2}]");

					foreach (var elem in transaction.Item3)
					{
						result.AppendLine(elem.GetInfo());
					}
				}
			}

			Console.WriteLine(result.ToString());
		}

		/// <summary>
		/// Вызывает вычисление статистики трёх наибольших трат у каждого из кошельков и выводит результат.
		/// </summary>
		/// <param name="targetMonth"> Целевой для статистики месяц. </param>
		private void PrintWalletBiggestMonthExpenses(int targetMonth)
		{
			var result = new StringBuilder();

			foreach (var wallet in _wallets)
			{
				result.AppendLine($"\nМесячная статистика наибольших расходов кошелька {wallet.WalletId}: \n");

				var transactions = wallet.GetMonthBiggestExpenses(targetMonth);

				foreach (var transaction in transactions)
				{
					result.AppendLine(transaction.GetInfo());
				}
			}

			Console.WriteLine(result.ToString());
		}
	}
}
