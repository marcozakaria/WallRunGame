using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI coinsCollectedText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] Button playButton;
    [SerializeField] GameObject mainMenuButtons;
    [SerializeField] GameObject deathMenu;
    [SerializeField] Animator transitionAnimator;

    [Header("MusicButton")]
    [SerializeField] Image musicButttonImage;
    [SerializeField] Sprite musicOn, musicOff;

    [Header("Player")]
    [SerializeField] PlayerController player;

    public bool isPlaying = false;

    [HideInInspector] public int score, highScore, coinsCollected;

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

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        highScore = playerPrefManager.GetHighScore();
        coinsCollected = playerPrefManager.GetMoney();
        coinsCollectedText.text = coinsCollected.ToString();

        // highScoreText.text = highScore.ToString();
    }

    private void Update()
    {
        if (isPlaying)
        {
            scoreText.text = (score++).ToString();
        }
    }

    public void CoinCollected(int value)
    {
        coinsCollected += value;
        coinsCollectedText.text = coinsCollected.ToString();
    }

    public void OnGameOver()
    {
        isPlaying = false;
        player.gameObject.SetActive(false);
        CameraShake.instance.ShakeIt();
        deathMenu.SetActive(true);

        if (score > highScore)
        {
            highScore = score;
            playerPrefManager.SaveHighScore(highScore);
            playerPrefManager.SaveCoins(coinsCollected);
            //highScoreText.text = highScore.ToString();
        }
    }

    #region UI Buttons
    public void OnPlayButtonPressed()
    {
        playButton.gameObject.SetActive(false);
        mainMenuButtons.SetActive(false);
        isPlaying = true;
        coinsCollected = score = 0;
        player.StartGame();
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

    public void OnRetryButton()
    {
        transitionAnimator.SetTrigger("close");
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(0);
    }

    #endregion
}
