using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatadorDeRocas : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Ground")
			Destroy(col.gameObject);
    }
}
