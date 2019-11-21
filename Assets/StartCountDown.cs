using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountDown : MonoBehaviour
{
	public Text txtCountdown;

    // Start is called before the first frame update
    void Start() {
		StartCoroutine(initialCountdown());  
    }
	
    IEnumerator initialCountdown() {
		Time.timeScale = 0;
		StartCoroutine(this.GetComponent<CamShakeSimple>().Shake(0.325f, 0.2f));
		txtCountdown.text = "3";
		txtCountdown.transform.GetChild(0).GetComponent<Text>().text = "3";
		yield return new WaitForSecondsRealtime(1);
		StartCoroutine(this.GetComponent<CamShakeSimple>().Shake(0.325f, 0.2f));
		txtCountdown.text = "2";
		txtCountdown.transform.GetChild(0).GetComponent<Text>().text = "2";
		yield return new WaitForSecondsRealtime(1);
		StartCoroutine(this.GetComponent<CamShakeSimple>().Shake(0.325f, 0.2f));
		txtCountdown.text = "1";
		txtCountdown.transform.GetChild(0).GetComponent<Text>().text = "1";
		yield return new WaitForSecondsRealtime(1);
		StartCoroutine(this.GetComponent<CamShakeSimple>().Shake(0.325f, 0.2f));
		txtCountdown.text = "GO";
		txtCountdown.transform.GetChild(0).GetComponent<Text>().text = "GO";
		Time.timeScale = 1;
		yield return new WaitForSecondsRealtime(1);
		txtCountdown.text = "";
		txtCountdown.transform.GetChild(0).GetComponent<Text>().text = "";
    }
}
