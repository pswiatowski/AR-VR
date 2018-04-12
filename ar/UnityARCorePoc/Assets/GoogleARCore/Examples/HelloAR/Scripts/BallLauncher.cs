using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour {

    public GameObject ballPrefab;
    public float ballSpeed = 2.0f;
    public float incrementSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (SwipeController.Instance.IsSwiping(SwipeDirection.Up)) {

            GameObject instance = Instantiate(ballPrefab);
            instance.transform.position = GetComponent<Camera>().transform.position + Vector3.forward;
            Rigidbody rb = instance.GetComponent<Rigidbody>();

            float x = (SwipeController.Instance.lastTouch.x - SwipeController.Instance.firstTouch.x) / Screen.height * 34f;
            float y = (SwipeController.Instance.lastTouch.y - SwipeController.Instance.firstTouch.y) / Screen.height * 34f;
            Vector3 force = new Vector3(x, y, 15);

            rb.AddForce(force * 0.3f, ForceMode.Impulse);

            Destroy(instance, 6);
        }
	}
}
