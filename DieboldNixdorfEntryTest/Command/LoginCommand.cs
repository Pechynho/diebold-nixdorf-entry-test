using DieboldNixdorfEntryTest.Command.Validation;
using DieboldNixdorfEntryTest.Core;
using DieboldNixdorfEntryTest.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DieboldNixdorfEntryTest.Entity;

namespace DieboldNixdorfEntryTest.Command
{
    class LoginCommand : AbstractCommand
    {
        public LoginCommand()
        {
            Name = "login";
            Description = "Tento příkaz slouží k ověření uživatelských údajů. Bude po vás chtít uživatelské jméno a heslo. Po zpracování vám oznámí, jestli se v databází nachází uživatel se zadanou kombinací údajů.";
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
            User user = repository.SearchForFirst(u => u.Username == username && u.Password == cipher.Encrypt(password));
            Console.WriteLine(user == null ? "V databázi se nenachází žádný uživatel se zadanou kombinací údajů." : $"Uživatel byl nalezen. Jeho ID je {user.ID}.");
        }
    }
}
