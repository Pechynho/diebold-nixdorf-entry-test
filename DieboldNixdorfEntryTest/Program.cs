using DieboldNixdorfEntryTest.Core;
using DieboldNixdorfEntryTest.Data;
using DieboldNixdorfEntryTest.Entity;
using DieboldNixdorfEntryTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieboldNixdorfEntryTest.Command;

namespace DieboldNixdorfEntryTest
{
    class Program
    {
        static List<AbstractCommand> commands = new List<AbstractCommand>();

        static void CreateCommands()
        {
            commands.Add(new ListCommand());
            commands.Add(new AddCommand());
            commands.Add(new LoginCommand());
            commands.Add(new DeleteCommand());
        }

        static void Introduction()
        {
            Console.WriteLine("Vítá vás aplikace pro správu uživatelů.");
            Console.WriteLine($"K jejímu ovládání využijte následující příkazy: {string.Join(", ", commands.Select(u => u.Name))}.");
            Console.WriteLine($"K zobrazení popisu příkazu využijte parametr --help(např. '{commands.First().Name} --help').");
            Console.WriteLine("Aplikaci ukončíte zadáním příkazu 'exit'.");
        }

        static void Main(string[] args)
        {
            CreateCommands();
            string input;
            do
            {
                Introduction();
                Console.Write("Zadejte příkaz: ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                input = input.Trim();
                if (input != "exit")
                {
                    string[] parts = input.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 3)
                    {
                        var command = commands.FirstOrDefault(u => u.Name == parts[0]);
                        if (command != null && parts.Length == 2)
                        {
                            if (command.TryParseParameter(out Parameter? parameter, parts[1]))
                            {
                                command.ProcessParameter(parameter.Value);
                                Console.ReadKey();
                            }
                        }
                        else if (command != null && parts.Length == 1)
                        {
                            command.Start();
                            Console.ReadKey();
                        }

                    }
                }
                Console.Clear();
                
            } while (input != "exit");
        }
    }
}
