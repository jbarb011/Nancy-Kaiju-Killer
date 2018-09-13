using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {


    public float ZoomOutSpeed = 0.5f;
    public float PanUpSpeed = 0.25f;
    public float GroundY = 0.0f;
    public float GroundSize = 4.0f;

    GameObject Player;
    Vector2 localPosition;
    bool readytostart = false;


    void Awake()
    {
        // GET GAME OBJECT
        Player = GameObject.Find("Player");
        readytostart = true;
    }

    // Update is called once per frame
    void Update () {
        if (readytostart && Player != null)
        {
            // GET POSITION OF NANCY
            localPosition = Player.GetComponent<Transform>().localPosition;

            // SUBTRACT Y COORDINATE BY THE GROUND COORDINATE
            float ZoomOutMult = localPosition.y - GroundY;
            if (ZoomOutMult > 0)
            {
                // IF YOU ARE ABOVE GROUND LEVEL ZOOMOUT CAMERA
                gameObject.GetComponent<Camera>().orthographicSize = GroundSize + (ZoomOutSpeed * ZoomOutMult);
            }
            else
            {
                // ELSE HAVE CAMERA BE DEFAULT SIZE
                gameObject.GetComponent<Camera>().orthographicSize = GroundSize;
            }

            //HAVE CAMERA STAY ON CONSTANT X COORDINATE WITH PLAYER CHARACTER
            gameObject.GetComponent<Transform>().localPosition = new Vector3(localPosition.x, gameObject.GetComponent<Transform>().localPosition.y, gameObject.GetComponent<Transform>().localPosition.z);
        }
    }
}
