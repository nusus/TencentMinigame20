using UnityEngine;
using System.Collections;

public class FeedFoodGD : MonoBehaviour {

    public BallController2D m_BallController;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReShootFood()
    {
        m_BallController.ReShootBall();
    }
}
