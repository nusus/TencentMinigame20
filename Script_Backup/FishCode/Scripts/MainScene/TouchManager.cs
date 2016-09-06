using UnityEngine;
using System.Collections;
//Attached to TouchManager gameobject in the scene. This object is invicible but manage the touch.
public class TouchManager : MonoBehaviour
{
    private Vector2 position = new Vector2(0f, 0f);
    private Vector2 temp = new Vector2(0f, 0f);

    // Use this for initialization
    void Start()
    {
        GetComponent<GUIText>().text = "NULL";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            position = getTouchPositionGameAxis();
            GetComponent<GUIText>().text = position.x.ToString() + "," + position.y.ToString();
        }
    }

    //Make sure you use if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) or other condition which make sure player start to touch before this func.
    //Get touch axis and convert it to game axis which have nothing to do with resolution.
    public Vector2 getTouchPositionGameAxis()
    {
        return Pixel2Game(Input.GetTouch(0).position);
    }

    //Convert screen pixel axis to game axis
    //default same to camera, considering its size is 4.5, that means 14.4 as width and 9 as height.
    //Zero point is at the center of the screen.
    public Vector2 Pixel2Game(Vector2 pixelAxis)
    {
        temp.x = pixelAxis.x * 14.4f / Screen.width - 7.2f;
        temp.y = pixelAxis.y * 9 / Screen.height - 4.5f;
        return temp;
    }
}
