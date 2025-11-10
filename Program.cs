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
            var controller = new Controller();

            controller.Run();
        }
    }
}
