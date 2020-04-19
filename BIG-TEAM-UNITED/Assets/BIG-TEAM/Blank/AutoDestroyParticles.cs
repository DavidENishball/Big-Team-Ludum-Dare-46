using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour {

    public ParticleSystem[] particleSystems;
	
	// Update is called once per frame
	void Update () {
        foreach(ParticleSystem ps in particleSystems)
        {
            if (ps.IsAlive(true))
                return;
        }

        Destroy(this.gameObject);
	}
}
