using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameEnd : MonoBehaviour
{
    [SerializeField] private GameObject gameLostCanvas;
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private Button retryWinButton;
    [SerializeField] private Button retryLoseButton;
    [SerializeField] private GameStateChecker gameStateChecker;

    private void Awake()
    {
        retryWinButton.onClick.AddListener(OnRetryWinButtonClicked);
        retryLoseButton.onClick.AddListener(OnRetryLoseButtonClicked);
    }

    private void Start()
    {
        gameStateChecker.OnDefeat += HandleDefeat;
        gameStateChecker.OnVictory += HandleVictory;
        gameWonCanvas.SetActive(false);
        gameLostCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        gameStateChecker.OnDefeat -= HandleDefeat;
        gameStateChecker.OnDefeat -= HandleVictory;
    }

    private void HandleDefeat()
    {
        gameLostCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void HandleVictory()
    {
        gameWonCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void OnRetryWinButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
    private void OnRetryLoseButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");

    }
}