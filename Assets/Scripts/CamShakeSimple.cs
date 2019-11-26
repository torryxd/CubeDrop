using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour
{
	public void Shake(float magnitude, float duration){
		StartCoroutine(Freeze());
		StartCoroutine(justShake(magnitude, duration));
	}

    public IEnumerator justShake(float magnitude, float duration)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPosition + new Vector3(x, y, -10f);
            elapsed += Time.unscaledDeltaTime;
            yield return 0;
        }
        transform.position = originalPosition;
    }

	public IEnumerator Freeze()
    {
		int frames = 2;
		while (frames > 0) {
            Camera.main.clearFlags = CameraClearFlags.Nothing;
            yield return null;
            Camera.main.cullingMask = 0;
            frames--;
        }
 
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.cullingMask = ~0;
    }
}
