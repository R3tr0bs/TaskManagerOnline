//Created By EBS
//License - MIT
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TMNAddons
{
    /// <summary>
    /// This class will be the Client.
    /// functions:
    /// Connect(ipAddr, port) - this function connect to the server on ipAddr:port. returns bool.
    /// Disconnect() - this function will disconnect from the server. returns bool.
    /// SendData(data) - this function will send the data(string) to the server. returns bool.
    /// GetData() - this function will get data from from the server. returns string.
    /// </summary>
    public static class Client
    {
        
        public static TcpClient client = null;
        public static bool Connect(string ipAddr, Int32 port)
        {
            //this function will connect to ipAddr:port
            bool ret = false;
            try
            {
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(ipAddr), port));
                ret = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = false;
            }
            return ret;
        }
        public static bool SendData(string data)
        {
            //this function will send data to server.
            //data - the string you want to send to the server.
            bool ret = false;
            if (client == null)
            {
                return ret;
            }
            try
            {
                byte[] msg = Encoding.Unicode.GetBytes(TMEAddons.Encryption.Encrypt(data));
                NetworkStream stream = client.GetStream();
                stream.Write(msg, 0, msg.Length);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = false;
            }
            return ret;
        }
        public static string GetData()
        {
            //this function will get data from the server
            //returns string
            string ret = "-1";
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[1024];
                Int32 msgBytes = stream.Read(data, 0, data.Length);
                ret = Encoding.Unicode.GetString(data, 0, msgBytes);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = "-1";
            }
            return TMEAddons.Encryption.Decrypt(ret);

        }
        public static bool Disconnect()
        {
            //this function will disconnect from the server.
            //returns true if everything went well,
            //otherwise returns false.
            bool ret = false;
            try
            {
                client.Close();
                ret = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = false;
            }
            return ret;
        }
    }
    /// <summary>
    /// This class will be the server.
    /// functions:
    /// StartServer(port, ipAddr) - this function will start the server on ipAddr(string) using the port(Int32). returns bool.
    /// StopServer() - this function will stop the server. returns bool.
    /// GetConnection() - this function will get connection. returns TcpClient(needs: using System.Socket.Net).
    /// SendData(data, send) - this function will send the data(string) to send(TcpClient). returns bool.
    /// GetData(from) - this function will get data from from(TcpClient). returns string.
    /// ExecuteComamnd(command, commandFrom) - this function will execute the command(string) and will send a state about the command to commandFrom(TcpClient)
    /// </summary>
    public static class Host
    {
        private static int BUFFER_SIZE = 1024;
        public static TcpListener server;
        public static bool StartServer(string ipAddr, Int32 port)
        {
            //this will start the server ipAddr:port.
            //if everything will go well then it will return true,
            //otherwise it will return false.
            bool ret = false;
            try
            {
                IPAddress servAddr = IPAddress.Parse(ipAddr);
                server = new TcpListener(servAddr, port);
                server.Start();
                ret = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = false;
            }
            return ret;


        }
        public static bool StopServer()
        {
            //this will stop the server.
            //if everything will go properly it will return true,
            //otherwise it will return false.
            bool ret = false;
            try
            {
                server.Stop();
                ret = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = false;
            }
            return ret;
        }
        public static TcpClient GetConnection()
        {
            //This is when the server gets the client connection.
            //if he returns null then there is no connection.
            //else it will returns the client as TcpClient.
            TcpClient ret = null;
            try
            {
                ret = server.AcceptTcpClient();

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
            return ret;
        }
        public static bool SendData(string data, TcpClient send)
        {
            //this function will send data to the tcp client
            //*PARAMETERS*
            //send - the client you want to send the data
            //*END*
            bool ret = false;
            try
            {
                byte[] end = Encoding.Unicode.GetBytes(TMEAddons.Encryption.Encrypt("STOP"));
                byte[] msg = Encoding.Unicode.GetBytes(TMEAddons.Encryption.Encrypt(data));
                NetworkStream sendStream = send.GetStream();
                int sent = 0;
                sendStream.Write(msg, 0, msg.Length);
                ret = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = false;
            }
            return ret;

        }
        public static string GetData(TcpClient get)
        {
            //this function will get data from the tcp client
            //*PARAMETERS*
            //get - the client you want to get data from
            //*END*
            string ret = "-1";
            try
            {
                NetworkStream stream = get.GetStream();
                byte[] data = new byte[1024];
                Int32 dat = stream.Read(data, 0, data.Length);
                ret = System.Text.Encoding.Unicode.GetString(data, 0, dat);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ret = "-1";
            }
            return TMEAddons.Encryption.Decrypt(ret);
        }
        public static void ExecuteCommand(string command, TcpClient commandFrom) 
        {
            //this function will execute the commands.
            //the commands is written like this XXXYYY...
            //XXX - this will be the main command,
            //YYY... - Optional, if the function need any other parameter (Process ID/name, Regedit folder/key/name/value and so on)
            //commandFrom - this is the client you got the function from to send him if the command executed properly.
            if(command.Substring(0,3) == "GAP") 
            {
                string str = TMPAddons.Processes.GetAllRunningProcesses();
                SendData(str, commandFrom);
                return;
            }
            //command Process Kill ID and then the id of the process
            if(command.Substring(0,3) == "PKI")
            {
                bool done = TMPAddons.Processes.KillProcessById(int.Parse(command.Substring(2,command.Length-3)));
                if (done) SendData("DONE", commandFrom);
                else SendData("ERROR", commandFrom);
                return;
            }
            //command Process Kill Name and then the name of the process.
            if(command.Substring(0,3) == "PKN") 
            {
                bool done = TMPAddons.Processes.KillProcessesByName(command.Substring(2, command.Length - 3));
                if (done) SendData("DONE", commandFrom);
                else SendData("ERROR", commandFrom);
                return;
            }
        }
    }
}
