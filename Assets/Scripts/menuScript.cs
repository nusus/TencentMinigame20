using UnityEngine;  
using System.Collections;  
using UnityEngine.UI;  
  
  
public class menuScript : MonoBehaviour {  
    public Canvas Canvasiswater;
    public Button Buttonfood;
	public Button Buttonwater;
	public GameObject mainCamera;
	
    // Use this for initialization  
    void Start () {
        Canvasiswater.enabled = false;   
    }  
  
    public void waterClick()  
    {
        Canvasiswater.enabled = true;
    }  
      
    //yes°´Å¥
    public void yeswaterPress()  
    {
        Canvasiswater.enabled = false;
    }

    public void nowaterPress()  
    {
        Canvasiswater.enabled = false;
    }
}  