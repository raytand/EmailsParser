using EmailsParser.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace EmailsParser
{
    public class Controller
    {
        private readonly IInputService _input;
        private readonly IParsingService _parsing;
        public Controller(IParsingService parsing,IInputService input)
        {
            _parsing = parsing;
            _input = input;
        }

        //Control method to do make start up process not in static main
        public async Task ControlAsync()
        {
            string path = await _input.PathInput();

            if (path == null)
            {
                await Console.Out.WriteLineAsync("Path cannot be null");
                return;
            }

            if (!Directory.Exists(path))
            {
                await Console.Out.WriteLineAsync("Cannot find directory");
                await Console.Out.WriteLineAsync("Try again");
                return;
            }
            try
            {
                await _parsing.ParseAsync(path);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

    }

}
