using EmailsParser.Exceptions;

namespace EmailsParser
{
    public class Controller
    {
        private readonly ConsoleInput _input;
        private readonly Parsing _parsing;


        public Controller() : this(new ConsoleInput(), new Parsing()) { }

        public Controller(ConsoleInput input, Parsing parsing)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _parsing = parsing ?? throw new ArgumentNullException(nameof(parsing));
        }

        public async Task Control()
        {
            try
            {
                var path = await GetInputPath();
                ValidatePath(path);
                await _parsing.ParseAsync(path);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task<string> GetInputPath()
        {
            var path = await _input.PathInput();
            if (string.IsNullOrEmpty(path))
            {
                throw new PathNotFoundException();
            }
            return path;
        }

        private void ValidatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }
        }

        private void HandleException(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
