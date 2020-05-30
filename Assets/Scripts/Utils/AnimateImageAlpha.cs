using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateImageAlpha : MonoBehaviour
{
    [SerializeField] float timeTo = 0.5f;
    [SerializeField] Image image;

    private void OnEnable()
    {
        StartCoroutine(AnimateImageA());
    }

    IEnumerator AnimateImageA()
    {
        float time = 0;
        while (time < timeTo)
        {
            time += Time.unscaledDeltaTime;
            image.color = Color.Lerp(Color.clear, Color.white, time / timeTo);
            yield return null;
        }
        time = 0;
        while (time < timeTo)
        {
            time += Time.unscaledDeltaTime;
            image.color = Color.Lerp(Color.white, Color.clear, time / timeTo);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
