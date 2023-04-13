using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateNicknamePanel : LobbyPanelBase
{
    [Header("Create Nickname Panel Vars")]
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private Button createNicknameButton;

    private const int MAX_CHAR_FOR_NICKNAME = 2;


    public override void InitPanel(LobbyUIManager lobbyUIManager)
    {
        base.InitPanel(lobbyUIManager);
        createNicknameButton.interactable = false;
        createNicknameButton.onClick.AddListener(OnClickCreateNickname);
        InputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string value)
    {
        createNicknameButton.interactable = value.Length > MAX_CHAR_FOR_NICKNAME;
    }

    private void OnClickCreateNickname()
    {
        var nickName = InputField.text;
        if (nickName.Length > MAX_CHAR_FOR_NICKNAME)
        {
            base.ClosePanel();
            lobbyUIManager.ShowPanel(LobbyPanelType.MiddleSectionPanel);
        }
    }
}
