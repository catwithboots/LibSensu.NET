using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using RabbitMQ.Client;


namespace LibSensu.NET.Models
{
    public class Client
    {
        public string sendingProt { get; set; }

        public int srcPort { get; set; }
        public string dstHostname { get; set; }
        public int dstPort { get; set; }
        public byte[] jsonbyte { get; set; }

        public string hostname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string vhost { get; set; }
        public string queue { get; set; }


        // Send objects over UDP to a listening client (in this case Sensu client instead of direct RabbitMQ)
        public static async Task<string> SendUdp(int srcPort, string dstIp, int dstPort, object jsonmessage)
        {
            byte[] jsonbyte = (byte[])jsonmessage;
            UdpClient udpClient = new UdpClient(srcPort);
            await udpClient.SendAsync(jsonbyte, jsonbyte.Length, dstIp, dstPort);
            return "Sent async status code to dstIp (UDP)";
        }
        
        // Send objects over TCP to a listening client (in this case Sensu client instead of direct RabbitMQ)
        public static void SendTcp(string dstIp, int dstPort, object jsonmessage)
        {
            byte[] jsonbyte = (byte[])jsonmessage;
            TcpClient tcpClient = new TcpClient(dstIp, dstPort);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(jsonbyte,0,jsonbyte.GetLength(0));
            networkStream.Close();
            return;
        }

        // Send objects over RMQ to a listening RabbitMQ host. (If you don't want to use the Sensu Client, JSON definition should be interfaced)
        public static bool SendRmq(string hostname, string username, string password, string vhost, string queue, object jsonmessage)
        {
            // Init factory
            ConnectionFactory factory = new ConnectionFactory();
            // Declare RMQ hostname
            factory.HostName = hostname;
            // Declare username used for RMQ
            factory.UserName = username;
            // Declare password user for RMQ
            factory.Password = password;
            // Declare the vhost you will be using
            factory.VirtualHost = vhost;

            // Start the connection
            using (IConnection connection = factory.CreateConnection())
            // Create the Channel
            using (IModel channel = connection.CreateModel())
            {
                // Define you're channel here for Sensu
                channel.QueueDeclare(queue, true, false, false, null);
                // Transform object to byte[]
                byte[] jsonbyte = (byte[])jsonmessage;
                // Send byte[] to RabbitMQ
                channel.BasicPublish("Checks", "key1", null, jsonbyte);

                return true;

            }
        }
    }
}

