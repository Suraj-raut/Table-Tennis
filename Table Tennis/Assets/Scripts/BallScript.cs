using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
   private Vector3 initailPos;
   public AnimationCurve yCurve;
   private float timeLapsed = 0;
   public string hitter;
	
   public static int playerScore = 0;
   public static int BotScore = 0;
	
   public bool isPlaying = true;
  
	public AudioSource RandomHit;

	
    void Start()
    {
        initailPos = transform.position;                                //---Ball intial position--//
		
    }
	
	
	void FixedUpdate()
	{
		timeLapsed += Time.deltaTime;                                    //---Time for y curve--//
	}
	
	private void OnCollisionEnter(Collision collision)     
	{
		if(collision.transform.CompareTag("Walls"))                  
		{
			RandomHit.Play();
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = initailPos;
		}
		
		if(collision.transform.CompareTag("Net") && isPlaying)
		{
			RandomHit.Play();
			if(hitter == "Player")
			{
				BotScore += 10;
				hitter = "null";
			}
			else if(hitter == "Bot")
			{
				playerScore += 10;
				hitter = "null";
			}
			StartCoroutine(HitToNet());
		}
		
		if(collision.transform.CompareTag("Player") ||  collision.transform.CompareTag("Bot"))
		{
			GetComponent<Rigidbody>().MovePosition(new Vector3 (0, transform.position.y + yCurve.Evaluate(timeLapsed), 0));
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		RandomHit.Play();

		if(other.CompareTag("Out")  && isPlaying)
		{
			if(hitter == "Player")
			{
				playerScore += 10;
				hitter = "null";
			}
			else if(hitter == "Bot")
			{
				BotScore += 10;
				hitter = "null";
			}
			
			isPlaying = false;
		}
		
		
	}
	
	IEnumerator HitToNet()
	{
		yield return new WaitForSeconds(3);
		transform.position = initailPos;
		isPlaying = false;
	}

    
}
