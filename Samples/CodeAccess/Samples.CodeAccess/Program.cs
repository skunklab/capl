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

            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();

            bool running = true;
            while (running)
            {

                //access the method with Code Access Policy
                try
                {
                    s.Start();
                    AccessMethod();
                    s.Stop();
                    string timeString = GetTimeString(s.ElapsedTicks);
                    Console.ForegroundColor = timeString.Contains("ms") ? ConsoleColor.White : ConsoleColor.Green;
                    Console.WriteLine(timeString);
                }
                catch (UnauthorizedAccessException se)
                {
                    s.Stop();
                    Console.ForegroundColor = ConsoleColor.Red;
                    string timeString = GetTimeString(s.ElapsedTicks);
                    Console.WriteLine("Unauthorized: {0} ", timeString);
                    Console.ResetColor();
                }
                finally
                {
                    s.Reset();
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


        static string GetTimeString(long ticks)
        {
            double value = 0;
            string unit = null;
            long ms = ticks / 10000L;
            TimeSpan ts = new TimeSpan(ticks);
            if(ts.TotalMilliseconds > 1)
            {
                unit = "ms";
                value = ts.TotalMilliseconds;
            }
            else
            {
                unit = "micro-secs";
                value = ticks / (TimeSpan.TicksPerMillisecond / 1000);
            }

            return String.Format("    {0} {1}", value, unit);
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
