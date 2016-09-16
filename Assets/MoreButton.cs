using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoreButton : MonoBehaviour {

	public Button shezhiButton;
	public Button juqingButton;
	public Button moreButton;

	// Use this for initialization
	void Start () {
		shezhiButton.gameObject.SetActive (false);
		juqingButton.gameObject.SetActive (false);

		EventTriggerListener.Get (moreButton.gameObject).onClick = OnClick;
	}

	private void OnClick (GameObject go) {
		if (shezhiButton.gameObject.activeSelf == true && juqingButton.gameObject.activeSelf == true) {
			shezhiButton.gameObject.SetActive (false);
			juqingButton.gameObject.SetActive (false);
		} else {
			shezhiButton.gameObject.SetActive (true);
			juqingButton.gameObject.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update (){
	}
}
