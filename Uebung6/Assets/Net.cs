using UnityEngine;
using System.Collections;

public static class Net
{
	public const string AS_CLIENT = "client";
	public const string AS_SERVER = "server";
	public const int hostPort = 25001;
	public static string hostIp = "127.0.0.1";
	
	
	public static void Connect(string role) {
		if(role == AS_CLIENT) {
			Network.Connect(hostIp, hostPort);
		} else {
			// TODO: set # connections properly
			Network.InitializeServer(32, hostPort, false);	
		}
		
	}
}


