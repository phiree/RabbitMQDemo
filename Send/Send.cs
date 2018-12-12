﻿using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Send
    {
        static void Main(string[] args)
        {
            var factory=new ConnectionFactory(){HostName="localhost"};
			using(var connection=factory.CreateConnection())
			{
				using(var channel=connection.CreateModel())
				{
					channel.QueueDeclare(
						queue:"hello",
						durable:false,
						exclusive:false,
						autoDelete:false,
						arguments:null
					);
					string message="hello,world";
					var body=Encoding.UTF8.GetBytes(message);
					channel.BasicPublish(
						exchange:"",
						routingKey:"hello",
						basicProperties:null,
						body:body
					);
					Console.WriteLine("{message} Sent");
					Console.ReadLine();
				}
				
			}
        }
    }
}
