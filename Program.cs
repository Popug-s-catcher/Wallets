namespace Wallets
{
    internal class Program
    {
        /// <summary>
        /// Точка входа в головную программу.
        /// </summary>
        /// <param name="args"> Аргументы входные. </param>
        static void Main(string[] args)
        {
            var wallets = DataGenerator.GenerateWallets(10);

            var date = new DateTime(2025, 9, 1);

            foreach (var wallet in wallets)
            {
                var transactions = DataGenerator.GenerateTransactions(50, date);

                foreach (var transaction in transactions)
                {
                    wallet.ConductTransaction(transaction);
                }

                Console.WriteLine(wallet.GetInfo());
            }
        }
    }
}
