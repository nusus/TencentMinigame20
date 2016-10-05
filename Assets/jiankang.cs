using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class jiankang : MonoBehaviour {

	public GameObject shuxingPanel = null;
	public Button jiankangB = null;

	// Use this for initialization
	void Start () {
		EventTriggerListener.Get(jiankangB.gameObject).onClick = OnClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnClick (GameObject go) {
		if (shuxingPanel.gameObject.activeSelf == true ) {
			shuxingPanel.gameObject.SetActive (false);
		} else {
			shuxingPanel.gameObject.SetActive (true);
		}
	}
}
