using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathMenu : CanvasGroubAlphaAnim
{
    [SerializeField] TextMeshProUGUI hsText;
    [SerializeField] RectTransform scoretext;
    [SerializeField] float timeToCount = 1.5f;
    private float valueToReach;

    private void OnEnable()
    {
        valueToReach = GameManager.instance.highScore;
        StartCoroutine(MoveNumber());
    }

    IEnumerator MoveNumber()
    {
        float time = 0;
        Vector2 sizedeltaXY = scoretext.sizeDelta;
        while (time < timeToCount)
        {
            time += Time.deltaTime;
            scoretext.sizeDelta = Vector2.Lerp(sizedeltaXY, sizedeltaXY * 2, time / (timeToCount / 2f));
            hsText.text = ((int)Mathf.Lerp(0, valueToReach, time / timeToCount)).ToString();
            yield return null;
        }
    }
}
