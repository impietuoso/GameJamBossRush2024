using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameText;
    public PlayerAttacks pa;
    public Boss b;

    private void Awake() {
        instance = this;
    }

    public void GameWin() {
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
        endGameText.text = "You Win";
        pa.enabled = false;
        b.enabled = false;
    }

    public void Lose() {
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
        endGameText.text = "Game Over";
        pa.enabled = false;
        b.enabled = false;
    }
}
