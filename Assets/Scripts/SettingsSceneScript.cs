using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// use player prefs to set diif and mode and color
public class SettingsSceneScript : MonoBehaviour {
    Toggle easyToggle;
    Toggle hardToggle;
    Toggle singleToggle;
    Toggle multiToggle;

    Image easyBG;
    Image hardBG;
    Image singleBG;
    Image multiBG;



    // Use this for initialization
    void Start() {
        if (GameObject.Find("EasyToggle") != null) {
            easyToggle = GameObject.Find("EasyToggle").GetComponent<Toggle>();
            hardToggle = GameObject.Find("HardToggle").GetComponent<Toggle>();
            easyBG = GameObject.Find("EasyBackground").GetComponent<Image>();
            hardBG = GameObject.Find("HardBackground").GetComponent<Image>();

            singleToggle = GameObject.Find("SingleToggle").GetComponent<Toggle>();
            multiToggle = GameObject.Find("2PlayerToggle").GetComponent<Toggle>();
            singleBG = GameObject.Find("SingleBackground").GetComponent<Image>();
            multiBG = GameObject.Find("MultiBackground").GetComponent<Image>();

            setDefaultColor();
            gameDifficulty();

        }

        // bt = btn1.GetComponent<Button>();
        // how to highlight multiply buttons

    }

    // Update is called once per frame
    void Update() {
        // btn1.GetComponent<Ima

    }

    public void openSettings() {
        SceneManager.LoadScene("optionsScene");
    }

    public void startGame() {
        BoardBehaviourScript.xScore = 0;
        BoardBehaviourScript.oScore = 0;
        BoardBehaviourScript.drawScore = 0;
        SceneManager.LoadScene("MainScene");
    }

    public void gameDifficulty() {

        // bt.colors.
        // Debug.Log(btn1.GetComponent<Toggle>().isOn);
        if (easyToggle.isOn) {
            easyBG.color = Color.red;
            PlayerPrefs.SetString("difficulty", "easy");
        } else {
            easyBG.color = Color.black;
        }

        if (hardToggle.isOn) {
            hardBG.color = Color.red;
            PlayerPrefs.SetString("difficulty", "hard");
        } else {
            hardBG.color = Color.black;
        }
        // btn1.GetComponent<Toggle>().onValueChanged.ToString();

    }
    
    public void gameMode() {
        
        if (singleToggle.isOn) {
            singleBG.color = Color.red;
            PlayerPrefs.SetString("gameMode", "one");
        } else {
            singleBG.color = Color.black;
        }

        if (multiToggle.isOn) {
            multiBG.color = Color.red;
            PlayerPrefs.SetString("gameMode", "multi");
        } else {
            multiBG.color = Color.black;
        }
    }

    // overlapping raycasters
    public void chooseXColor() {

    }

    public void chooseOColor() {

    }

    private void setDefaultColor() {
        // difficulty
        if (PlayerPrefs.GetString("difficulty", "hard") == "easy") {
            easyToggle.isOn = true;
        } else {
            hardToggle.isOn = true;

        }

        // game mode
        if (PlayerPrefs.GetString("gameMode", "one") == "one") {
            singleToggle.isOn = true;
        } else {
            multiToggle.isOn = true;

        }
    }
}

