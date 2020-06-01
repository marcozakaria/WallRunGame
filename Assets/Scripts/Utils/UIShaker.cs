using System.Collections;
using UnityEngine;

public class UIShaker : MonoBehaviour
{
	[SerializeField] float duration;
	[SerializeField] float delay;
	[SerializeField] float fromAngle;
	[SerializeField] float toAngle;

	private WaitForSeconds delayWait;

	private void OnEnable()
	{
		delayWait = new WaitForSeconds(delay);
		StartCoroutine(StartRotate());
	}

	IEnumerator StartRotate()
	{
		while (true)
		{
			float timeCounter = 0;
			float rotateTime = duration * 0.25f;
			while (timeCounter < rotateTime)
			{
				timeCounter += Time.deltaTime;
				Quaternion currentAngle = transform.rotation;
				transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, Mathf.Lerp(0, fromAngle, timeCounter / rotateTime));
				yield return null;
			}

			timeCounter = 0;
			rotateTime = duration * 0.5f;
			while (timeCounter < rotateTime)
			{
				timeCounter += Time.deltaTime;
				Quaternion currentAngle = transform.rotation;
				transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, Mathf.Lerp(fromAngle, toAngle, timeCounter / rotateTime));
				yield return null;
			}

			timeCounter = 0;
			rotateTime = duration * 0.25f;
			while (timeCounter < rotateTime)
			{
				timeCounter += Time.deltaTime;
				Quaternion currentAngle = transform.rotation;
				transform.rotation = Quaternion.Euler(currentAngle.x, currentAngle.y, Mathf.Lerp(toAngle, 0, timeCounter / rotateTime));
				yield return null;
			}

			yield return delayWait;
		}
	}
}
