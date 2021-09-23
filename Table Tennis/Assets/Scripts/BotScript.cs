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
	
	void Start()
	{
		targetPosition = transform.position;
		animator = GetComponent<Animator>();
		animator.Play("idle");
	}
	
	void Update()
	{
		Move();
	}
	
	void Move()
	{
		targetPosition = new Vector3(transform.position.x, 0.2f , ball.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
		
	}
	
	private Vector3 PickTargets()
	{
		int randomValue = Random.Range(0, targets.Length);
		return targets[randomValue].position;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ball"))
		{
			Vector3 dir = PickTargets() - transform.position;
			Vector3 offset = new Vector3(0, 1f, 0);
			
			other.transform.position = Vector3.SmoothDamp(other.transform.position, PickTargets(), ref dir,force * Time.deltaTime);
			other.GetComponent<Rigidbody>().velocity = dir.normalized * force;
			
			Vector3 ballDir = ball.position - transform.position;
			
			if(ballDir.z >= 0 )
			{
				animator.Play("RightSwing");
			}
			else
			    animator.Play("LeftSwing");
		}
	}
}
