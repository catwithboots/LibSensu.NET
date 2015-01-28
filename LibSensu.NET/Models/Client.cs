using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace LibSensu.NET.Models
{
    public class Client
    {
        public string sndProt { get; set; }

        public int srcPort { get; set; }
        public string dstHostname { get; set; }
        public int dstPort { get; set; }
        public byte[] jsonbyte { get; set; }

        // Send objects over UDP to a listening client (in this case Sensu client instead of direct RabbitMQ)
        public static async Task<string> SendUdp(int srcPort, string dstIp, int dstPort, byte[] jsonbyte)
        {
            UdpClient udpClient = new UdpClient(srcPort);
            await udpClient.SendAsync(jsonbyte, jsonbyte.Length, dstIp, dstPort);
            return "Send async status code to dstIp (UDP)";
        }



        // Send objects over TCP to a listening client (in this case Sensu client instead of direct RabbitMQ)
        public static void SendTcp(string dstIp, int dstPort, byte[] jsonbyte)
        {
            TcpClient tcpClient = new TcpClient(dstIp, dstPort);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(jsonbyte,0,jsonbyte.GetLength(0));
            networkStream.Close();
            return;
        }


    }

    public class Send : Client
    {
        public static async void udp(int srcPort, string dstIP, int dstPort, byte[] jsonbyte)
        {
            await SendUdp(srcPort, dstIP, dstPort, jsonbyte);
        }

        public static void tcp(string dstIp, int dstPort, byte[] jsonbyte)
        {
            SendTcp(dstIp, dstPort, jsonbyte);
            return;
        }
    }
}

