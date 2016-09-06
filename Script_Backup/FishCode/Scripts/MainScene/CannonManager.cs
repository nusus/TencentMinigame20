using UnityEngine;
using System.Collections;

//Attached to the gameobject of cannon father.(I'm not sure, well let's try)
public class CannonManager : MonoBehaviour
{
    //Todo: manage 4 cannon rotates. Currently remains here.
    public Vector2 touchPosition;
    private Vector3 targetPosition, relativePos, defaultRotationEuler;
    public float fireRate;//time between two shots.
    public float bombSpeed;

    //private variables, needless to know.
    private float nextFire;//time to count next file
    private GameObject bulletToInstantiate;//bullet to shoot
    private Quaternion tempCannonRotation;//cannon head to
    // Use this for initialization
    void Start()
    {
        //Todo: instantiate cannons.
        defaultRotationEuler = GetComponent<Transform>().rotation.eulerAngles;
        bulletToInstantiate = Resources.Load("Prefabs/MainScene/Bullets/bullet2_ion", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rotateCannons();
    }

    public void rotateCannons()
    {
        GetComponentInChildren<CannonPlayAnimation>().cannonPlay();

        if (Input.touchCount > 0)
        {
            touchPosition = GetComponent<TouchManager>().getTouchPositionGameAxis();
        }

        targetPosition.x = touchPosition.x;
        targetPosition.y = touchPosition.y;
    
        targetPosition.z = 0f;
        relativePos = targetPosition - GetComponent<Transform>().position;

        //cannon's heading to
        if (relativePos.y > 0)
            tempCannonRotation = Quaternion.Euler(new Vector3(0f, 0f, defaultRotationEuler.z - Mathf.Atan(relativePos.x / relativePos.y) / Mathf.Deg2Rad));
        else if (relativePos.x <= 0)
            tempCannonRotation = Quaternion.Euler(new Vector3(0f, 0f, defaultRotationEuler.z + 90f));
        else
            tempCannonRotation = Quaternion.Euler(new Vector3(0f, 0f, defaultRotationEuler.z - 90f));
        GetComponent<Transform>().rotation = tempCannonRotation;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject obj = (GameObject)Instantiate(bulletToInstantiate, GetComponent<Transform>().position, tempCannonRotation);
            obj.GetComponent<Rigidbody2D>().velocity = bombSpeed * relativePos / (Mathf.Sqrt(Mathf.Pow(relativePos.x, 2) + Mathf.Pow(relativePos.y, 2)));
        }
    }
}
