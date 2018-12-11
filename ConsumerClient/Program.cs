using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DelayedEcho;
using System.Linq;

namespace ConsumerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var processClass = new ProcessClass();
            processClass.MessageDelayed += ProcessClass_MessageDelayed1;
            
            string line = string.Empty;
            var outputStringDic = new Dictionary<int, string>();

            Input:
            line = Console.ReadLine();

            if (line != string.Empty)
            {
                if (!line.Contains(':'))
                {
                    Console.WriteLine("Invalid entry... Your entry should be in the following format string:number");
                }
                else
                {
                    var splitArray = line.Split(':');
                    if (splitArray.Length == 2)
                    {
                        if (!string.IsNullOrEmpty(splitArray[1]))
                        {
                            var iTemp = 0;

                            if (int.TryParse(splitArray[1].Trim(), out iTemp))
                            {
                                outputStringDic.Add(iTemp, splitArray[0].Trim());
                            }
                            else
                            {
                                Console.WriteLine("Invalid entry : " + splitArray[1].Trim());
                            }
                        }
                    }
                    goto Input;
                }                
            }
            else
            {
                foreach (var delayMessage in outputStringDic)
                {
                    Task.Run(()=>processClass.EchoDelayed(delayMessage.Value, delayMessage.Key));
                }
            }

            Console.ReadKey();
        }

        private static async Task ProcessClass_MessageDelayed1(string message, EventArgs e)
        {
            Console.WriteLine(message);
            return;
        }        
    }
}
