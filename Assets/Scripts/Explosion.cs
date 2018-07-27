using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(KillSwitch());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator KillSwitch()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
