using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryTrigger : MonoBehaviour {

    public GameObject explosionParticlesPrefab;

    Collider expectedCollider;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ExpectCollider(Collider collider) {
        expectedCollider = collider;
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (expectedCollider == otherCollider) {
            GameController.Instance.IncrementScore(1);

            // Particles
            if (explosionParticlesPrefab) {
                GameObject explosion = Instantiate(explosionParticlesPrefab, otherCollider.transform.position, explosionParticlesPrefab.transform.rotation);
                Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
            }
        }
    }
}
