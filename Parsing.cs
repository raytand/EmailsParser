using EmailsParser.Interfaces;
using System.Text.RegularExpressions;

namespace EmailsParser
{
    public class Parsing : IParsingService
    {
        private const string EmailPattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,7}\b";

        private readonly string[] _mailboxRestrictions =
        {
            @"info@itvdn.com",
            @"spam@quarantine6.antispamcloud.com",
            @"MAILER-DAEMON@eu-west-1.amazonses.com"
        };

        private readonly string[] _domainRestrictions = { @"@eu-west-1.amazonses.com" };

        private readonly Writing _writing;

        public Parsing()
        {
            _writing = new Writing();
        }

        public async Task ParseAsync(string path)
        {
            SetConsoleColor(ConsoleColor.Green);
            await Console.Out.WriteLineAsync("Parsing started");
            ResetConsoleColor();

            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    using (var stream = new StreamReader(file))
                    {
                        if (File.Exists(file))
                        {
                            var text = stream.ReadToEnd();
                            var emails = ExtractEmails(text);
                            await WriteEmailsToFile(emails, file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            SetConsoleColor(ConsoleColor.Green);
            await Console.Out.WriteLineAsync("Parsing ended");
            ResetConsoleColor();
        }

        private static void SetConsoleColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        private static void ResetConsoleColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private MatchCollection ExtractEmails(string text)
        {
            var regex = new Regex(EmailPattern);
            return regex.Matches(text);
        }

        private async Task WriteEmailsToFile(MatchCollection matches, string file)
        {
            var filteredEmails = matches
                .Where(c => !_mailboxRestrictions.Contains(c.Value) && !_domainRestrictions.Contains(c.Value))
                .ToList();

            if (filteredEmails.Any())
            {
                await _writing.WriteMailAsync(filteredEmails.Last().Value, "mails.txt");
            }
            else
            {
                await _writing.WriteNotFoundAsync(file, "mailNotFound.txt");
            }
        }
    }
}
