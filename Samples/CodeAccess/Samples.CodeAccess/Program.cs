using Capl.Authorization;
using Capl.Authorization.Matching;
using Capl.Authorization.Operations;
using Capl.ServiceModel;
using Samples.CodeAccessPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.CodeAccess
{
    class Program
    {
        
        static void Main(string[] args)
        {
            PrintHeader();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            //create an identity and put on current thread
            SetIdentity();
            Console.WriteLine("Remember to set your CAPL policy with the Policy Manager App...");
            Console.ReadKey();

            bool running = true;
            while (running)
            {

                //access the method with Code Access Policy
                long start = DateTime.Now.Ticks;
                try
                {                    
                    AccessMethod();
                    DateTime clock = DateTime.Now.Subtract(new TimeSpan(start));
                    Console.Write("   {0} micro-secs", clock.Ticks / (TimeSpan.TicksPerMillisecond / 1000));
                    Console.WriteLine();
                }
                catch (UnauthorizedAccessException se)
                {
                    DateTime clock = DateTime.Now.Subtract(new TimeSpan(start));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("AccessMethod is {0} {1} micro-secs", se.Message, clock.Ticks / (TimeSpan.TicksPerMillisecond / 1000));
                    Console.ResetColor();
                }
                
                Thread.Sleep(250);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }


        [CaplCodeAccess(SecurityAction.Demand, Action=SecurityAction.Demand, 
            PolicyId = "http://www.example.org/policy/codeaccess", Unrestricted =false)]
        static void AccessMethod()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Method ACCESSED!");
            Console.ResetColor();
        }

        static void SetIdentity()
        {
            List<Claim> claimSet = new List<Claim>();
            claimSet.Add(new Claim("http://www.example.org/claims/name", "Bob"));
            claimSet.Add(new Claim("http://www.example.org/claims/role", "manager"));
            claimSet.Add(new Claim("http://www.example.org/claims/country", "US"));

            ClaimsIdentity identity = new ClaimsIdentity(claimSet);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            Thread.CurrentPrincipal = principal;
        }

        static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"--  ***_____**********_**********************************");
            Console.WriteLine(@"--  **/*____|********|*|**********/\*********************");
            Console.WriteLine(@"--  *|*|*****___***__|*|*___*****/**\***___*___*___**___*");
            Console.WriteLine(@"--  *|*|****/*_*\*/*_`*|/*_*\***/*/\*\*/*__/*__/*_*\/*__|");
            Console.WriteLine(@"--  *|*|___|*(_)*|*(_|*|**__/**/*____*\*(_|*(_|**__/\__*\");
            Console.WriteLine(@"--  **\_____\___/*\__,_|\___|*/_/****\_\___\___\___||___/");
            Console.WriteLine(@"--  *****************************************************");
            Console.WriteLine(@"--  *****************************************************");
            Console.WriteLine(@"--  **_____******_*_************");
            Console.WriteLine(@"--  *|**__*\****|*(_)***********");
            Console.WriteLine(@"--  *|*|__)*|__*|*|_**___*_***_*");
            Console.WriteLine(@"--  *|**___/*_*\|*|*|/*__|*|*|*|");
            Console.WriteLine(@"--  *|*|**|*(_)*|*|*|*(__|*|_|*|");
            Console.WriteLine(@"--  *|_|***\___/|_|_|\___|\__,*|");
            Console.WriteLine(@"--  ***********************__/*|");
            Console.WriteLine(@"--  **********************|___/*");
            Console.ResetColor();

        }
    }
}
