using UnityEngine;

public class TileBehaviorScript : MonoBehaviour {
    public static GameObject tileSelected;
    public  GameObject[] tiless;
    public static bool isSelected = false;
    public static string selectedName = "";
    bool rotate = false;
    public static RaycastHit2D box;

    // Use this for initialization
    void Start() {
        tiless = GameObject.FindGameObjectsWithTag("PlayTile");
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log("rotate: " + rotate);
        if (rotate) {
            // transform.Rotate(0, 172 * Time.deltaTime, 0);
            //transform.GetComponent("")
        } else {
            // transform.Rotate(0, 0, 0);
        }

    }

    void OnMouseDown() {
     
        box = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (box.collider != null && box.transform.gameObject.tag == "PlayTile") {
            selectedName = box.transform.name;

            if (Input.GetMouseButtonDown(0)) {
                tileSelected = this.gameObject;
                isSelected = true;
            }

            if (Input.GetMouseButtonDown(0) && !rotate) {

                rotate = true;
            } else if (Input.GetMouseButtonDown(0) && rotate) {
                rotate = false;
            }

           

            //transform.Rotate(0, 100 * Time.deltaTime, 0);
        }
        

    }


    public GameObject getTile() {
        return tileSelected;
    }

    public bool getIsSelected() {
        return isSelected;
    }

    public string getSelectedName() {
        return selectedName;
    }
}

