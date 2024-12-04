using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicMouse : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
