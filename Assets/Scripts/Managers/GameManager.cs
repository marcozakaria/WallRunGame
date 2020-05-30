using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI coinsCollectedText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [Header("MusicButton")]
    [SerializeField] Image musicButttonImage;
    [SerializeField] Sprite musicOn, musicOff;

    public bool isPlaying = false;

    private int score, highScore, coinsCollected;

    private PlayerPrefManager playerPrefManager = new PlayerPrefManager();
    private bool isMusicOff = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        highScore = playerPrefManager.GetHighScore();
       // highScoreText.text = highScore.ToString();
    }

    public void CoinCollected(int value)
    {
        coinsCollected += value;
    }

    public void OnGameOver()
    {
        isPlaying = false;

        if (score > highScore)
        {
            highScore = score;
            playerPrefManager.SaveHighScore(highScore);
            highScoreText.text = highScore.ToString();
        }
    }

    #region UI Buttons
    public void OnPlayButtonPressed()
    {
        isPlaying = true;
        coinsCollected = score = 0;
    }

    public void OnShareButtonPressed()
    {
        new NativeShare().SetTitle("Try This Cool Game").
        SetText("I Scored : " + highScore + " Points in the game \n How mush could you Score? \n ").Share();
    }

    public void OnRateButtonPressed()
    {
        Application.OpenURL("");
    }

    public void OnMusicButtonPressed()
    {
        if (isMusicOff)
        {
            isMusicOff = false;
            AudioManager.instance.PlayMainMusic();
            musicButttonImage.sprite = musicOn;
        }
        else
        {
            isMusicOff = true;
            AudioManager.instance.StopMainMusic();
            musicButttonImage.sprite = musicOff;
        }
    }

    #endregion
}
