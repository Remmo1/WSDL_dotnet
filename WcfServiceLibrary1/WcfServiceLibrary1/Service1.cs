using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
     [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple
        )]
    public class MyCalculator : ICalculator
    {

        public int iAdd(int val1, int val2)
        {
            int toReturn = checked(val1 + val2);
            Console.WriteLine("Method iAdd(" + val1 + ", " + val2 + ") returns: " + toReturn);
            return toReturn;
        }

        public int iSub(int val1, int val2)
        {
            int toReturn = checked(val1 - val2);
            Console.WriteLine("Method iSub(" + val1 + ", " + val2 + ") returns: " + toReturn);
            return toReturn;
        }

        public int iMul(int val1, int val2)
        {
            int toReturn = checked(val1 * val2);
            Console.WriteLine("Method iMul(" + val1 + ", " + val2 + ") returns: " + toReturn);
            return toReturn;
        }

        public int iDiv(int val1, int val2)
        {
            int toReturn = checked(val1 / val2);
            Console.WriteLine("Method iDiv(" + val1 + ", " + val2 + ") returns: " + toReturn);
            return toReturn;
        }

        public int iMod(int val1, int val2)
        {
            int toReturn = checked(val1 % val2);
            Console.WriteLine("Method iMod(" + val1 + ", " + val2 + ") returns: " + toReturn);
            return toReturn;
        }

        public async Task<int> HMultiplyAsync(int val1, int val2)
        {
            Console.WriteLine("Running method HMultiplyAsync");
            int toReturn = checked(val1 * val2);
            Console.WriteLine("Method iMul(" + val1 + ", " + val2 + ") returns: " + toReturn);
            Thread.Sleep(500);
            return toReturn;
        }

        /*public async Task<(int, int)> FindPrimesBetweenAsync(int val1, int val2)
        {
            (int amount, int highest) toReturn = (0, 0);
            if (val2 < val1 || val1 < 0)
                throw new Exception();
            bool isPrime;
            for (int i = val1; i <= val2; i++)
            {
                if (i < 2)
                    continue;
                isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    toReturn.amount++;
                    toReturn.highest = i;
                }
            }
            Console.WriteLine("Metoda FindPrimesBetweenAsync(" + val1 + ", " + val2 + "), zwraca: " + toReturn);
            return toReturn;
        }*/

        public async Task<(int, int)> FindPrimesBetweenAsync(int val1, int val2)
        {
            Console.WriteLine("Wywołanie metody FindPrimesBetweenAsync");

            if (val2 < val1 || val1 < 0)
                throw new Exception();

            var prime = new bool[val2 + 1];
            for (var i = 0; i < prime.Length; i++)
                prime[i] = true;

            prime[0] = false;
            prime[1] = false;

            for (var p = 2; p * p <= val2; p++)
            {
                if (prime[p])
                {
                    for (var i = p * p; i <= val2; i += p)
                        prime[i] = false;
                }
            }

            var amount = 0;
            var highest = -1;
            for (var p = val1; p <= val2; p++)
            {
                if (prime[p])
                {
                    amount++;
                    highest = p;
                }
            }
            Console.WriteLine("Metoda FindPrimesBetweenAsync(" + val1 + ", " + val2 + "), zwraca: ({0},{1})", amount, highest);
            return (amount, highest);
        }

    }
}
