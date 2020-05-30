using System.Collections;
using UnityEngine;

public class CanvasGroubAlphaAnim : MonoBehaviour
{
    [SerializeField] float timeAll = 1f;
    [SerializeField] CanvasGroup canvasGroup;

    private void OnEnable()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(AlphaFade(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(AlphaFade(1f, 0f));
    }

    IEnumerator AlphaFade(float from, float to)
    {
        float time = 0f;
        while (time <= timeAll)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, time / timeAll);
            yield return null;
        }
    }
}
