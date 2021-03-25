using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherAI : MonoBehaviour
{

    int i = 0;
    public float patrolSpeed = 1f;
    public float chaseSpeed = 8f;
    public float searchSpeed = 5f;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    //setting 'groundCheck' as butcher sprite's child to lauch the Raycast from its position
    public Transform groundCheck;
    private float distance;
    public float howClose = 2.5f;
    bool isFacingRight = true;

    RaycastHit2D hit;
    private Transform piggyTrans;
    private GameObject piggyGO;
    public GameObject patrolPoint;
    public bool hidden;

    //Enumeration of the differents states which the butcher can be in.
    public enum BUTCHER_STATE {
        PATROL,
        IDLE,
        CHASE,
        SEARCH,
        BACKPATROLLING
    }
    public static BUTCHER_STATE butcher_state;

    void Start(){
    // Finding piggy gameObject inside the game
        piggyTrans = GameObject.FindGameObjectWithTag("Player").transform;
        piggyGO = GameObject.FindGameObjectWithTag("Player");
        patrolPoint = GameObject.FindGameObjectWithTag("PatrolPoint");

        for(i = 3; i < 8; i++){
            Physics2D.IgnoreLayerCollision(i,10);

        }
        butcher_state = BUTCHER_STATE.BACKPATROLLING;

    }

    void Update() {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 100f, groundLayers);
        switch (butcher_state)
        {
            case BUTCHER_STATE.PATROL:
                Patrol();
                break;
            case BUTCHER_STATE.IDLE:
                break;
            case BUTCHER_STATE.CHASE:
                Chase();
                break;
            case BUTCHER_STATE.SEARCH:
                Search();
                break;
            case BUTCHER_STATE.BACKPATROLLING:
                transform.position = Vector2.MoveTowards(this.transform.position, patrolPoint.transform.position, patrolSpeed * 2f * Time.deltaTime);
                break;
        }
        distance = Vector3.Distance(piggyTrans.position, transform.position);
        if(distance <= howClose && !hidden){
            butcher_state = BUTCHER_STATE.CHASE;
        }
        Debug.Log(butcher_state);

        if(butcher_state == BUTCHER_STATE.BACKPATROLLING && hit.collider == true){
            butcher_state = BUTCHER_STATE.PATROL;
        }

        if (piggyGO.activeSelf == false){
            hidden = true;
        }
        else{
            hidden = false;
        }

        // if (hidden){
        //     Physics2D.IgnoreLayerCollision(8,10);
        // }
        // else{
        //     Physics2D.IgnoreLayerCollision(8,10,false);
        // }


        
    }
    
        
    // I used a Raycast directed to the down to detect ground and flip position et continue patrolling
    private void Patrol() {
        if(hit.collider != false ){
            if(isFacingRight){
                rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            }else{
                rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            }
        }
        else{
            isFacingRight =!isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    // The butcher is starting chasing piggy by moving from his current position toward piggy position  
    private void Chase(){
        // the butcher stops chasing when to close and stay next to piggy
        if (Vector3.Distance(piggyTrans.position, transform.position) >= 1.5) {
            transform.position = Vector2.MoveTowards(this.transform.position, piggyTrans.position, chaseSpeed * Time.deltaTime);
        }

        if (hidden){
            Invoke("setBackPatrolMode", 2);
          

        }

        if (Vector3.Distance(piggyTrans.position, transform.position) >= 10f){
            setBackPatrolMode();
        }
    }
    private void Search(){
        if(butcher_state != BUTCHER_STATE.BACKPATROLLING)
         {
             transform.position = Vector2.MoveTowards(this.transform.position, TriggerPoint.position, searchSpeed * Time.deltaTime);
         }
   
    }
    void setBackPatrolMode(){
        butcher_state = BUTCHER_STATE.BACKPATROLLING;
    }
}
