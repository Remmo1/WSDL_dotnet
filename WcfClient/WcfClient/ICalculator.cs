using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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
    }
}

