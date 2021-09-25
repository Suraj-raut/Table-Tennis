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
		if(collision.transform.CompareTag("Walls"))                   //---If the ball hit Wall---//                 
		{
			RandomHit.Play();
			GetComponent<Rigidbody>().velocity = Vector3.zero;      //---Set the velocity of ball to zero--//
			transform.position = initailPos;                        //--Start again by setting the ball to intial position--//
		}
		
		if(collision.transform.CompareTag("Net") && isPlaying)     //--If the ball hit net--//
		{
			RandomHit.Play();
			if(hitter == "Player")
			{
				BotScore += 10;                                    //---If ball last hit by player give 10 point to bot--// 
				hitter = "null";
			}
			else if(hitter == "Bot")
			{
				playerScore += 10;                                  //---If ball last hit by bot give 10 point to player--// 
				hitter = "null";
			}
			StartCoroutine(HitToNet());
		}
		
		if(collision.transform.CompareTag("Player") ||  collision.transform.CompareTag("Bot")) 
		{
			GetComponent<Rigidbody>().MovePosition(new Vector3 (0, transform.position.y + yCurve.Evaluate(timeLapsed), 0));
		}                       //---If the ball is hit by player or bot give the ball a y-curve to pass the net--// 
	}
	
	private void OnTriggerEnter(Collider other)
	{
		RandomHit.Play();

		if(other.CompareTag("Out")  && isPlaying)        //---If ball hits the wall or ground i.e with out tag--//
		{
			if(hitter == "Player")                       //---if ball hit by player and it is out means not hit back by bot--//
			{
				playerScore += 10;                        //--Give 10 point to player--//
				hitter = "null";
			}
			else if(hitter == "Bot")                      //---if ball hit by bot and not hit back by player--//
			{
				BotScore += 10;                           //--Give 10 point to Bot--//
				hitter = "null";
			}
			
			isPlaying = false;
		}
		
		
	}
	
	IEnumerator HitToNet()                                  //---If the ball hit to net wait for 3 sec--//
	{
		yield return new WaitForSeconds(3);
		transform.position = initailPos;
		isPlaying = false;
	}

    
}
