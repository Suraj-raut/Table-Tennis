using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
   private Vector3 initailPos;
   private Rigidbody rb;
   public AnimationCurve yCurve;
   private float timeLapsed = 0;	
	
	
    void Start()
    {
        initailPos = transform.position;
		rb = GetComponent<Rigidbody>();
		
    }
	
	void FixedUpdate()
	{
		timeLapsed += Time.deltaTime; 
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.CompareTag("Walls"))
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = initailPos;
		}
		
		if(collision.transform.CompareTag("Player") ||  collision.transform.CompareTag("Bot"))
		{
			GetComponent<Rigidbody>().MovePosition(new Vector3 (0, transform.position.y + yCurve.Evaluate(timeLapsed), 0));
		}
	}

    
}
