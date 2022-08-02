using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Assets.source.DataManager;

public class Session : NetworkBehaviour
{

    private NetworkRunner _runner;

    

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
    }

    public override void Spawned()
    {
        base.Spawned();
        
    }


    public async Task Connect()
    {
        
        var result = await _runner.JoinSessionLobby(SessionLobby.Custom, "wah");

        if (!result.Ok)
        {
            Debug.Log("hi");

        }
        else
        {
            Debug.Log("hello");
            //StartGame(GameMode.Client);
        }

        

    }
    async void StartGame(GameMode mode)
    {

        //mode = GameMode.Client;
        // Create the Fusion runner and let it know that we will be providing user input
        //_runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
        });


    }

    public async Task<bool> JoinIdleLobby()
    {   
        Debug.Log(_runner);
        var result = await _runner.JoinSessionLobby(SessionLobby.Custom, "idle");

        return result.Ok;
        
    }


    void Start()
    {
        
        //Debug.Log("Hello");
        //Connect();
        //StartGame(GameMode.Client);

      

    }

    public void SetUp()
    {
        _runner = GetComponent<NetworkRunner>();
        _runner.AddCallbacks(GameController.Instance);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
