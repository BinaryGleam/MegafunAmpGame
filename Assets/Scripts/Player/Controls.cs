using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HorizontalMovement()
	{
        float horizontal = Input.GetAxisRaw("Horizontal");
    }
}
