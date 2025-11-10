using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Wallets
{
	/// <summary>
	/// Кошелёк.
	/// </summary>
	public class Wallet
	{
		#region Fields
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid WalletId { get; }

		/// <summary>
		/// Название кошелька.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Валюта.
		/// </summary>
		public string Currency { get; }

		/// <summary>
		/// Начальный баланс.
		/// </summary>
		public double InitialBalance { get; }

		/// <summary>
		/// Текущий баланс.
		/// </summary>
		public double CurrentBalance { get; private set; }

		/// <summary>
		/// Список транзакций.
		/// </summary>
		private List<Transaction> _transactions { get; set; }
		#endregion

		/// <summary>
		/// Инициализирует все поля, создаёт идентификатор.
		/// </summary>
		/// <param name="name"> Название кошелька. </param>
		/// <param name="currency"> Валюта. </param>
		/// <param name="initialBalance"> Начальный баланс. </param>
		public Wallet(string name, string currency, double initialBalance)
		{
			WalletId = Guid.NewGuid();
			
			Name = name;
			
			Currency = currency;
			
			InitialBalance = initialBalance;
			CurrentBalance = initialBalance;

			_transactions = new List<Transaction>();
		}

		/// <summary>
		/// Вычисляет текущий баланс.
		/// </summary>
		/// <returns> Текущий баланс. </returns>
		public double CalculateCurrentBalance()
		{
			CurrentBalance = InitialBalance;

			foreach (var transaction in _transactions)
			{
				CurrentBalance += (transaction.Type == TransactionType.Income
					? transaction.Amount : transaction.Amount * -1);
			}

			return CurrentBalance;
		}

		/// <summary>
		/// Проводит транзакцию <paramref name="transaction"/> согласно её типу.
		/// </summary>
		/// <param name="transaction"> Транзакция дохода или расхода. </param>
		public void ConductTransaction(Transaction transaction)
		{
			if (transaction.Type == TransactionType.Income)
			{
				CurrentBalance += transaction.Amount;

				_transactions.Add(transaction);
			}
			else
			{
				try
				{
					CommonHelper.IsNotNegativeDifference(CurrentBalance, transaction.Amount);

					CurrentBalance -= transaction.Amount;

					_transactions.Add(transaction);
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine(ex.ToString());
					Console.WriteLine($"Транзакция [{transaction.TransactionId}] не была проведена в кошельке [{WalletId}]!\n");
				}
			}
		}

		/// <summary>
		/// Расчёт месячных доходов/расходов в зависимости от заданного <paramref name="type"/>.
		/// </summary>
		/// <param name="type"> Рассчитываемый тип - доходы или расходы. </param>
		/// <returns> Вещественная сумма доходов/расходов. </returns>
		public double CalculateMonthOperationsTotal(TransactionType type, int month)
		{
			return _transactions
				.Where(t => t.Type == TransactionType.Income && t.Date.Month == month)
				.Sum(t => t.Amount);
		}

		/// <summary>
		/// Рассчитывает статистику за выбранный <paramref name="targetMonth"/> транзакций
		/// кошелька по типам транзакций, сортируя транзакции между собой по возрастанию и группы транзакций по убыванию.
		/// </summary>
		/// <param name="targetMonth"> Целевой месяц. </param>
		/// <returns> Список кортежей (тип транзакции, общая сумма группы, список транзакций) </returns>
		public List<(TransactionType, double, List<Transaction>)> GetMonthTransactionsStatistics(int targetMonth)
		{
			return _transactions
				.Where(t => t.Date.Month == targetMonth)
				.GroupBy(x => x.Type)
				.Select(g => 
				(
					g.Key,
					g.Sum(t => t.Amount),
					g.OrderBy(t => t.Date).ToList()
				))
				.OrderByDescending(g => g.Item2)
				.ToList();
		}

		/// <summary>
		/// Вычисляет три наибольшие затраты за <paramref name="targetMonth"/> для кошелька, сортируя их по убыванию.
		/// </summary>
		/// <param name="targetMonth"> Целевой месяц. </param>
		/// <returns> Список из трёх транзакций. </returns>
		public List<Transaction> GetMonthBiggestExpenses(int targetMonth)
		{
			return _transactions
				.Where(t => 
					t.Type == TransactionType.Expense &&
					t.Date.Month == targetMonth)
				.OrderByDescending(t => t.Amount)
				.Take(3)
				.ToList();
		}

		/// <summary>
		/// Возвращает информацию о кошельке и его транзакциях за весь период в виде строки.
		/// </summary>
		/// <returns> Строка сведений об объекте. </returns>
		public string GetInfo()
		{
			var info = new StringBuilder(
				$"Кошелёк [{WalletId}] - имя [{Name}] - валюта [{Currency}]\n" +
				$"Исходный баланс [{InitialBalance:f2}] - текущий баланс [{CalculateCurrentBalance():f2}]\n" +
				$"\nСписок транзакций кошелька:\n"
				);

			for(var i = 0; i < _transactions.Count(); i++)
			{
				info.AppendLine($"{i + 1}. \t" + _transactions[i].GetInfo());
			}

			info.AppendLine();
			
			return info.ToString();
		}
	}
}
