using Capl.Authorization;
using Capl.Authorization.Matching;
using Capl.Authorization.Operations;
using Capl.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.CodeAccess.PolicyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader();
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
            string policyId = "http://www.example.org/policy/codeaccess";
            AddPolicy(policyId, "manager");
            Console.WriteLine("Policy written to service.");
            Console.WriteLine();

            bool run = true;
            while(run)
            {
                Console.Write("Change Policy (Y/N) ? ");
                if("y" == Console.ReadLine().ToLower())
                {
                    Console.Write("Enter role ? ");
                    string role = Console.ReadLine();
                    AddPolicy(policyId, role);
                    Console.WriteLine("Policy written to service.");
                }
                else
                {
                    Console.WriteLine("Exit Policy Manager (Y/N) ? ");
                    run = Console.ReadLine().ToLower() != "y";
                }
            }
        }

        static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(@"--  **_____******_*_************");
            Console.WriteLine(@"--  *|**__*\****|*(_)***********");
            Console.WriteLine(@"--  *|*|__)*|__*|*|_**___*_***_*");
            Console.WriteLine(@"--  *|**___/*_*\|*|*|/*__|*|*|*|");
            Console.WriteLine(@"--  *|*|**|*(_)*|*|*|*(__|*|_|*|");
            Console.WriteLine(@"--  *|_|***\___/|_|_|\___|\__,*|");
            Console.WriteLine(@"--  ***********************__/*|");
            Console.WriteLine(@"--  **********************|___/*");
            Console.WriteLine(@"--  **__**__***********************************");
            Console.WriteLine(@"--  *|**\/**|**********************************");
            Console.WriteLine(@"--  *|*\**/*|*__*_*_*__***__*_**__*_**___*_*__*");
            Console.WriteLine(@"--  *|*|\/|*|/*_`*|*'_*\*/*_`*|/*_`*|/*_*\*'__|");
            Console.WriteLine(@"--  *|*|**|*|*(_|*|*|*|*|*(_|*|*(_|*|**__/*|***");
            Console.WriteLine(@"--  *|_|**|_|\__,_|_|*|_|\__,_|\__,*|\___|_|***");
            Console.WriteLine(@"--  ****************************__/*|**********");
            Console.WriteLine(@"--  ***************************|___/***********");
            Console.ResetColor();





















        }

        static void AddPolicy(string policyId, string role)
        {
            AuthorizationPolicy policy = CreatePolicy(policyId, role);
            WebServiceStore store = new WebServiceStore("http://localhost:1926/api/policy");
            store.SetPolicy(policy);
        }

        static AuthorizationPolicy CreatePolicy(string policyId, string role)
        {
            EvaluationOperation operation = new EvaluationOperation(EqualOperation.OperationUri, role);
            Match match = new Match(LiteralMatchExpression.MatchUri, "http://www.example.org/claims/role");
            Rule rule = new Rule(match, operation);
            return new AuthorizationPolicy(rule, new Uri(policyId));
        }
    }
}
