using UnityEngine;
using System.Collections;

public class ChangeNumber : MonoBehaviour {
	public UnityEngine.UI.Image m_HealthTenNumberImage;
	public UnityEngine.UI.Image m_HealthUnitNumberImage;
	private int m_HealthNumber;

	public UnityEngine.UI.Image m_RMBhundNumImage;
	public UnityEngine.UI.Image m_RMBTenNumImage;
	public UnityEngine.UI.Image m_RMBUnitNumImage;
	private int m_RMBNum;

	public Sprite[] m_Images;
	//private Sprite m_GoldNumber
	// Use this for initialization

	void Start () {
		m_HealthNumber = 1;
		m_RMBNum = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			m_HealthNumber += 1;
			m_RMBNum += 10;
			ChangehealNumberSprite (m_HealthNumber,m_HealthTenNumberImage,m_HealthUnitNumberImage);
			ChangeRMBNumberSprite (m_RMBNum,m_RMBhundNumImage,m_RMBTenNumImage,m_RMBUnitNumImage);
		}
	
	}

	public void ChangehealNumberSprite(int number,UnityEngine.UI.Image b,UnityEngine.UI.Image c){
		int ten = number / 10;
		int unit = number - ten * 10;
		b.sprite = m_Images [ten];
		c.sprite = m_Images [unit];
	}

	public void ChangeRMBNumberSprite(int number,UnityEngine.UI.Image a,UnityEngine.UI.Image b,UnityEngine.UI.Image c){
		int hund = number / 100;
		int ten = (number - hund*100 )/ 10;
		int unit = number - hund*100 - ten * 10;
		a.sprite = m_Images [hund];
		b.sprite = m_Images [ten];
		c.sprite = m_Images [unit];
	}
}
