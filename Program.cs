namespace EmailsParser
{
    internal class Program
    { 
        static async Task Main()
        {
            var start = new Controller();
            await start.Control();
        }
    }
}