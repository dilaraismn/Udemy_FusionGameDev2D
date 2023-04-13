using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("Middle Section Panel Vars")]
    [SerializeField] private Button joinRandomRoomButton;
    [SerializeField] private Button joinRoomByArgsButton;
    [SerializeField] private Button createRoomButton;

    [SerializeField] private TMP_InputField joinRoomByArgsInputField;
    [SerializeField] private TMP_InputField createRoomInputField;

    private NetworkRunnerController networkRunnerController;

    public override void InitPanel(LobbyUIManager UIManager)
    {
        base.InitPanel(UIManager);

        networkRunnerController = GlobalManagers.Instance.networkRunnerController;
        
        joinRandomRoomButton.onClick.AddListener(JoinRandomRoom);
        joinRoomByArgsButton.onClick.AddListener((() => CreateRoom(GameMode.Client, joinRoomByArgsInputField.text)));
        createRoomButton.onClick.AddListener((() => CreateRoom(GameMode.Host, createRoomInputField.text)));
    }

    private void CreateRoom(GameMode mode, string field)
    {
        if (field.Length > 2)
        {
            print("create or join room with args");
            networkRunnerController.StartGame(mode, field);
        }
    }
    
    private void JoinRandomRoom()
    {
        print("join random room");
        networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }
}
