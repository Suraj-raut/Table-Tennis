using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	
  public AudioSource Cilck;
	
  public void StartTheGame()
  {
	  Cilck.Play();
	 SceneManager.LoadScene("Game"); 
  }
	
  public void QuitTheGame()
  {
	  Cilck.Play();
	 Application.Quit();
  }
	
}
