using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections.Generic;

public class MainServer : NetworkManager {

	private NetworkManager manager;

	private string localIP;
	private string publicIP;
	private int mainPort = 5000;

    void Start ()
    {
		manager = GetComponent<NetworkManager>();
        StartServer ();
	}

	// ------- SERVER -------
	private void StartServer()
    {
		GetIPAddresses ();
		manager.networkPort = mainPort;
		manager.networkAddress = localIP;

		manager.StartServer ();
	}

	public override void OnStartServer()
    {
		base.OnStartServer ();
		Debug.Log ("Main Server started [Port: " + mainPort + "]");
	}
		
	private void GetIPAddresses()
    {
		localIP = Network.player.ipAddress;
		publicIP = new System.Net.WebClient().DownloadString("http://icanhazip.com"); 
	}

	// ------- Client Handling -------
	public override void OnServerConnect(NetworkConnection conn)
    {	
		base.OnServerConnect (conn);
		Debug.Log("Client " + conn.connectionId + " Connected");
	}

	public override void OnServerDisconnect(NetworkConnection conn)
    {	
		base.OnServerDisconnect (conn);
		Debug.Log("Client " + conn.connectionId + " Disconnected");
	}

}
