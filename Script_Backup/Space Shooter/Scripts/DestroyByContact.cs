using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    void Start()
    {
        scoreValue = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }
        
        Instantiate(explosion, transform.position, transform.rotation);
        scoreValue += 10;
        GameObject.FindWithTag("GameController").GetComponent<GameController>().AddScore(scoreValue);
        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameObject.FindWithTag("GameController").GetComponent<GameController>().GameOverFunc();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
