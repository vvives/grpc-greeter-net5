/*
 * Copyright (c) 2021 Víctor Vives - All rights reserved.
 * 
 * Licensed under the MIT License. 
 * See LICENSE file in the project root for full license information.
 */

using System.Threading.Tasks;

using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter.Service.Services
{
    /// <summary>
    /// The greeter service.
    /// </summary>
    /// <seealso cref="GrpcGreeter.Greeter.GreeterBase" />
    public class GreeterService : Greeter.GreeterBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<GreeterService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreeterService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GreeterService(ILogger<GreeterService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Procedure that greets a user.
        /// </summary>
        /// <param name="request">The request received from the client.</param>
        /// <param name="context">The context of the server-side call handler being invoked.</param>
        /// <returns>
        /// The response to send back to the client (wrapped by a task).
        /// </returns>
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            this.logger.LogInformation($"Received request from {request.Name}.");

            HelloReply reply = new HelloReply
            {
                Message = $"Hello, {request.Name}!"
            };

            this.logger.LogInformation($"Reply sent to {request.Name}.");

            return Task.FromResult(reply);
        }
    }
}
