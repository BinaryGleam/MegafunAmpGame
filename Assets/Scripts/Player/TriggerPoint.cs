using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    GameObject butcher;
    public static Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D (Collider2D col){
        if (col.gameObject.tag == "Player"){
            if (ButcherAI.butcher_state != ButcherAI.BUTCHER_STATE.CHASE){
                ButcherAI.butcher_state = ButcherAI.BUTCHER_STATE.SEARCH;
                position = transform.position;
            }

        }
        if (col.gameObject.tag == "Butcher"){
            if (ButcherAI.butcher_state == ButcherAI.BUTCHER_STATE.SEARCH){
                    ButcherAI.butcher_state = ButcherAI.BUTCHER_STATE.IDLE;
                    Invoke("Patrolling", 4);
                }

        }
            
    }


    private void Patrolling(){
        ButcherAI.butcher_state = ButcherAI.BUTCHER_STATE.PATROL;

    }
}
