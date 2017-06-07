using System;
using System.Linq;
using System.Runtime;
using System.Runtime.Remoting;
using NUnit.Framework;
using ProviderInfoService;
using ProviderInfoService.Respository;

namespace ProviderInfoTests
{
    [TestFixture]
    public class ProviderInfoServiceTests
    {

        [SetUp]
        public void SetUp()
        {
            
        }


        [Test]
        public void limitCount100_providerInfo_get()
        {
            
            /*
                I try to use a simple approach to testing.
                Monitor your output and compare to your desire output.
                The difference is an error signal which we can then test against.
            */
            //using AAA approach
            //Arrange
            ProviderInfoRepository repository = new ProviderInfoRepository();
            int limit = 100;
            
            //Act
            var response = repository.Get(limit);
            
            //Assert
            Assert.IsTrue(response.Count() <= limit);



        }
    }
}