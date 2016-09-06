using UnityEngine;
using System.Collections;

public class FishSpawnner : MonoBehaviour
{
    //Fish swim speed.
    public float swimSpeed;
    //time between two fish spawn.
    public float fishSpawnRate;

    private float nextSpawn;
    private GameObject[] fishRes;
    // Use this for initialization
    void Start()
    {
        fishRes = new GameObject[24];
        string fishPath = "Prefabs/MainScene/Fish/fish";
        for (int i=0; i<24; i++)
        {
            string b = fishPath + (i + 1).ToString();
            fishRes[i] = Resources.Load(b, typeof(GameObject)) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + fishSpawnRate;
            Vector3 position = new Vector3(-10f, Random.Range(-4.5f, 4.5f), 0f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject obj = Instantiate(fishRes[Random.Range(0, 23)], position, rotation) as GameObject;
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(swimSpeed, 0f);
        }
    }
}
