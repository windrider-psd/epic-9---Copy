using Assets.Source.DataManager;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour, INetworkRunnerCallbacks
{
    public Session session;
    public GameObject sessionPrefab;
    private static GameController _instance;
    private NetworkSceneManagerBase _loader;
    private NetworkRunner _runner;
    private IDataManager dataManager;



    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();
            return _instance;
        }
    }

    public IDataManager DataManager { get => dataManager; private set => dataManager = value; }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Connected to Server");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("Failed to Connect");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("Connect request");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("Custom Auth");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("Disconnected from server");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("On host migration");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
         Debug.Log("Input");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log("Missing input");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player Joined");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player left");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("Reliable Data recieved");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("Scene load done");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("Scene load start");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("session list updated");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("shutdown");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("user simulation message");
    }

    // Start is called before the first frame update

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (_instance != this)
        {
            Destroy(gameObject);
        }

        DataManager = new LocalDataManager();

        DontDestroyOnLoad(gameObject);
        //CreateSession();
    }

    public void CreateSession()
    {
        GameObject go = Instantiate(sessionPrefab, this.transform);
        go.transform.SetParent(this.transform);
        go.AddComponent<NetworkRunner>();
        session = go.GetComponent<Session>();
        session.SetUp();
        
    }

    public Task<bool> JoinIdleLobby()
    {
        return session.JoinIdleLobby();  
    }
    
}