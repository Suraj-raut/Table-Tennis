using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Text PlayerScoreText;
    [SerializeField]
    public Text BotScoreText; 
	
	[SerializeField]
	private GameObject WinningPanel;
	[SerializeField]
	private GameObject WinningText;
	
	public AudioSource WinningSound;
	public AudioSource LosingSound;
	
	
  
    // Update is called once per frame
    void Update()
    {
        PlayerScoreText.GetComponent<Text>().text = "" + BallScript.playerScore;
		BotScoreText.GetComponent<Text>().text = "" + BallScript.BotScore;
		
		if(BallScript.playerScore == 90 || BallScript.BotScore == 90)
		{
			WinningPanel.SetActive(true);
			if(BallScript.playerScore == 90)
			{
				WinningSound.Play();
				WinningText.GetComponent<Text>().text = "Congratulations You Win..!!!";
			}
			else if(BallScript.BotScore == 90)
			{
				LosingSound.Play();
				WinningText.GetComponent<Text>().text = "Sorry You Lose";
			}
			
		}
		
    }
	
	public void PlayAgain()
	{
		WinningPanel.SetActive(false);
		BallScript.playerScore = 0;
		BallScript.BotScore = 0;
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu");
		BallScript.playerScore = 0;
		BallScript.BotScore = 0;
	}
}
