using EmailsParser.Interfaces;
using System.Text.RegularExpressions;

namespace EmailsParser
{
    internal class Parsing : IParsingService
    {
        private readonly Writing write = new Writing();

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
                    var stream = new StreamReader(file);

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
                            if (File.Exists("mails.txt"))
                            {
                                Console.WriteLine(email);
                                await write.WriteMailAsync(email, "mails.txt");
                            }
                            else
                            {
                                var fileController = File.Create("mails.txt");
                                fileController.Close();
                                await Console.Out.WriteLineAsync(email);
                                await write.WriteMailAsync(email, "mails.txt");

                            }
                        }
                        else
                        {
                            if (File.Exists("mailNotFound.txt"))
                            {

                                await write.WriteNotFoundAsync(file, "mailNotFound.txt");
                            }
                            else
                            {
                                var fileController = File.Create("mailNotFound.txt");
                                fileController.Close();
                                await write.WriteNotFoundAsync(file, "mailNotFound.txt");

                            }
                        }
                    }

                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync( ex.Message  );
                
                return;
            }
            Console.BackgroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync("Parsing ended");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
