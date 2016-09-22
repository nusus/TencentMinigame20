using UnityEngine;
using System.Collections;

public class FeedFoodGD : MonoBehaviour {

    private BallController m_BallController;
	// Use this for initialization
	void Start () {
        m_BallController = GameObject.Find("slingshot").transform.FindChild("food").
            transform.GetComponent<BallController>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReShootFood()
    {
        m_BallController.ReShootBall();
    }
}
