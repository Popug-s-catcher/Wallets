using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallets
{
	/// <summary>
	/// Транзакция.
	/// </summary>
	public class Transaction
	{
	#region Fields
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid TransactionId { get; }

		/// <summary>
		/// Время проведения транзакции.
		/// </summary>
		public DateTime Date { get; }

		/// <summary>
		/// Сумма.
		/// </summary>
		public double Amount { get; }

		/// <summary>
		/// Тип транзакции.
		/// </summary>
		public TransactionType Type { get; }

		/// <summary>
		/// Описание.
		/// </summary>
		public string Description { get; }
	#endregion

		/// <summary>
		/// Инициализирует все поля, создаёт идентификатор.
		/// </summary>
		/// <param name="date"> Дата транзакции. </param>
		/// <param name="amount"> Сумма. </param>
		/// <param name="type"> Тип транзакции. </param>
		/// <param name="description"> Описание. </param>
		public Transaction(DateTime date, double amount, TransactionType type, string description)
		{
			TransactionId = Guid.NewGuid();
			Date = date;
			Amount = amount;
			Type = type;
			Description = description;
		}

		/// <summary>
		/// Возвращает информацию о транзакции в виде строки.
		/// </summary>
		/// <returns> Строка сведений об объекте. </returns>
		public string GetInfo()
		{
			return new StringBuilder($"ID - {TransactionId} | " +
				$"{Date} | {Type} \t| {Amount:f2} \t| {Description}").ToString();
		}
	}

	/// <summary>
	/// Типы транзакций.
	/// </summary>
	public enum TransactionType
	{
		Income = 0,
		Expense = 1
	}
}
