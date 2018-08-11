using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {
    ParticleSystem ps;
	// Use this for initialization
	void Start () {

        ps = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, ps.duration);
	}

}
