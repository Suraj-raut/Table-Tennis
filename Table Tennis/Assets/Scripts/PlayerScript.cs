using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 3.0f;
    private float force = 3.0f;
	
	
    [SerializeField]
    private Transform aimTarget;
	
	[SerializeField]
    private Transform ball;
	
	private Animator animator;
	
	Vector3 aimTargetInitialPos;
	private Touch touch;
	
	

   	void Start()
	{
		animator = GetComponent<Animator>();
		animator.Play("idle");
		aimTargetInitialPos = aimTarget.position;
	}
    void Update()
    {
		//Keyboard movement
		
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		if(h != 0 || v != 0 )
		{
			transform.Translate(new Vector3(v, 0, h) * speed * Time.deltaTime);
		}
		
		aimTarget.Translate(new Vector3(0, 0, -h) * speed * Time.deltaTime);
		
		
		//Touch movement
		if(Input.touchCount > 0)
		{
			touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Moved)
			{
				transform.Translate(new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y) * speed * Time.deltaTime);
			}
		}
		
    }
	
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ball"))
		{
			Vector3 dir = aimTarget.position - transform.position;
			Vector3 offset = new Vector3(0, 1f, 0);
			
			other.transform.position = Vector3.SmoothDamp(other.transform.position, aimTarget.position, ref dir,force * Time.deltaTime);
			other.GetComponent<Rigidbody>().velocity = (dir.normalized * force) + Vector3.up * offset.y;
			
			Vector3 ballDir = ball.position - transform.position;
			
			if(ballDir.z >= 0 )
			{
				animator.Play("RightSwing");
			}
			else
			    animator.Play("LeftSwing");
			
			aimTarget.position = aimTargetInitialPos;
		}
	}
}
