using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherAI : MonoBehaviour
{

    public float speed = 1f;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    //setting 'groundCheck' as butcher sprite's child to lauch the Raycast from its position
    public Transform groundCheck;

    bool isFacingRight = true;

    RaycastHit2D hit;
    private Transform piggy;

    //Enumeration of the differents states which the butcher can be in.
    public enum BUTCHER_STATE {
        PATROL,
        IDLE,
        CHASE,
        SEARCH
    }
    public BUTCHER_STATE butcher_state;

    void Start(){
    // Finding piggy gameObject inside the game
        piggy = GameObject.FindGameObjectWithTag("Piggy").transform;
    }

    void Update() {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
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
                break;
        }
    }
        
    // I used a Raycast directed to the down to detect ground and flip position et continue patrolling
    private void Patrol() {
        if(hit.collider != false ){
            if(isFacingRight){
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }else{
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else{
            isFacingRight =!isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        }
    }
    // The butcher is starting chasing piggy by moving from his current position toward piggy position  
    private void Chase(){
        transform.position = Vector2.MoveTowards(this.transform.position, piggy.position, speed * 1.5f * Time.deltaTime);
    }
}
