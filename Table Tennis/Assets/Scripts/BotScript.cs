using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    private float speed = 50f;
	private Animator animator;
	
	[SerializeField]
    private Transform ball;
	
	public Transform[] targets;
	
	private Vector3 targetPosition = Vector3.zero;
	
	private float force = 3f;
	public AudioSource BallHit;
	
	
	
	void Start()
	{
		targetPosition = transform.position;       //---Get the position of target--//
		animator = GetComponent<Animator>();
	
	}
	
	void Update()
	{
		Move();
	}
	
	void Move()
	{
		targetPosition = new Vector3(transform.position.x, 0.2f , ball.position.z);   //---new target position according to ball//
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
		                                                      //---Move towards ball position--//
	}
	
	private Vector3 PickTargets()                           
	{
		int randomValue = Random.Range(0, targets.Length);           //--Pick the random targets to hit by bot --//
		return targets[randomValue].position;                       //--return the position of target based on random value--//
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ball"))                                //--Bot hit the ball--//
		{
			BallHit.Play();
			Vector3 dir = PickTargets() - transform.position;        //---distance between random target position and bot--//
		
			
			other.transform.position = Vector3.SmoothDamp(other.transform.position, PickTargets(), ref dir,force * Time.deltaTime);
			other.GetComponent<Rigidbody>().velocity = dir.normalized * force; //--hit the ball with force in target dir --// 
			
			Vector3 ballDir = ball.position - transform.position;   //--distance between ball and bot for left and right animation
			
			if(ballDir.z >= 0 )
			{
				animator.Play("RightSwing");
			}
			else
			    animator.Play("LeftSwing");
			
			ball.GetComponent<BallScript>().hitter = "Bot";       //--Get the last hitter of ball as 'bot'--//
			ball.GetComponent<BallScript>().isPlaying = true;
		}
	}
}
