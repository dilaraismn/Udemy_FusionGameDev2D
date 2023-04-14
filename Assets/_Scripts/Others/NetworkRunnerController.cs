using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;

    public event Action OnStartedRunnerController;
    public event Action OnPlayerJoinedSucessfully;
    
    private NetworkRunner networkRunnerInstance;


    public void ShutDownRunner()
    {
        networkRunnerInstance.Shutdown();
    }
    
    public async void StartGame(GameMode mode, string roomName)
    {
        OnStartedRunnerController?.Invoke();
        if (networkRunnerInstance == null)
        {
            networkRunnerInstance = Instantiate(networkRunnerPrefab);
        }
        
        networkRunnerInstance.AddCallbacks(this); // in order for these callbacks to be called
        networkRunnerInstance.ProvideInput = true; // means that player records and sends inputs to the server

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            PlayerCount = 4,
            SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>()
        };

        var result = await networkRunnerInstance.StartGame(startGameArgs);
        
        if (result.Ok)
        {
            const string SCENE_NAME = "MainGame";
            networkRunnerInstance.SetActiveScene(SCENE_NAME);
        }
        else
        {
            print($"Failed To Start {result.ShutdownReason}");
        }
    }
    
   
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        print("OnPlayerJoined");
        OnPlayerJoinedSucessfully?.Invoke();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        print("OnPlayerLeft");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        print("OnInput");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        print("OnInputMissing");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        print("OnShutdown");

        const string LOBBY_SCENE = "Lobby";
        SceneManager.LoadScene(LOBBY_SCENE);
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        print("OnConnectedToServer");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        print("OnDisconnectedFromServer");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        print("OnConnectRequest");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        print("OnConnectFailed");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        print("OnUserSimulationMessage");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        print("OnSessionListUpdated");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        print("OnCustomAuthenticationResponse");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        print("OnHostMigration");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        print("OnReliableDataReceived");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        print("OnSceneLoadDone");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        print("OnSceneLoadStart");
    }
}
