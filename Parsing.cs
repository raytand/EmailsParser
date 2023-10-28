using EmailsParser.Interfaces;
using System.Text.RegularExpressions;

namespace EmailsParser
{
    internal class Parsing : IParsingService
    {
        private readonly Writing writing = new Writing();

        public async Task ParseAsync(string path)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync("Parsing started");
            Console.BackgroundColor = ConsoleColor.Black;
            try
            {
                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    using (var stream = new StreamReader(file))
                    {

                        if (File.Exists(file))
                        {
                            //Regex setting
                            string Text = stream.ReadToEnd();
                            string pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,7}\b";
                            var regex = new Regex(pattern);
                            var matches = regex.Matches(Text);

                            //LINQ check to filter company mails
                            var filteredMatch = matches
                            .Where(c =>
                            c.Value != @"info@itvdn.com" && c.Value != @"spam@quarantine6.antispamcloud.com" &&
                            c.Value != @"MAILER-DAEMON@eu-west-1.amazonses.com" &&
                            !c.Value.Contains(@"@eu-west-1.amazonses.com"));

                            string email = filteredMatch.LastOrDefault()?.Value;
                            if (email != null)
                            {
                                await writing.WriteMailAsync(email, "mails.txt");
                            }
                            else
                            {
                                await writing.WriteNotFoundAsync(file, "mailNotFound.txt");
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);


            }
            Console.BackgroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync("Parsing ended");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
