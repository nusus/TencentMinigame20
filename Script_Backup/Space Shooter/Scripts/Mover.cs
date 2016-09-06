using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;
    private Vector3 trans = new Vector3(0f, 0f, 1.0f);
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().velocity = trans * speed;
    }
}
