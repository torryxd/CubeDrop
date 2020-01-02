using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    private GameController gc;
    private DDOL ddol;

    // Start is called before the first frame update
    void Start()
    {
		gc = GameObject.FindObjectOfType<GameController>();
		ddol = GameObject.FindObjectOfType<DDOL>();

		for(int i = 0; i < 4; i++){
			gc.players[i].gameObject.SetActive(false);
		}

		Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 4; i++){
			for(int u = 0; u < 5; u++){
				if(Input.GetKeyDown(gc.assignControls(i)[u]) && !gc.players[i].activeSelf){
					gc.players[i].gameObject.SetActive(true);
					ddol.playerEnabled[i] = true;
				}
			}
		}
    }

	public void gotoScene(string name){
		SceneManager.LoadScene(name);
	}
}
