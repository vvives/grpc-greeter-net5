/*
 * Copyright (c) 2021 Víctor Vives - All rights reserved.
 * 
 * Licensed under the MIT License. 
 * See LICENSE file in the project root for full license information.
 */

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Grpc.Core;
using Grpc.Net.Client;

using static GrpcGreeter.Greeter;

namespace GrpcGreeter.Client
{
    /// <summary>
    /// The main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static async Task Main(string[] args)
        {
            // Return true to allow certificates that are untrusted/invalid.
            HttpClientHandler httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // This is the default host and port.
            using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });


            GreeterClient client = new GreeterClient(channel);

            HelloRequest request = new HelloRequest
            {
                Name = args[0]
            };

            HelloReply reply = await client.SayHelloAsync(request);

            Console.WriteLine(reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
