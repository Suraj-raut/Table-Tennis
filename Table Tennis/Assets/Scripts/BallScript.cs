using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
   Vector3 initailPos;
   Rigidbody rb;
	
	
    void Start()
    {
        initailPos = transform.position;
		rb = GetComponent<Rigidbody>();
		
    }
	
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.CompareTag("Walls"))
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = initailPos;
		}
	}

    
}
