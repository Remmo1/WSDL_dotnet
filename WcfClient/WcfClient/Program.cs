using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;
using WcfClient.ServiceReference2;
using WcfServiceLibrary1;
using MyDataClient;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Messaging;

namespace WcfClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyData.info();

            Console.WriteLine("... The client is started");
            //Step 1: Create client proxy based on communication channel
            
            //base address
            Uri baseAddress;
            //binding, address, endpoint address
            BasicHttpBinding myBinding = new BasicHttpBinding();
            baseAddress = new Uri("http://localhost:7777/WcfServiceLibrary1/endpoint1");
            EndpointAddress eAddress = new EndpointAddress(baseAddress);
            //channel factory
            ChannelFactory<ServiceReference2.ICalculator> myCF = new ChannelFactory<ServiceReference2.ICalculator>(myBinding, eAddress);
            //client proxy (here MyClient) based on channel
            ServiceReference2.ICalculator myClient = myCF.CreateChannel();
                //second endpoint
            ServiceReference2.CalculatorClient myClient2 = new ServiceReference2.CalculatorClient("WSHttpBinding_ICalculator1");
            
            //Step 2: service operations call
            while(true)
            {
                Console.WriteLine("\n========== MAIN MENU =============");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("===      [1] -> Add              ===");
                Console.WriteLine("===      [2] -> Subtract         ===");
                Console.WriteLine("===      [3] -> Multiply         ===");
                Console.WriteLine("===      [4] -> Divide           ===");
                Console.WriteLine("===      [5] -> Modulo           ===");
                Console.WriteLine("===      [6] -> PrimesAsync      ===");
                Console.WriteLine("===      [7] -> MultiplyAsync    ===");
                Console.WriteLine("===      [8] -> Exit             ===");
                Console.WriteLine("------------------------------------");
                Console.Write("Option: ");
                string choice = Console.ReadLine();
                int choiceInt;
                bool choiceCorrect = Int32.TryParse(choice, out choiceInt);
                if (choiceCorrect)
                {
                    int a, b;
                    bool exitLoop = false;
                    int result;
                    try
                    {
                        switch (choiceInt)
                        {
                            case 1:
                                readValues(out a, out b, "Add");
                                Console.Write("...calling Add (for endpoint1) - ");
                                result = myClient.iAdd(a, b);
                                Console.WriteLine("Result = " + result);
                                break;
                            case 2:
                                readValues(out a, out b, "Subtract");
                                Console.Write("...calling Subtract (for endpoint1) - ");
                                result = myClient.iSub(a, b);
                                Console.WriteLine("Result = " + result);
                                break;
                            case 3:
                                readValues(out a, out b, "Multiply");
                                Console.Write("...calling Multiply (for endpoint1) - ");
                                result = myClient.iMul(a, b);
                                Console.WriteLine("Result = " + result);
                                break;
                            case 4:
                                readValues(out a, out b, "Divide");
                                Console.Write("...calling Divide (for endpoint1) - ");
                                result = myClient.iDiv(a, b);
                                Console.WriteLine("Result = " + result);
                                break;
                            case 5:
                                readValues(out a, out b, "Modulo");
                                Console.Write("...calling Modulo (for endpoint1) - ");
                                result = myClient.iMod(a, b);
                                Console.WriteLine("Result = " + result);
                                break;
                            case 6:
                                readValues(out a, out b, "FindPrimesBetween(from, to)");
                                Console.WriteLine("2...calling FindPrimesBetween ASYNCHRONOUSLY !!!");
                                FindPrimesBetweenWrapper(a, b, myClient2);
                                break;
                            case 7:
                                readValues(out a, out b, "HMultiplyAsync");
                                Console.WriteLine("2...calling HMultiplyAsync ASYNCHRONOUSLY !!!");
                                HMultiplyWrapper(a, b, myClient2);
                                break;
                            case 8:
                                exitLoop = true;
                                break;
                            default:
                                Console.WriteLine("...choice was not one of available options, try again");
                                break;
                        }
                    }
                    catch (System.ServiceModel.FaultException fe)
                    {
                        Console.WriteLine("\n!!!...given values led to integer overflow or are undefined");
                        Console.WriteLine("!!!...aborting the operation and going back to the menu");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("!!!...something went wrong, going back to menu");
                        //Console.WriteLine(e.ToString());
                    }

                    if(exitLoop) 
                    { 
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("...choice was not an integer, try again");
                    continue;
                }
            }

        //Step 3: Closing the client - closes connection and clears resources.
            Console.WriteLine("...press <ENTER> to STOP client...");
            Console.WriteLine();
            Console.ReadLine(); //to not finish app immediately*/
            ((IClientChannel)myClient).Close();
            Console.WriteLine("...Client closed - FINISHED");
        }

        static void readValues(out int a, out int b, string operation)
        {
            string sVal1, sVal2;
            int val1, val2;
            Console.Write("...please, provide first value for method \'{0}\': ", operation);
            sVal1 = Console.ReadLine();
            bool goodValueGiven = Int32.TryParse(sVal1, out val1);
            if (!goodValueGiven)
            {
                Console.WriteLine("!!!...Oops! Given value is not correct");
                throw new Exception();
            }
            a = val1;
            Console.Write("...please, provide second value for method \'{0}\': ", operation);
            sVal2 = Console.ReadLine();
            goodValueGiven = Int32.TryParse(sVal2, out val2);
            if (!goodValueGiven)
            {
                Console.WriteLine("!!!...Oops! Given value is not correct");
                throw new Exception();
            }
            b = val2;
        }

        static async Task<int> callHMultiplyAsync(int val1, int val2, ServiceReference2.CalculatorClient client)
        {
            Console.WriteLine("2......called callHMultiplyAsync");
            int reply = await client.HMultiplyAsync(val1, val2);
            Console.WriteLine("2......finished HMultipleAsync");
            return reply;
        }

        static async Task<(int, int)> callFindPrimesBetweenAsync(int n1, int n2, ServiceReference2.CalculatorClient client)
        {
            Console.WriteLine("2......called FindPrimesBetweenAsync");
            (int, int) reply = await client.FindPrimesBetweenAsync(n1, n2);
            Console.WriteLine("2......finished FindPrimesBetweenAsync");
            return reply;
        }

        private static async Task HMultiplyWrapper(int n1, int n2, ServiceReference2.CalculatorClient client)
        {
            int res = await callHMultiplyAsync(n1, n2, client);
            Console.WriteLine("2...HMultiplyAsync Result = " + res);
        }
        private static async Task FindPrimesBetweenWrapper(int n1, int n2, ServiceReference2.CalculatorClient client)
        {
            (int amount, int highest) res = await callFindPrimesBetweenAsync(n1, n2, client);
            Console.WriteLine("2...in a given range there are {0} primes, and the biggest one is {1}", res.amount, res.highest != 0 ? res.highest.ToString() : "non-existant");
        }
    }
}
