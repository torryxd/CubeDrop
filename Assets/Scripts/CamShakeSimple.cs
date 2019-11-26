using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour
{
    public IEnumerator Shake(float magnitude, float duration)
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
}
