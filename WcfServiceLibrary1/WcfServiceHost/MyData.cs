using System;
using System.Net;
using System.Net.Sockets;

namespace MyDataClient
{
    internal class MyData
    {
        public static void info()
        {
            Console.WriteLine("Maria Markowiak, 260417");
            Console.WriteLine("Remigiusz Pisarski, 260364");
            DateTime data = DateTime.Now;
            Console.WriteLine(data.ToString("dd") + " " + data.ToString("MMMM") + ", " + data.ToString("HH") + ":" + data.ToString("mm") + ":" + data.ToString("ss"));
            Console.WriteLine(Environment.Version.ToString());
            Console.WriteLine(Environment.UserName.ToString());
            Console.WriteLine(Environment.OSVersion.ToString());
            var ipv4Address = Array.FindLast(
                                Dns.GetHostEntry(string.Empty).AddressList,
                                a => a.AddressFamily == AddressFamily.InterNetwork);
            Console.WriteLine(ipv4Address);
        }
    }
}
