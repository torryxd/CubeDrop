using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAt : MonoBehaviour
{
	public float timeToDestroy = 1;

    // Start is called before the first frame update
    void Start(){
        Destroy(this.gameObject, timeToDestroy);
    }
}
