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
  
    // Update is called once per frame
    void Update()
    {
        PlayerScoreText.GetComponent<Text>().text = "" + BallScript.playerScore;   //---For UI display player score--//
		BotScoreText.GetComponent<Text>().text = "" + BallScript.BotScore;         //---For UI display Bot score--//
		
		if(BallScript.playerScore == 90 || BallScript.BotScore == 90)  //--Winning screen set active if Player/Bot gets 90 points
		{
			WinningPanel.SetActive(true);
			if(BallScript.playerScore == 90)
			{
				WinningText.GetComponent<Text>().text = "Congratulations You Win..!!!";  //---If Player Wins--//
			}
			else if(BallScript.BotScore == 90)
			{
				WinningText.GetComponent<Text>().text = "Sorry You Lose";               //---If Bot Wins--//
			}
			
		}
		
	
    }
	
	public void PlayAgain()                                           //---Play again button of winning screen--//
	{
		WinningPanel.SetActive(false);
		BallScript.playerScore = 0;
		BallScript.BotScore = 0;
	}
	
	public void BackToMenu()                                        //--Back to menu screen--///
	{
		SceneManager.LoadScene("Menu");
		BallScript.playerScore = 0;
		BallScript.BotScore = 0;
	}
}
