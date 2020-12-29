using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Threading;

namespace TMRETest
{
    [TestClass]
    public class TestTMRE
    {
        private int passed;
        private TcpClient client;
        private string data = "-1";
        [TestMethod]
        public void TestRegEdit()
        {
            const string SUPPOSE = "Software\nSystem\n";
            const string SUPPOSE_LOC = @"HKEY_CURRENT_CONFIG\System\";
            const string SUPPOSE_LOC2 = @"HKEY_CURRENT_CONFIG\Software\Fonts\";
            TMREAddons.RegEdit.MainFolder("HKEY_CURRENT_CONFIG");
            string test1 = TMREAddons.RegEdit.GetSubFolder();
            Assert.AreEqual(SUPPOSE, test1);
            //TMREAddons.RegEdit.GetIntoFolder("System");
            //string test2 = TMREAddons.RegEdit.GetCurrentFolder();
            //Assert.AreEqual(SUPPOSE_LOC, test2);
            TMREAddons.RegEdit.GetIntoFolder("Software");
            TMREAddons.RegEdit.GetIntoFolder("Fonts");
            string test3 = TMREAddons.RegEdit.GetCurrentFolder();
            Assert.AreEqual(SUPPOSE_LOC2, test3);
            Dictionary<string, object> test4 = TMREAddons.RegEdit.GetAllKeysFromCurLoc();
            string key = "";
            TMREAddons.RegEdit.CreateKey("test", 12);
            foreach (string str in test4.Keys) key += str + "\n";
            Assert.AreEqual("LogPixels\n", key);
            test1 = TMREAddons.RegEdit.GetBackOneFolder();
            Assert.AreEqual(@"Software\", test1);

        }
        [TestMethod]
        public void TestEncryption() 
        {
            string test1 = "abc";
            string test2 = "123fsa";
            string test3 = "abhfas";
            string test4 = "abcdefghijklmnopqrtuvwxyz";

            Assert.AreEqual(test1, TMEAddons.Encryption.Decrypt(TMEAddons.Encryption.Encrypt(test1)));
            Assert.AreEqual(test2, TMEAddons.Encryption.Decrypt(TMEAddons.Encryption.Encrypt(test2)));
            Assert.AreEqual(test3, TMEAddons.Encryption.Decrypt(TMEAddons.Encryption.Encrypt(test3)));
            Assert.AreEqual(test4, TMEAddons.Encryption.Decrypt(TMEAddons.Encryption.Encrypt(test4)));
        }
        [TestMethod]
        public void TestNetwork() 
        {
            bool test = TMNAddons.Host.StartServer("127.0.0.1", 10000);
            Assert.IsTrue(test);
            string message = "hello";
            Thread testThread = new Thread(ServerSide);
            testThread.Start();
            test = TMNAddons.Client.Connect("127.0.0.1",10000);
            Assert.IsTrue(test);
            passed = -1;
            do
            {
                //waiting that the connection will astablish
            } while (data == "-1");
            passed = 1;
            Assert.IsNotNull(client);
            
            TMNAddons.Client.SendData(message);
            passed = -1;
            do
            {
                //waiting for the data to come;
            } while (data == "-1");
            passed = 1;
            Assert.AreEqual(data, message);
            test = TMNAddons.Client.Disconnect();
            Assert.IsTrue(test);
            test = TMNAddons.Host.StopServer();
            Assert.IsTrue(test);
            
        }
        private void ServerSide() 
        {
            data = "-1";
            client = TMNAddons.Host.GetConnection();
            data = "";
            do
            {

            } while (passed == -1);
            data = "-1";
            data = TMNAddons.Host.GetData(client);
            
        }
    }
}
