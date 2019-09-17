using DieboldNixdorfEntryTest.Data;
using DieboldNixdorfEntryTest.Entity;
using DieboldNixdorfEntryTest.Repository;
using DieboldNixdorfEntryTest.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Core
{
    static class Container
    {
        public static ServiceProvider ServiceProvider { get; }

        static Container()
        {
            ServiceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<AppDbContext, AppDbContext>()
                .AddSingleton<ICipher, Aes256>()
                .BuildServiceProvider(); 
        }
    }
}
