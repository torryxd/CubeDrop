using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class canvasController : MonoBehaviour
{
	public GameObject pauseMenu;
	public Text countdownTxt;
	private DDOL ddol;

	private KeyCode pauseKey = KeyCode.Escape;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
		ddol = GameObject.FindObjectOfType<DDOL>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pauseKey)){
			goPause();
		}
    }

	public void goPause(){
		if(countdownTxt.text == ""){
			if(pauseMenu.activeSelf){
				pauseMenu.SetActive(false);
				Time.timeScale = 1;
			}else{
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
			}
		}
	}
	
	public void Restart(){
		Application.LoadLevel("SelectScene");

		for(int i = 0; i < 4; i++){ //Reset all variables players
			ddol.playerEnabled[i] = false;
			ddol.score[i] = 0;
		}
	}

	public void Quit(){
		Application.Quit();	
	}
}
