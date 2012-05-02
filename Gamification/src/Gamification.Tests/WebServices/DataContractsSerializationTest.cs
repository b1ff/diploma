using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Gamification.Core.Extensions;
using Gamification.WebServices.ServicesContracts;
using NUnit.Framework;

namespace Gamification.Testing.Unit.WebServices
{
    [TestFixture]
    public class DataContractsSerializationTest
    {
        [Test]
        public void SerializeAllDataContractsTest()
        {
            var dataContracts = typeof(IActionsService).Assembly.GetTypes().Where(x => x.GetAttribute<DataContractAttribute>() != null && !x.IsAbstract);

            foreach (var dataContract in dataContracts)
            {
                Debug.WriteLine(string.Format("Try serialize {0}", dataContract.FullName));
                var contract = Activator.CreateInstance(dataContract);
                Utility.ContractObjectToXml(contract);
            }
        }

        public static class Utility
        {

            public static string ContractObjectToXml<T>(T obj)
            {
                DataContractSerializer dataContractSerializer = new DataContractSerializer(obj.GetType());

                String text;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    dataContractSerializer.WriteObject(memoryStream, obj);
                    byte[] data = new byte[memoryStream.Length];
                    Array.Copy(memoryStream.GetBuffer(), data, data.Length);
                 
                    text = Encoding.UTF8.GetString(data);
                }

                return text;

            }

        }
    }
}
