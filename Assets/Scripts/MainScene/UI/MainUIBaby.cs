using UnityEngine;
using System.Collections;

public class MainUIBaby : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnThirstLowerStandard()
    {
        GetComponent<Animator>().SetBool("isThirsty", true);
        GetComponent<RandomAction>().isIdle = 0;
    }

    public void OnHungerLowerStandard()
    {
        GetComponent<Animator>().SetBool("isHungry", true);
        GetComponent<RandomAction>().isIdle = 0;
    }

    public void OnEnergyLowerStandard()
    {
        GetComponent<Animator>().SetBool("isLowEnergy", true);
        GetComponent<RandomAction>().isIdle = 0;
    }

    public void ExitException()
    {
    }

}
