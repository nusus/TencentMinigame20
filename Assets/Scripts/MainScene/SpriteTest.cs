using UnityEngine;
using System.Collections;

public class SpriteTest : MonoBehaviour {

    private Sprite m_Sprite;
	// Use this for initialization
	void Start () {
        m_Sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        print(transform.position);
        print(m_Sprite.border);
        print(m_Sprite.bounds);
        print(m_Sprite.rect);
        print(m_Sprite.textureRect);
        print(m_Sprite.pixelsPerUnit);
        print(transform.lossyScale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
