using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary1;
using MyDataClient;

namespace WcfServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyData.info();

            //Krok i URI dla bazowego adresu serwisu
            Uri baseAddress = new Uri("http://localhost:7777/WcfServiceLibrary1");
            //Krok 2 Instancja serwisu
            ServiceHost myHost = new ServiceHost(typeof(MyCalculator), baseAddress);
            //Krok 3 Endpoint serwisu
            BasicHttpBinding myBinding = new BasicHttpBinding();
            ServiceEndpoint endpoint1 = myHost.AddServiceEndpoint(typeof(ICalculator), myBinding, "endpoint1");
            //Krok 4 Ustawienie metadanych
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            myHost.Description.Behaviors.Add(smb);
            try
            {
                //dodanie kolejnego endpointu
                WSHttpBinding binding2 = new WSHttpBinding();
                binding2.Security.Mode = SecurityMode.None;
                ServiceEndpoint endpoint2 = myHost.AddServiceEndpoint(typeof(ICalculator), binding2, "endpoint2");
                Console.WriteLine("\n-->Endpoints:");
                EndpointInfo(endpoint1);
                EndpointInfo(endpoint2);
                //Krok 5 Uruchomienie serwisu
                myHost.Open();
                Console.WriteLine("\nService is started and running.");
                Console.WriteLine("Press <ENTER> to STOP service...");
                Console.WriteLine();
                Console.ReadLine(); // aby nie konczyc natychmiast
                myHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Exception occured: {0}", ce.Message);
                myHost.Abort();
            }
        }
        static void EndpointInfo(ServiceEndpoint endpoint)
        {
            Console.WriteLine("\nService endpoint {0}:", endpoint.Name);
            Console.WriteLine("Binding: {0}", endpoint.Binding.ToString());
            Console.WriteLine("ListenUri: {0}", endpoint.ListenUri.ToString());
        }
    }
}
