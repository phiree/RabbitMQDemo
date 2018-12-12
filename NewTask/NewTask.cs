﻿using System;
using System.Text;
using RabbitMQ.Client;
namespace NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
          
            var factory=new ConnectionFactory(){HostName="localhost"};
			using(var connection=factory.CreateConnection())
			{
				using(var channel=connection.CreateModel())
				{
					var properties=channel.CreateBasicProperties();
					properties.Persistent=true;
					channel.QueueDeclare(
						queue:"task_queue",
						durable:false,
						exclusive:false,
						autoDelete:false,
						arguments:null
					);
					string message=GetMessage(args);
					var body=Encoding.UTF8.GetBytes(message);
					channel.BasicPublish(
						exchange:"",
						routingKey:"task_queue",
						basicProperties:properties,
						body:body
					);
					Console.WriteLine("{message} Sent");
					Console.ReadLine();
				}
				
			}
        
		}
		private static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}
        
    }
}
