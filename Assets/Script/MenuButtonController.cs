using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {
	public Button[] buttons;
    Scene currScene;

	void Start() {
        //button: 0=levels; 1=tutorial; 2=learn; 3=MainMenu
        currScene = SceneManager.GetActiveScene();
        buttons [0].onClick.AddListener (goMainMenu);
		buttons [1].onClick.AddListener (goLevel);
		buttons [2].onClick.AddListener (goTutorial);
		buttons [3].onClick.AddListener (goLearn);
        //to deactivate the button that lead to the same scene as current scene
        if (currScene.name == "MainMenu")
        {
            buttons[0].gameObject.SetActive(false);
        }
        if (currScene.name == "Levels")
        {
            buttons[1].gameObject.SetActive(false);
        }
        if (currScene.name == "Tutorial")
        {
            buttons[2].gameObject.SetActive(false);
        }
        if (currScene.name == "Learn")
        {
            buttons[3].gameObject.SetActive(false);
        }
    }

    void goMainMenu() {
            SceneManager.LoadScene("MainMenu");
	}

	void goLevel() {
            SceneManager.LoadScene("Levels");
	}

	void goTutorial() {       
            SceneManager.LoadScene("Tutorial");        
	}

	void goLearn() {
            SceneManager.LoadScene("Learn");        
	}
    
    
}
