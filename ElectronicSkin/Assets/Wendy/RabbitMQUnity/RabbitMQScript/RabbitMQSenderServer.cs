﻿using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using NccuWise;

public class RabbitMQSenderServer : MonoBehaviour {

	// Settings
	public string serverIP = "wearable.nccu.edu.tw";
	public string channelName = "demo.EXCHANGE1";
	public bool serverAutoStart = false;
	bool isStarted = false;
	bool isReady = false;

	// for RabbitMQ Connection
	ConnectionFactory factory;
	IConnection connection;
	IModel channel;
	
	// Use this for initialization
	void Start ()
	{
		if (serverAutoStart)
			StartServer ();
	}

	void OnGUI () {
		// if no auto start, ask user input
		if (!serverAutoStart) {

			if( isStarted ) return;

			GUILayout.Label("Server IP :");
			serverIP = GUILayout.TextField( serverIP );

			GUILayout.Label("Topic Name :" );
			channelName = GUILayout.TextField( channelName );

			if( GUILayout.Button("Connect") )
			{
				isStarted = true;
				StartServer();
			}
		}
	}
	void StartServer () {

		// connecting to rabbitMQ Server
		factory = new ConnectionFactory() { Uri = "amqp://admin:admin@" + serverIP };
		connection = factory.CreateConnection ();
		channel = connection.CreateModel ();

		// setting channel
		channel.ExchangeDeclare ( channelName , "fanout");
		
		var queueName = channel.QueueDeclare ().QueueName;
		
		channel.QueueBind (queueName, channelName, "");

		isReady = true;
	}

	public bool IsReady ()
	{
		return isReady;
	}
	// Update is called once per frame

	public void SendMessageToServer ( string message )
	{
		channel.BasicPublish( channelName, "", null, Encoding.UTF8.GetBytes( message ) );
	}

}

