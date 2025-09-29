using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameView gameView;
    public PlayerModel playerModel;
    public GameAPI gameAPI;

    void Start()
    {
        gameAPI = gameObject.AddComponent<GameAPI>();
        gameView.SetRegisterButtonListener(OnRegisterButtonClicked);
        gameView.SetLoginButtonListener(OnLoginButtonClicked);
    }

    public void OnRegisterButtonClicked()
    {
        string playerName = gameView.playerNameInput.text;
        StartCoroutine(gameAPI.RegisterPlayer(playerName, "1234"));
    }

    public void OnLoginButtonClicked()
    {
        string playerName = gameView.playerNameInput.text;
    }

    private IEnumerator LoginPlayerCoroutine(string playerName, string password)
    {
        yield return gameAPI.LoginPlayer(playerName, password, player =>
        {
            playerModel = player;
            UpdateResourcesDisplay();
        });
    }

    public void UpdateResourcesDisplay()
    {
        if(playerModel != null)
        {
            gameView.SetPlayerName(playerModel.playerName);
            gameView.UpdateResources(playerModel.metal, playerModel.crystal, playerModel.deuterium);
        }
    }

    void Update()
    {
        
    }
}
