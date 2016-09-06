using UnityEngine;
using System.Collections;

public class BombTrytoCatch : MonoBehaviour
{
    //After this time, a game object will be destroyed.
    public float destroyTime;
    //cannonpower for the four cannons. lefttop, righttop, leftbottom and rightbottom.
    private int[] cannonPower = new int[4];
    private Vector2 velocityTemp;
    private Rigidbody2D rb2d;
    private Animator bombAnimator;
    private int bounceLifeTime;

    //void DestroyByTime(float waitTimeSec)
    //{
    //    Destroy(gameObject, waitTimeSec);
    //}

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bombAnimator = GetComponent<Animator>();
        bounceLifeTime = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fish")
        {
            bombAnimator.SetTrigger("isCollided");
            rb2d.velocity = new Vector2(0f, 0f);
            Destroy(gameObject, 1f);
            //Todo: Send fish to be caught message to server. include bomb power, fish type and etc.
            //currently it's single player version.
            if(Random.Range(0, 100) >= 90)
            {
                other.GetComponent<Animator>().SetTrigger("isCaught");
                Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                Destroy(other.gameObject.GetComponent<Collider2D>());
                Destroy(other.gameObject, 1f);
            }
        }
        //bounce boombs if they hit the boundary.
        //Todo: rotation.
        else if (other.tag == "BoundaryVertical")
        {
            judgeBounceLifeTime();
            velocityTemp = rb2d.velocity;
            velocityTemp.x = -velocityTemp.x;
            rb2d.velocity = velocityTemp;
        }
        else if (other.tag == "BoundaryHorizontal")
        {
            judgeBounceLifeTime();
            velocityTemp = rb2d.velocity;
            velocityTemp.y = -velocityTemp.y;
            rb2d.velocity = velocityTemp;
        }
        else
            return;
    }

    void judgeBounceLifeTime()
    {
        if (bounceLifeTime <= 10)
            bounceLifeTime++;
        else
        {
            bombAnimator.SetTrigger("isCollided");
            rb2d.velocity = new Vector2(0f, 0f);
            Destroy(gameObject, 1f);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement);
        rb2d.MoveRotation(Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) / Mathf.Deg2Rad - 90);
    }
}
