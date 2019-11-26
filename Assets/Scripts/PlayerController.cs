using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ID = 0;
	public bool DEAD = false;
    public float moveForce = 10f;
    public float maxSpeed = 20f;
	[Range(0.9f, 1f)] public float moveFriction = 0.9f;
    public LayerMask whatCanHit;
	public float hitForce = 300;
    public float jumpForce = 15f;
    public float grosorCap = 0.5f;
    public GameObject rockOJB;
    public float chargeCoolDown = 1;
	public int chargeNum = 2;
    public float groundCheckOffSet = 0.3f;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.3f;


    private Rigidbody2D rb;
    private float scaleX;
    private bool grounded = false;
	private bool falling = false;
    private float defaultGravity;
    private GameController gc;
	private Vector2 groundCheckPos;
	private Vector2 pos;
    private KeyCode[] wasd;
	private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
		gc = GameObject.FindObjectOfType<GameController>();
		anim = this.GetComponent<Animator>();

        scaleX = this.transform.localScale.x;
        defaultGravity = rb.gravityScale;
		gc.coolDown[ID] = chargeCoolDown;

        wasd = gc.assignControls(ID);
	}

    // Update is called once per frame
    void Update() {
		pos.x = this.transform.position.x; pos.y = this.transform.position.y;
		
        groundCheckPos = new Vector2(pos.x, pos.y+groundCheckOffSet);
		grounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, whatIsGround);
		anim.SetBool("air", !grounded);

        if (Input.GetKey(wasd[1]) && !Input.GetKey(wasd[3])) {
			this.transform.localScale = new Vector2(-scaleX,this.transform.localScale.y);
			rb.AddForce(new Vector2(-moveForce,0) * Time.deltaTime);
        } if (Input.GetKey(wasd[3]) && !Input.GetKey(wasd[1])) {
			this.transform.localScale = new Vector2(scaleX,this.transform.localScale.y);
			rb.AddForce(new Vector2(moveForce, 0) * Time.deltaTime);
		} else {
			rb.velocity = new Vector2(rb.velocity.x * moveFriction, rb.velocity.y);
		}
		

		if(grounded){
			if (Input.GetKeyDown(wasd[1])){
				golpearBloque(true);
			}
			if(Input.GetKeyDown(wasd[3])){
				golpearBloque(false);
			}

			if (Input.GetKeyDown(wasd[0]) && Time.timeScale > 0) {
				grounded = false;
				rb.velocity = new Vector2(rb.velocity.x, 0);
				rb.AddForce(new Vector2(0, jumpForce));

				Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y-0.5f), 0.1f, whatCanHit);
				for(int i = 0;i < cols.Length;i++)
					cols[i].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-hitForce/2));
				Debug.Log("W" + cols.Length);
			}
		}

        if ((Input.GetKeyDown(wasd[2])||Input.GetKeyDown(wasd[4])) && gc.coolDown[ID] > chargeCoolDown && !Physics2D.OverlapCircle(new Vector2(pos.x, pos.y-0.75f), 0.2f, whatIsGround)) {
            Instantiate(rockOJB, new Vector2(pos.x, pos.y-0.85f), this.transform.rotation);
			StartCoroutine(gc.gameObject.GetComponent<CamShakeSimple>().Shake(0.05f, 0.05f));
            gc.coolDown[ID] -= chargeCoolDown;
        }

        if (gc.coolDown[ID] <= (chargeCoolDown * chargeNum)){
            gc.coolDown[ID] += Time.deltaTime;
			gc.setSlider(ID, gc.coolDown[ID], chargeCoolDown * chargeNum);
		}

		if ((!Input.GetKey(wasd[0]) || rb.velocity.y < 0) && !grounded)
			rb.gravityScale = defaultGravity * 2.5f;
		else
			rb.gravityScale = defaultGravity;

		if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
		anim.SetFloat("speed", (Mathf.Abs(rb.velocity.x)));
    }
    
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            if (col.transform.position.y > pos.y
            && col.transform.position.x > pos.x - grosorCap
            && col.transform.position.x < pos.x + grosorCap) {
				gc.kill(ID);
            }
            else {
                rb.gravityScale = defaultGravity;
            }
        }
		else if (col.gameObject.tag == "Player") {
			Vector2 repel = this.transform.position - col.transform.position;
            rb.AddForce(repel * 350);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Killer") {
			gc.kill(ID);
        }else if (col.gameObject.tag == "Respawner") {
			this.transform.position = new Vector2(0,10);
        }
    }
	
	void golpearBloque(bool leftRight) {
		Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(pos.x+(0.5f*(leftRight?-1:1)), pos.y), 0.1f, whatCanHit);
		for(int i = 0;i < cols.Length;i++){
			cols[i].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hitForce*(leftRight?-1:1),10));
			
			anim.ResetTrigger("kick");
			anim.SetTrigger("kick");
		}
		
		Debug.Log((leftRight?"D":"A") + cols.Length);
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawSphere(new Vector2(pos.x, pos.y-0.75f), 0.2f);
	}
}
