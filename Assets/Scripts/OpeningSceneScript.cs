using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningSceneScript : MonoBehaviour {
  //  GameObject playNowBtn;
    // GameObject optionBtn;
	// Use this for initialization
	void Start () {
        //playNowBtn = GameObject.Find("PlayNow");
        //optionBtn = GameObject.Find("Options");
      

    }
	
	// Update is called once per frame
	void Update () {
        


    }

    public void startGame() {
       // SceneManager.MergeScenes(SceneManager.GetSceneAt(0), SceneManager.GetSceneAt(1));
        SceneManager.LoadScene(1);
    }

    public void openOptions() {
        SceneManager.LoadScene("OptionsScene");
    }
}
