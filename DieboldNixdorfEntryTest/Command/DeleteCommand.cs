using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieboldNixdorfEntryTest.Command.Validation;
using DieboldNixdorfEntryTest.Entity;

namespace DieboldNixdorfEntryTest.Command
{
    class DeleteCommand : AbstractCommand
    {
        public DeleteCommand()
        {
            Name = "delete";
            Description = "Tento příkaz slouží k odstranění uživatele z databáze pomocí uživatelského jména.";
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
            if (user == null)
            {
                Console.WriteLine("Nebyl nalezen žádný uživatel dle zadaného uživatelského jména.");
                return;
            }
            repository.Delete(user);
            Console.WriteLine("Uživatel byl úspěšně odstraněn z databáze.");
        }
    }
}
