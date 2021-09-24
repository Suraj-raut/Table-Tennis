using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour                                ///---Game Object orientation x - forward/backward
{                                                                         //                           y -  Up / Down
    private float speed = 3.0f;                                           //                           z - left/ Right
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
		animator = GetComponent<Animator>();                          //--Player Bat Animation--//
		aimTargetInitialPos = aimTarget.position;                     //--aimTarget position is fixed at center after hitting--//
	}
    void Update()
    {
		//Keyboard movement
		
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		if(h != 0 || v != 0 )                                       //---if we are moving using keyboard keys(a,s,d,w)---//
		{
			transform.Translate(new Vector3(v, 0, h) * speed * Time.deltaTime);  //---Move the player v-fornt/back : h-left/right//
		}
		
		aimTarget.Translate(new Vector3(0, 0, -h) * speed * Time.deltaTime); //--Move the aim target of player in z dir : left/right
		
		
		//Touch movement
		if(Input.touchCount > 0)                         //--If we touch the screen--//
		{
			touch = Input.GetTouch(0);                   //--Touch count number = touch number--//
			if(touch.phase == TouchPhase.Moved)         // --Is finger moving--//
			{
				transform.Translate(new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y) * speed * Time.deltaTime);
			}                                            //--Move the object as per touch movement--//
		}
		
    }
	
	
	private void OnTriggerEnter(Collider other)                                  //---Player Bat collision--//
	{
		if(other.CompareTag("Ball"))                                             //--If the ball collided with bat--//                       
		{
			Vector3 dir = aimTarget.position - transform.position;              //--Distance betwn target postion & Player position
			Vector3 offset = new Vector3(0, 1f, 0);
			
			other.transform.position = Vector3.SmoothDamp(other.transform.position, aimTarget.position, ref dir,force * Time.deltaTime);
			other.GetComponent<Rigidbody>().velocity = (dir.normalized * force) + Vector3.up * offset.y;  
			                                                              //Move the ball towards target position with y offset--//
		
			Vector3 ballDir = ball.position - transform.position;        // ---Distance between ball and player--//
			
			if(ballDir.z >= 0 )
			{
				animator.Play("RightSwing");                             // --If ball distance is +ive play RightSwing animation--//
			}
			else
			    animator.Play("LeftSwing");                             // --If ball distance is +ive play LeftSwing animation--//
			
			aimTarget.position = aimTargetInitialPos;                  //--Set the aim target to initial position--//
			ball.GetComponent<BallScript>().hitter = "Player";
			ball.GetComponent<BallScript>().isPlaying = true;
			
		}
	}
}
