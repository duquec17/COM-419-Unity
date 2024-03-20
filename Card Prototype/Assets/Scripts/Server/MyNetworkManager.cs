using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkBehaviour
{
    //Starts server (Host + Client)
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        //Tells us that the server is running
        Debug.Log("OnStartServer was activated");
    }

    [Server]
    public override void OnStopServer()
    {
        base.OnStopServer();

        //Tells us that the server is running
        Debug.Log("OnStartServer was deactivated");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //Message to show that client is running; appears in console tab
        Debug.Log("OnStartClient was activated");
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        //Message to show that client is not running; appears in console tab
        Debug.Log("OnStartClient was deactivated");
    }
}
