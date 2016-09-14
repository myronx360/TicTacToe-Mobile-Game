using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardBehaviourScript : MonoBehaviour {

    private ArrayList player1Tiles = new ArrayList();
    private ArrayList player2Tiles = new ArrayList();
    private ArrayList checkedTiles = new ArrayList();
    public GameObject[] tiles = new GameObject[9];
    public Animator[] anim = new Animator[9];

    RaycastHit2D box;
    GameObject playBtn;
    GameObject winLabel;
    GameObject xLabel;
    GameObject oLabel;
    GameObject drawLabel;

    bool winnerFound = false;
    bool winner1Found = false;
    bool winner2Found = false;
    bool noWinnerFound = false;
    bool aiTurn;
    bool aiOn = true;
    bool hardMode = true;
    bool changePlayer = false;
    bool scoreUpdated;
    int currentPlayer;
    int firstPlayer;
    int gameMode;// 0-pvp, 1-pve
    public static int xScore = 0;
    public static int oScore = 0;
    public static int drawScore = 0;
    public Animator animatorX;
    public Animator animatorO;
    public Animator animatorDraw;

    void Awake() {
        DontDestroyOnLoad(GameObject.Find("xScoreLabel"));
        DontDestroyOnLoad(GameObject.Find("oScoreLabel"));
        DontDestroyOnLoad(GameObject.Find("drawScoreLabel"));
    }

    // Use this for initialization
    void Start() {
        setDifficulty();
        setGameMode();

        playBtn = GameObject.Find("PlayAgainButton");
        winLabel = GameObject.Find("YouWinLabel");
        playBtn.SetActive(false);
        winLabel.SetActive(false);


        xLabel = GameObject.Find("XScoreLabel");
        oLabel = GameObject.Find("OScoreLabel");
        drawLabel = GameObject.Find("DrawScoreLabel");

        xLabel.GetComponent<Text>().text = xScore.ToString();
        oLabel.GetComponent<Text>().text = oScore.ToString();
        drawLabel.GetComponent<Text>().text = drawScore.ToString();
           
        xScore = int.Parse(xLabel.GetComponent<Text>().text);
        oScore = int.Parse(oLabel.GetComponent<Text>().text);
        drawScore = int.Parse(drawLabel.GetComponent<Text>().text);
 
    
        // set the player that goes first
        firstPlayer = Random.Range(1, 3);
        scoreUpdated = false;

        // if gameMode P Vs AI
        if (aiOn) {
            // if the random firstPlayer number is 1 then AI goes first else AI goes second
            if (firstPlayer == 1) {
                aiTurn = true;
                currentPlayer = 1;
            } else {
                aiTurn = false;
                currentPlayer = 2;
            }
        } else {
            currentPlayer = Random.Range(1, 3);
            aiTurn = false;
        }


    }

    // Update is called once per frame
    void Update() {
        if (!winnerFound) {
            //if a winner hasn't been found check for a click or if it is the AI's turn
            // AND if a tile is clicked
            if ((Input.GetMouseButtonDown(0) && TileBehaviorScript.isSelected) || aiTurn) {
                gamePlay(aiTurn);
                TileBehaviorScript.isSelected = false;
                checkWin();
                if (changePlayer)
                    changesPlayer();
            }
        } else {

            playBtn.SetActive(true);
            winLabel.SetActive(true);
            winLabel.GetComponent<Text>().text = getWinnerText();

        }
        xLabel.GetComponent<Text>().text = xScore.ToString();
        oLabel.GetComponent<Text>().text = oScore.ToString();
        drawLabel.GetComponent<Text>().text = drawScore.ToString();
        winnerFoundCheck();

    }

    //  you will get more accurate results from physics code if you place it in the FixedUpdate function rather than Update
    void FixedUpdate() {
        //  Vector3 force = transform.forward * driveForce * Input.GetAxis("Vertical");
        // rigidbody.AddForce(force);
        for (int i = 0; i < 9; i++) {
            //if (tiles[i].transform.position.x < Camera.main.transform.position.x) {
            //}

        }
    }

    /**It is also useful sometimes to be able to make additional changes at a point after the Update and FixedUpdate functions have been called 
     * for all objects in the scene and after all animations have been calculated. An example is where a camera should remain trained on a target
     * object; the adjustment to the camera’s orientation must be made after the target object has moved. Another example is where the script code 
     * should override the effect of an animation (say, to make the character’s head look towards a target object in the scene).
     **/
    void LateUpdate() {
        // Camera.main.transform.LookAt(target.transform);
    }

    /**
     * Unity has a system for rendering GUI controls over the main action in the scene and responding to clicks on these controls. This code is handled 
     * somewhat differently from the normal frame update and so it should be placed in the OnGUI function, which will be called periodically.
     **/
    void OnGUI() {
        //GUI.
    }

    //void OnCollisionEnter(otherObj: Collision) {
    //    if (otherObj.tag == "Arrow") {
    //        ApplyDamage(10);
    //    }
    //}

    /* void OnMouseDown() {
         Debug.Log("ONMOUSEDOWN");
         if(box.transform.gameObject.name == "Pos0") {
             Debug.Log("Name");
         }
         box = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

         if (box.collider != null && box.transform.gameObject.tag == "PlayTile") {

             if (Input.GetMouseButtonDown(0)) {
                 Debug.Log("Print Box name: " + box.transform.gameObject.name);
                 Debug.Log("Print Box name: " + box.transform.gameObject.ToString());
             }
         }
     }*/

    // void OnDestroy() {
    //PlayerPrefs.SetInt("xScore", xScore);
    //PlayerPrefs.SetInt("oScore", oScore);
    //PlayerPrefs.SetInt("drawScore", drawScore);
    //}

    private void checkWin() {
        // winning player 1 positions
        if (player1Tiles.Contains(0) && player1Tiles.Contains(1) && player1Tiles.Contains(2)) {
            anim[0].SetTrigger("XWinnerFound"); anim[1].SetTrigger("XWinnerFound"); anim[2].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(3) && player1Tiles.Contains(4) && player1Tiles.Contains(5)) {
            anim[3].SetTrigger("XWinnerFound"); anim[4].SetTrigger("XWinnerFound"); anim[5].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(6) && player1Tiles.Contains(7) && player1Tiles.Contains(8)) {
            anim[6].SetTrigger("XWinnerFound"); anim[7].SetTrigger("XWinnerFound"); anim[8].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(0) && player1Tiles.Contains(3) && player1Tiles.Contains(6)) {
            anim[0].SetTrigger("XWinnerFound"); anim[3].SetTrigger("XWinnerFound"); anim[6].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(1) && player1Tiles.Contains(4) && player1Tiles.Contains(7)) {
            anim[1].SetTrigger("XWinnerFound"); anim[4].SetTrigger("XWinnerFound"); anim[7].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(2) && player1Tiles.Contains(5) && player1Tiles.Contains(8)) {
            anim[2].SetTrigger("XWinnerFound"); anim[5].SetTrigger("XWinnerFound"); anim[8].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(0) && player1Tiles.Contains(4) && player1Tiles.Contains(8)) {
            anim[0].SetTrigger("XWinnerFound"); anim[4].SetTrigger("XWinnerFound"); anim[8].SetTrigger("XWinnerFound");
            winner1Found = true;
        } else if (player1Tiles.Contains(2) && player1Tiles.Contains(4) && player1Tiles.Contains(6)) {
            anim[2].SetTrigger("XWinnerFound"); anim[4].SetTrigger("XWinnerFound"); anim[6].SetTrigger("XWinnerFound");
            winner1Found = true;


            // winning player 2 positions
        } else if (player2Tiles.Contains(0) && player2Tiles.Contains(1) && player2Tiles.Contains(2)) {
            anim[0].SetTrigger("OWinnerFound"); anim[1].SetTrigger("OWinnerFound"); anim[2].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(3) && player2Tiles.Contains(4) && player2Tiles.Contains(5)) {
            anim[3].SetTrigger("OWinnerFound"); anim[4].SetTrigger("OWinnerFound"); anim[5].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(6) && player2Tiles.Contains(7) && player2Tiles.Contains(8)) {
            anim[6].SetTrigger("OWinnerFound"); anim[7].SetTrigger("OWinnerFound"); anim[8].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(0) && player2Tiles.Contains(3) && player2Tiles.Contains(6)) {
            anim[0].SetTrigger("OWinnerFound"); anim[3].SetTrigger("OWinnerFound"); anim[6].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(1) && player2Tiles.Contains(4) && player2Tiles.Contains(7)) {
            anim[1].SetTrigger("OWinnerFound"); anim[4].SetTrigger("OWinnerFound"); anim[7].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(2) && player2Tiles.Contains(5) && player2Tiles.Contains(8)) {
            anim[2].SetTrigger("OWinnerFound"); anim[5].SetTrigger("OWinnerFound"); anim[8].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(0) && player2Tiles.Contains(4) && player2Tiles.Contains(8)) {
            anim[0].SetTrigger("OWinnerFound"); anim[4].SetTrigger("OWinnerFound"); anim[8].SetTrigger("OWinnerFound");
            winner2Found = true;
        } else if (player2Tiles.Contains(2) && player2Tiles.Contains(4) && player2Tiles.Contains(6)) {
            anim[2].SetTrigger("OWinnerFound"); anim[4].SetTrigger("OWinnerFound"); anim[6].SetTrigger("OWinnerFound");
            winner2Found = true;



            // winning player 2 positions
            /*} else if ((player2Tiles.Contains(0) && player2Tiles.Contains(1) && player2Tiles.Contains(2)) ||
                (player2Tiles.Contains(3) && player2Tiles.Contains(4) && player2Tiles.Contains(5)) ||
                (player2Tiles.Contains(6) && player2Tiles.Contains(7) && player2Tiles.Contains(8)) ||
                (player2Tiles.Contains(0) && player2Tiles.Contains(3) && player2Tiles.Contains(6)) ||
                (player2Tiles.Contains(1) && player2Tiles.Contains(4) && player2Tiles.Contains(7)) ||
                (player2Tiles.Contains(2) && player2Tiles.Contains(5) && player2Tiles.Contains(8)) ||
                (player2Tiles.Contains(0) && player2Tiles.Contains(4) && player2Tiles.Contains(8)) ||
                (player2Tiles.Contains(2) && player2Tiles.Contains(4) && player2Tiles.Contains(6))) {

                Debug.Log("Player2 Wins");
                winnerFound = true;
                //animatorO.SetBool("OWinnerFound", true);
                for (int i = 0; i < 9; i++) {
                    anim[i].SetTrigger("OWinnerFound");
                }*/

        } else {// A draw
            if (!winnerFound && checkedTiles.Count == 9) {
                noWinnerFound = true;
                for (int i = 0; i < 9; i++) {
                    anim[i].SetTrigger("DrawTrigger");
                }
            }
        }
    }

    private void changesPlayer() {
        if (currentPlayer == 1) {
            currentPlayer = 2;
            if (aiOn)
                aiTurn = false;
        } else {
            currentPlayer = 1;
            if (aiOn)
                aiTurn = true;
        }
    }

    private void gamePlay(bool aiTurn) {

        string selectedName = "";
        // if it the AI's turn changed the selected name to a random unselected tile
        // else if it the AI's turn changed the selected name to a smarter unselected tile
        // else 
        // gets the name of the tile that was clicked on
        if (aiTurn && !hardMode) {
            selectedName = getAISelectedTileName();
        } else if ((aiTurn && hardMode)) {
            selectedName = betterAI();
        } else {
            selectedName = TileBehaviorScript.selectedName;
        }

        for (int i = 0; i < 9; i++) {
            // if the current tile name is equal to the selected name
            if (tiles[i].name == selectedName) {
                // if the tile hasn't been selected then
                // add the tile number to the current player's tile list and the already checkedTile list 
                if (!checkedTiles.Contains(i)) {
                    changePlayer = true;
                    if (currentPlayer == 1) {
                        player1Tiles.Add(i);
                        checkedTiles.Add(i);
                        // anim[i].speed = 2;
                        anim[i].SetTrigger("XStart");
                        //  anim[i].GetComponent<SpriteRenderer>().color = Color.white;
                        // display player 1 choice
                        //tiles[i].transform.Rotate(0, 172 * Time.deltaTime, 0);
                        // tiles[i].GetComponent<SpriteRenderer>().color = Color.blue;

                        //tiles[i].GetComponent<Transform>().transform.position.Set(tiles[i].GetComponent<Transform>().transform.position.x, tiles[i].GetComponent<Transform>().transform.position.y, 5);


                        // anim[i].gameObject.SetActive(true);



                        //tiles[i].GetComponent<Transform>().transform.position = new Vector3(0, 0, 2);

                    } else {
                        player2Tiles.Add(i);
                        checkedTiles.Add(i);
                        //anim[i].speed = 2;
                        anim[i].SetTrigger("OStart");


                        //diplay player 2 choice
                        //tiles[i].transform.Rotate(172 * Time.deltaTime, 0, 0);
                        //tiles[i].GetComponent<SpriteRenderer>().color = Color.red;

                        // tiles[i].GetComponent<Transform>().transform.position.Set(tiles[i].GetComponent<Transform>().transform.position.x, tiles[i].GetComponent<Transform>().transform.position.y,5);
                        //   tiles[i].GetComponent<Transform>().transform.position.Set
                        //anim[i].gameObject.SetActive(true);

                        //tiles[i].GetComponent<Transform>().transform.position = new Vector3(0, 0, 2);

                    }
                    anim[i].GetComponent<SpriteRenderer>().color = Color.white;
                } else {
                    changePlayer = false; // doesn't change the player if the user selects an already selected tile
                }
            }
        }
    }

    private string getAISelectedTileName() {
        // get a random number to select a tile space 
        //  if that number isn't in the checkedTiles list (which contains all selected tiles)
        //  then choose that space
        bool foundSpace = false;
        int randNum = 0;

        while (!foundSpace) {
            randNum = Random.Range(0, 9);
            if (!checkedTiles.Contains(randNum)) {
                foundSpace = true;
            }
        }
        return tiles[randNum].name;
    }

    public void restart() {
        SceneManager.LoadScene(1);
    }

    public void startGame() {
        // SceneManager.MergeScenes(SceneManager.GetSceneAt(1), SceneManager.GetSceneAt(0));
        SceneManager.LoadScene(1);
    }

    private bool winnerFoundCheck() {
        if (winner1Found || winner2Found || noWinnerFound) {
            winnerFound = true;
            if (!scoreUpdated) {
                increamentScore();
                scoreUpdated = true;
            }
            return true;
        } else {
            return false;
        }
    }

    private string getWinnerText() {
        if (aiOn) {
            if (winner1Found) {
                return "You Lose";
            } else if (winner2Found) {
                return "You Win";
            } else {
                return "Draw";
            }
        }else {
            if (winner1Found) {
                return "X Wins";
            } else if (winner2Found) {
                return "O Wins";
            } else {
                return "Draw";
            }
        }
    }

    // makes the AI have better coices than being just random
    private string betterAI() {
        // get a random number to select a tile space 
        //  if that number isn't in the checkedTiles list (which contains all selected tiles)
        // AND is a corner
        //  then choose that space
        bool foundSpace = false;
        int randNum = 0;

        while (!foundSpace) {
            randNum = Random.Range(0, 9);

            // fill corners first
            if ((!checkedTiles.Contains(randNum)) &&
                (randNum == 0 || randNum == 2 || randNum == 6 || randNum == 8)) {
                foundSpace = true;
            }

            // when corners are filled fill in sides and center
            if ((checkedTiles.Contains(0) && checkedTiles.Contains(2) && checkedTiles.Contains(6) && checkedTiles.Contains(8))
                && (randNum == 1 || randNum == 3 || randNum == 5 || randNum == 7 || randNum == 4)) {
                foundSpace = true;
            }



            // Defensive Moves
            if (player2Tiles.Contains(0) && player2Tiles.Contains(2) && !checkedTiles.Contains(1)) {
                randNum = 1;
            } else if (player2Tiles.Contains(3) && player2Tiles.Contains(5) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player2Tiles.Contains(6) && player2Tiles.Contains(8) && !checkedTiles.Contains(7)) {
                randNum = 7;
            } else if (player2Tiles.Contains(0) && player2Tiles.Contains(6) && !checkedTiles.Contains(3)) {
                randNum = 3;
            } else if (player2Tiles.Contains(1) && player2Tiles.Contains(7) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player2Tiles.Contains(2) && player2Tiles.Contains(8) && !checkedTiles.Contains(5)) {
                randNum = 5;
            } else if (player2Tiles.Contains(0) && player2Tiles.Contains(8) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player2Tiles.Contains(2) && player2Tiles.Contains(6) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player2Tiles.Contains(0) && player2Tiles.Contains(1) && !checkedTiles.Contains(2)) {
                randNum = 2;
            } else if (player2Tiles.Contains(3) && player2Tiles.Contains(4) && !checkedTiles.Contains(5)) {
                randNum = 5;
            } else if (player2Tiles.Contains(6) && player2Tiles.Contains(7) && !checkedTiles.Contains(8)) {
                randNum = 8;
            } else if (player2Tiles.Contains(1) && player2Tiles.Contains(2) && !checkedTiles.Contains(0)) {
                randNum = 0;
            } else if (player2Tiles.Contains(4) && player2Tiles.Contains(5) && !checkedTiles.Contains(3)) {
                randNum = 3;
            } else if (player2Tiles.Contains(7) && player2Tiles.Contains(8) && !checkedTiles.Contains(6)) {
                randNum = 6;
            } else if (player2Tiles.Contains(0) && player2Tiles.Contains(3) && !checkedTiles.Contains(6)) {
                randNum = 6;
            } else if (player2Tiles.Contains(1) && player2Tiles.Contains(4) && !checkedTiles.Contains(7)) {
                randNum = 7;
            } else if (player2Tiles.Contains(2) && player2Tiles.Contains(5) && !checkedTiles.Contains(8)) {
                randNum = 8;
            } else if (player2Tiles.Contains(3) && player2Tiles.Contains(6) && !checkedTiles.Contains(0)) {
                randNum = 0;
            } else if (player2Tiles.Contains(4) && player2Tiles.Contains(7) && !checkedTiles.Contains(1)) {
                randNum = 1;
            } else if (player2Tiles.Contains(5) && player2Tiles.Contains(8) && !checkedTiles.Contains(2)) {
                randNum = 2;
            } else if (player2Tiles.Contains(0) && player2Tiles.Contains(4) && !checkedTiles.Contains(8)) {
                randNum = 8;
            } else if (player2Tiles.Contains(4) && player2Tiles.Contains(8) && !checkedTiles.Contains(0)) {
                randNum = 0;
            } else if (player2Tiles.Contains(2) && player2Tiles.Contains(4) && !checkedTiles.Contains(6)) {
                randNum = 6;
            } else if (player2Tiles.Contains(4) && player2Tiles.Contains(6) && !checkedTiles.Contains(2)) {
                randNum = 2;
            }

            // check when two AI tiles are close to a win
            // offensive moves (placed after defensive and initial moves to overwrite them)
            if (player1Tiles.Contains(0) && player1Tiles.Contains(2) && !checkedTiles.Contains(1)) {
                randNum = 1;
            } else if (player1Tiles.Contains(3) && player1Tiles.Contains(5) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player1Tiles.Contains(6) && player1Tiles.Contains(8) && !checkedTiles.Contains(7)) {
                randNum = 7;
            } else if (player1Tiles.Contains(0) && player1Tiles.Contains(6) && !checkedTiles.Contains(3)) {
                randNum = 3;
            } else if (player1Tiles.Contains(1) && player1Tiles.Contains(7) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player1Tiles.Contains(2) && player1Tiles.Contains(8) && !checkedTiles.Contains(5)) {
                randNum = 5;
            } else if (player1Tiles.Contains(2) && player1Tiles.Contains(6) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player1Tiles.Contains(0) && player1Tiles.Contains(8) && !checkedTiles.Contains(4)) {
                randNum = 4;
            } else if (player1Tiles.Contains(0) && player1Tiles.Contains(2) && !checkedTiles.Contains(1)) {
                randNum = 1;
            } else if (player1Tiles.Contains(0) && player1Tiles.Contains(3) && !checkedTiles.Contains(6)) {
                randNum = 6;
            } else if (player1Tiles.Contains(1) && player1Tiles.Contains(4) && !checkedTiles.Contains(7)) {
                randNum = 7;
            } else if (player1Tiles.Contains(2) && player1Tiles.Contains(5) && !checkedTiles.Contains(8)) {
                randNum = 8;
            } else if (player1Tiles.Contains(3) && player1Tiles.Contains(6) && !checkedTiles.Contains(0)) {
                randNum = 0;
            } else if (player1Tiles.Contains(4) && player1Tiles.Contains(7) && !checkedTiles.Contains(1)) {
                randNum = 1;
            } else if (player1Tiles.Contains(5) && player1Tiles.Contains(8) && !checkedTiles.Contains(2)) {
                randNum = 2;
            }

        }
        return tiles[randNum].name;
    }

    private void increamentScore() {
        if (winner1Found)
            xScore++;
        else if (winner2Found)
            oScore++;
        else
            drawScore++;
    }

    private void setDifficulty() {
        if (PlayerPrefs.GetString("difficulty", "hard") == "easy") {
            hardMode = false;
        } else {
            hardMode = true;
        }
    }

    private void setGameMode() {
        if (PlayerPrefs.GetString("gameMode", "one") == "one") {
            aiOn = true;
        } else {
            aiOn = false;
        }
    }

}