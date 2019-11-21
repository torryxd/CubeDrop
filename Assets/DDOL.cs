using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    public bool[] playerEnabled = new bool[]{false,false,false,false};
    public int[] score = new int[] {0,0,0,0};

	private static DDOL ddolInstance;
    void Awake() {
        DontDestroyOnLoad(this.gameObject);

		if (ddolInstance == null) {
			ddolInstance = this;
		} else {
			Destroy(this.gameObject);
		}
    }
}
