﻿using System;
using IEC60870.SAP;

namespace TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var client = new ClientSAP("127.0.0.1", 2404);
                var server = new ServerSAP("127.0.0.1", 2405);               

                client.NewASdu += asdu => {
                    Console.WriteLine(asdu);
                    server.SendASdu(asdu);                    
                };

                client.ConnectionClosed += e =>
                {
                    Console.WriteLine(e);
                };

                client.Connect();

                server.StartListen(100);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

    }
}