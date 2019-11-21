using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCloudMove : MonoBehaviour
{
    public float desp = 3f;
    public float speed = 0.3f;
	public bool followXcam = false;
	private float offsetcam = 0;

    // Update is called once per frame
    void Update()
    {
		if(followXcam)
			offsetcam = GameObject.FindObjectOfType<Camera>().gameObject.transform.position.x;

        this.gameObject.transform.position = new Vector2((Mathf.Sin(Time.time * speed) * desp) + offsetcam, this.gameObject.transform.position.y);
	}
}
