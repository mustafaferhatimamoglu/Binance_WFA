namespace Binance_WFA
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form3());
            //Application.Run(new Form3_LINAUSDT());
            Application.Run(new Form7());
            //Application.Run(new Form4());
        }
    }
}