using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockController : MonoBehaviour
{
	private Vector3 startScale;
	public float growSpeed = 1;

    // Start is called before the first frame update
    void Start() {
        startScale = this.transform.GetChild(0).transform.localScale;
		this.transform.GetChild(0).transform.localScale *= 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
		if(this.transform.GetChild(0).transform.localScale.x < startScale.x*1.1f){
			this.transform.GetChild(0).transform.localScale *= 1 + (Time.timeScale * growSpeed);
		}else{
			this.transform.GetChild(0).transform.localScale = startScale;
			this.enabled = false;
		}
    }
}
