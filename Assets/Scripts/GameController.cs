using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float defaultAltura;
    public GameObject[] players;
    public GameObject[] huds;
	public float[] coolDown;
	public GameObject effXpl;
	public GameObject deadPlayer;
	private int roundWinner = -1;
	private DDOL ddol;

    // Start is called before the first frame update
    void Start()
    {
        defaultAltura = this.transform.position.y;
		ddol = GameObject.FindObjectOfType<DDOL>();

		for(int i = 0; i < players.Length; i++){
			huds[i].GetComponentInChildren<Text>().text = "" + ddol.score[i];
		}

		setPlayers();
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        float midPos = 0;
		Vector2 targetPos = Vector2.zero;
        for (int i = 0; i < players.Length; i++){
			if(players[i].gameObject.activeSelf){
				midPos += players[i].transform.position.y+2;
			}
		}
        midPos /= players.Length;

        if(roundWinner != -1){
			targetPos = players[roundWinner].transform.position;
		}else{
            targetPos = new Vector2(this.transform.position.x, midPos);
        }

		if(targetPos.y < defaultAltura)
			targetPos = new Vector2(targetPos.x, defaultAltura);

		targetPos = Vector2.Lerp(this.transform.position, targetPos, 5 * Time.deltaTime);
		this.transform.position = new Vector3(targetPos.x, targetPos.y, this.transform.position.z);
    }
	
    public void kill(int toPerson) {
		if(!players[toPerson].gameObject.GetComponent<PlayerController>().DEAD){
			players[toPerson].gameObject.GetComponent<PlayerController>().DEAD = true;
			players[toPerson].gameObject.SetActive(false);
			Instantiate(effXpl, players[toPerson].transform.position, transform.rotation);
			this.GetComponent<CamShakeSimple>().Shake(0.45f, 0.225f);
			
			GameObject deadP = Instantiate(deadPlayer, players[toPerson].transform.position, deadPlayer.transform.rotation);
			deadP.transform.localScale = players[toPerson].transform.localScale;
			deadP.GetComponent<Animator>().SetTrigger("dead_" + toPerson);
			deadP.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150));

			int remain = 0;
			foreach(GameObject p in players){
				if(p.gameObject.activeSelf)
					remain++;
			}
			foreach(GameObject p in players){
				if(p.gameObject.activeSelf && remain <= 1 && roundWinner==-1){
					roundWinner = p.GetComponent<PlayerController>().ID;
					StartCoroutine(win());
				}
			}
		}

    }
    public IEnumerator win() {
        ddol.score[roundWinner]++;
		this.GetComponent<Camera>().orthographicSize *= 0.85f;
		huds[roundWinner].GetComponentInChildren<Text>().text = "" + ddol.score[roundWinner];
		yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
    }

	public void setSlider(int ID, float val, float over){
		huds[ID].GetComponent<Slider>().value = val/over;
	}

	public void setPlayers(){
		for(int i = 0; i < players.Length; i++){
			if(ddol.playerEnabled[i]){
				players[i].gameObject.GetComponent<PlayerController>().DEAD = false;
				players[i].gameObject.SetActive(true);
			}else{
				players[i].gameObject.GetComponent<PlayerController>().DEAD = true;
				players[i].gameObject.SetActive(false);
			}
		}	
	}

	public KeyCode[] assignControls(int ID){
		if (ID == 0){
            return new KeyCode[] {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftControl};
		} if (ID == 1){
            return new KeyCode[] {KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.RightControl};
		} else if (ID == 2){
			return new KeyCode[] {KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F3};
		} else { //if (ID == 3){
			return new KeyCode[] {KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F7};
		}
	}
}
