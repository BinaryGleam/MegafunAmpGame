using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    protected Vector3 startPosition;
    protected Quaternion startRotation;
    protected Vector3 startScale;

    // Start is called before the first frame update
    protected void Awake()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        startScale = transform.localScale;
    }

	virtual public void CustomReset()
	{
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        transform.localScale = startScale;
    }

}
