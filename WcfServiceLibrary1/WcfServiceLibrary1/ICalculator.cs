using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    public interface ICalculator
    {
        [OperationContract]
        int iAdd(int val1, int val2);

        [OperationContract]
        int iSub(int val1, int val2);

        [OperationContract]
        int iMul(int val1, int val2);

        [OperationContract]
        int iDiv(int val1, int val2);

        [OperationContract]
        int iMod(int val1, int val2);

        [OperationContract]
        Task<int> HMultiplyAsync(int val1, int val2);

        [OperationContract]
        Task<(int, int)> FindPrimesBetweenAsync(int val1, int val2);
    }
}
