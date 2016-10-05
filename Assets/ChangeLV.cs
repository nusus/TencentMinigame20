using UnityEngine;
using System.Collections;

public class ChangeLV : MonoBehaviour {
	public UnityEngine.UI.Image m_LVTenNumImage;
	public UnityEngine.UI.Image m_LVUnitNumImage;
	private int m_LVNum;

	public Sprite[] m_Images;
	// Use this for initialization
	void Start () {
		m_LVNum = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			m_LVNum += 1;
			ChangeNumberSprite (m_LVNum);
		}
	}

	public void ChangeNumberSprite(int number){
		int ten = number / 10;
		int unit = number - ten * 10;
		m_LVTenNumImage.sprite = m_Images [ten];
		m_LVUnitNumImage.sprite = m_Images [unit];
	}
}
