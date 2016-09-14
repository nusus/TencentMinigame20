using UnityEngine;
using System.Collections;

public class watertime : MonoBehaviour {

	public Transform weishuiPanel;
	public Transform weishuijieguoPanel;

	// Use this for initialization
	void Start () {
		Invoke ("LaunchweishuijieguoPanel", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LaunchweishuijieguoPanel(){
		weishuijieguoPanel.gameObject.SetActive(true);
	}
}
