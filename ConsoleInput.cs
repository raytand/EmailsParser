namespace EmailsParser
{
    public class ConsoleInput
    {
        private static string path;
        public Task<string> PathInput()
        {
            try
            {
                while (path == null)
                {
                    Console.WriteLine("Enter path to folder");
                    path = Console.ReadLine();
                    if (path == null)
                    {
                        Console.WriteLine("Can not use null reference");
                    }
                }
                return Task.FromResult<string>(path);

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult<string>(path);
            }
        }
    }
}
