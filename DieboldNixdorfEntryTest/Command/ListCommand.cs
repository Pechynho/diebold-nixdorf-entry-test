using DieboldNixdorfEntryTest.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Command
{
    class ListCommand : AbstractCommand
    {
        public ListCommand()
        {
            Name = "list";
            Description = "Tento příkaz slouží k vypsání všech uložených uživatelů v databázi.";
        }

        public override void Start()
        {
            foreach (User user in repository.GetAll())
            {
                Console.Write($"{user.ID.ToString("D4").PadRight(5)}{user.Username.PadRight(16)}{user.Password}{Environment.NewLine}");
            }
        }
    }
}
