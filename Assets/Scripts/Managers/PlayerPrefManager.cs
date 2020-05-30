using UnityEngine;

public class PlayerPrefManager 
{
    private readonly string highScore = "highScore";
    private readonly string moneyCollected = "moneyCollected";

    public void SaveHighScore(int value)
    {
        PlayerPrefs.SetInt(highScore, value);
        PlayerPrefs.Save();
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highScore);
    }
}
