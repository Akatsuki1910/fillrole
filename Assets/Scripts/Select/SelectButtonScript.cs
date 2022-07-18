using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButtonScript : MonoBehaviour
{
    private string number;

    public void OnClick()
    {
        number = this.GetComponentInChildren<Text>().text;
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("GameScene");
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var gameManager = GameObject.FindWithTag("GameCanvas").GetComponent<GameScript>();
        gameManager.number = number;
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
