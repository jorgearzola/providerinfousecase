using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProviderInfoModel;
using ProviderInfoService.Respository;

namespace ProviderInfoTests
{
    [TestFixture]
    public class SFDocumentTest
    {
        [SetUp]
        public void SetUp()
        {
            //we can add set up code here.
        }

        public byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                FileMode.Open,
                FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }


        [Test]
        public void upload_oneDocument_success_integration()
        {
            /*
                I try to use a simple approach to testing.
                Monitor your output and compare to your desire output.
                The difference is an error signal which we can then test against.
            */

            //Arrange
            UnicodeEncoding unic = new UnicodeEncoding();
            SFDocument sfDoc = new SFDocument();
            sfDoc.Document = FileToByteArray("README.txt");
            sfDoc.FileName = "15009";
            sfDoc.Stamp = DateTime.Now;
            sfDoc.Type = "pdf";
            sfDoc.User = new SFUser() { Username = "jarzola@gmail.com", Password = "Wu?ewr5Teb" };
            SFDocumentsRepository repo = new SFDocumentsRepository(sfDoc);

            //Act
            bool result = repo.upload();

            //Assert
            Assert.IsTrue(result);



        }
    }
}
