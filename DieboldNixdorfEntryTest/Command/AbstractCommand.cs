using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieboldNixdorfEntryTest.Command.Validation;
using DieboldNixdorfEntryTest.Core;
using DieboldNixdorfEntryTest.Data;
using DieboldNixdorfEntryTest.Entity;
using DieboldNixdorfEntryTest.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DieboldNixdorfEntryTest.Command
{
    enum Parameter
    {
        [field: System.ComponentModel.Description("--help")]
        Help
    }

    abstract class AbstractCommand
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        protected Repository<User> repository = new Repository<User>(Container.ServiceProvider.GetService<AppDbContext>());

        public abstract void Start();

        public T GetInput<T>(string message, InputValidation inputValidation)
        {
            string input;
            bool inputTest;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                inputTest = inputValidation.TestInput(input);
                if (!inputTest)
                {
                    Console.Write("\b");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    for (int i = 0; i < (input == null ? message.Length : message.Length + input.Length); i++)
                    {
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            } while (!inputTest);
            return (T)(object)input;
        }

        public virtual void ProcessParameter(Parameter parameter)
        {
            if (parameter == Parameter.Help)
            {
                Console.WriteLine(Description);
            }
        }

        public bool TryParseParameter(out Parameter? parameter, string value)
        {
            parameter = null;
            var parameters = Enum.GetValues(typeof(Parameter));
            foreach (Parameter param in parameters)
            {
                var description = (System.ComponentModel.DescriptionAttribute)param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0];
                if (description.Description == value)
                {
                    parameter = param;
                    return true;
                }
            }
            return false;
        }
    }
}
