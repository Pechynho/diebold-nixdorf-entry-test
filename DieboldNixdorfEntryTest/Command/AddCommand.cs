using DieboldNixdorfEntryTest.Command.Validation;
using DieboldNixdorfEntryTest.Core;
using DieboldNixdorfEntryTest.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DieboldNixdorfEntryTest.Utility;

namespace DieboldNixdorfEntryTest.Command
{
    class AddCommand : AbstractCommand
    {
        public AddCommand()
        {
            Name = "add";
            Description = "Tento příkaz slouží k uložení nového uživatele do databáze. Bude po vás chtít uživatelské jméno maximálně 15 znaků dlouhé a heslo s délkou od 5 do 20 znaků.";
        }

        public override void Start()
        {
            string username = GetInput<string>("Uživatelské heslo: ", new InputStringValidation()
            {
                AllowEmpty = false,
                AllowNull = false,
                AllowOnlyWhitespaces = false,
                BlacklistCharacters = new char[] { ' ' },
                MinLength = 1,
                MaxLength = 15
            }).Trim();
            User user = repository.SearchForFirst(u => u.Username == username);
            if (user != null)
            {
                Console.WriteLine("Dané uživatelské jméno využívá již jiný uživatel.");
                return;
            }
            string password = GetInput<string>("Heslo: ", new InputStringValidation()
            {
                AllowEmpty = false,
                AllowNull = false,
                AllowOnlyWhitespaces = false,
                BlacklistCharacters = new char[] { ' ' },
                MinLength = 5,
                MaxLength = 20
            }).Trim();
            var cipher = Container.ServiceProvider.GetService<ICipher>();
            repository.Add(new User(username, cipher.Encrypt(password)));
            Console.WriteLine("Uživatel byl úspěšně přidán do databáze.");
        }
    }
}
