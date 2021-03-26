using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherAI : MonoBehaviour
{

    int i = 0;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 8f;
    public float searchSpeed = 5f;
    public AudioSource breath = null;
    public AudioSource scream = null;
    public AudioSource step = null;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform groundCheck; //setting 'groundCheck' as butcher sprite's child to lauch the Raycast from its position
    private float distance;
    public float howClose = 4f;
    bool isFacingRight = true;
    RaycastHit2D hit;
    private Transform piggyTrans;
    private GameObject piggyGO;
    private bool didScreamOnce = false;
    public bool hidden;

    private Animator buAnimator = null;

    //Enumeration of the differents states which the butcher can be in.
    public enum BUTCHER_STATE {
        PATROL,
        IDLE,
        CHASE,
        SEARCH,
        NOTHING
    }
    public static BUTCHER_STATE butcher_state;
    public BUTCHER_STATE butcher_stateTest;

    private float look = 1f;

    private void Awake()
	{
        buAnimator = GetComponent<Animator>();
        if (buAnimator == null)
            Debug.LogError("No animator on butcher");
        if (breath == null)
            Debug.LogError("Reference breath audiosource to butcher script");
        if (step == null)
            Debug.LogError("Reference breath audiosource to butcher script");
    }

	void Start()
    {
    // Finding piggy gameObject inside the game
        piggyTrans = GameObject.FindGameObjectWithTag("Player").transform;
        piggyGO = GameObject.FindGameObjectWithTag("Player");
        for(i = 3; i < 8; i++){
            Physics2D.IgnoreLayerCollision(i,10);

        }
        butcher_state = BUTCHER_STATE.PATROL;

    }

    void Update() {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 100f, groundLayers);
        switch (butcher_state)
        {
            case BUTCHER_STATE.PATROL:
                Patrol();
                didScreamOnce = false;
                buAnimator.Play("Walk");
                break;
            case BUTCHER_STATE.IDLE:
                Idle();
                didScreamOnce = false;
                buAnimator.Play("Idle");
                break;
            case BUTCHER_STATE.CHASE:
                Chase();            
                buAnimator.Play("Run");
                break;
            case BUTCHER_STATE.SEARCH:    
                Search();            
                didScreamOnce = false;
                buAnimator.Play("Run");
                break;
            case BUTCHER_STATE.NOTHING:
                nothing();
                buAnimator.Play("Idle");
                break;
        }
        distance = Vector3.Distance(piggyTrans.position, transform.position);
        if(distance <= howClose && !hidden){
            if(didScreamOnce == false)
			{
                PlayScream();
                didScreamOnce = true;
			}
            butcher_state = BUTCHER_STATE.CHASE;
        }
        Debug.Log(butcher_state);

        if (piggyGO.activeSelf == false){
            hidden = true;
        }
        else{
            hidden = false;
        }
        if (butcher_state == BUTCHER_STATE.PATROL)
        {
            if (rb.velocity.x < -0.5f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (rb.velocity.x > 0.5f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
    
        
    // I used a Raycast directed to the down to detect ground and flip position et continue patrolling
    private void Patrol() {
        
        if(hit.collider != false ){
            if(isFacingRight){
                rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            }
        }
        else{
            butcher_state = BUTCHER_STATE.IDLE;
            isFacingRight =!isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Invoke("setPatrolMode", 2);
        }
    }
    // The butcher is starting chasing piggy by moving from his current position toward piggy position  
    private void Chase(){
        // the butcher stops chasing when to close and stay next to piggy
        lookPiggy();
        if (Vector3.Distance(piggyTrans.position, transform.position) >= 1.5 && hit.collider != false) {
            transform.position = Vector2.MoveTowards(this.transform.position, piggyTrans.position, chaseSpeed * Time.deltaTime);
        }
    // If the butcher is in chasing mode and suddenly he can't see piggy because she is hidden , the butcher continue chasing in that direction before getting back to the idle mode before going to patrol mode
        if (hidden){
            butcher_state = BUTCHER_STATE.IDLE;
            Invoke("setPatrolMode", 2);
          

        }
    // If Piggy is so far of butcher , we can say that she escape so the butcher get back the idle mode for few second and switch to the patrol mode
        if (Vector3.Distance(piggyTrans.position, transform.position) >= 10f){
            butcher_state = BUTCHER_STATE.IDLE;
            Invoke("setPatrolMode", 2);

        }
    }
    private void Search(){
        lookPiggy();
        // the butcher get toward the area where the signal is triggered inside his own area    
        if (hit.collider != false ){
            transform.position = Vector2.MoveTowards(this.transform.position, TriggerPoint.position, searchSpeed * Time.deltaTime);            
        }
    // When the butcher is on the triggered area and he notices no changes he get back to patrol mode
        if (Vector3.Distance(TriggerPoint.position, transform.position) < 1.5) {
            Invoke("setPatrolMode", 2);

        }

    }
    // When the butcher is in the idle mode he stop going further and the corresponding animation is setted 
    private void Idle(){
        rb.velocity = new Vector2(0f, 0f);
    }
    void setPatrolMode(){
        butcher_state = BUTCHER_STATE.PATROL;
    }

    public void PlayBreath()
	{
        breath.Play();
	}

    public void PlayScream()
    {
        breath.Stop();
        scream.Play();
    }

    public void PlayStep()
	{
        step.Play();
	}

    void lookPiggy()
    {
        if (butcher_state != BUTCHER_STATE.PATROL && butcher_state != BUTCHER_STATE.IDLE)
        {
            if (transform.position.x - piggyTrans.position.x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void nothing()
    {

    }

    public void Stop()
	{
        butcher_state = BUTCHER_STATE.NOTHING;
        rb.velocity = Vector2.zero;
	}

    public void Restart()
    {
        butcher_state = BUTCHER_STATE.PATROL;
    }
}
