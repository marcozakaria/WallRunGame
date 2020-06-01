using System.Collections;
using UnityEngine;

public class UIExpand : MonoBehaviour
{
	[SerializeField] float duration;
	[SerializeField] float delay;
	[SerializeField] Vector3 toSize;

	private WaitForSeconds delayWait;

	private void OnEnable()
	{
		delayWait = new WaitForSeconds(delay);
		StartCoroutine(StartResize());
	}

	IEnumerator StartResize()
	{
		Vector3 originalScale = transform.localScale;

		while (true)
		{
			float timeCounter = 0;
			while (timeCounter < duration)
			{
				timeCounter += Time.deltaTime;
				transform.localScale = Vector3.Lerp(originalScale, toSize, timeCounter / duration);
				yield return null;
			}

			timeCounter = 0;
			while (timeCounter < duration)
			{
				timeCounter += Time.deltaTime;
				transform.localScale = Vector3.Lerp(toSize, originalScale, timeCounter / duration); 
				yield return null;
			}

			yield return delayWait;
		}
	}
}
