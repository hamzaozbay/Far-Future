using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public void MenuButton() {
		if (!GameManager.instance.isGamePaused)
			Pause();
	}

	public void Resume() {
		AudioManager.instance.PlaySound("MenuButton");
		this.gameObject.SetActive(false);
		GameManager.instance.UnPauseGame();
	}

	public void ReturnMainMenu() {	
		GameManager.instance.UnPauseGame();
		SceneLoader.Load(Scenes.MainMenu.ToString());
	}

	public void Pause() {	
		AudioManager.instance.PlaySound("MenuButton");
		this.gameObject.SetActive(true);		
		GameManager.instance.PauseGame();			
	}

}
