namespace EmailsParser
{
    public class Controller
    {
        private readonly ConsoleInput input = new ConsoleInput();
        private readonly Parsing parsing = new Parsing();
        //Cotrol method to do make start up process not in static main
        public async Task Control()
        {
            string path = await input.PathInput();

            if (path == null)
            {
                Console.WriteLine("Path cannot be null");
                return;
            }

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Cannot find directory");
                Console.WriteLine("Try again");
                return;
            }
            try
            {
                await parsing.ParseAsync(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}
