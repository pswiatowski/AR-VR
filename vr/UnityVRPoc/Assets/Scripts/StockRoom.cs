using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockRoom : MonoBehaviour {

    public GameObject prefab;

    public int arrayResolution = 10;

    GameObject[] array;

	// Use this for initialization
	void Start () {
        array = new GameObject[arrayResolution];
        for (int i = 0; i < arrayResolution; i++) {
            array[i] = CreatePoint(i, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private GameObject CreatePoint(int x, int y, int z) {
        GameObject point = Instantiate(prefab);
        point.transform.localPosition = GetCoordinates(x);
        /*point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / arrayResolution,
            (float)y / arrayResolution,
            (float)z / arrayResolution
        );*/
        return point;
    }

    private Vector3 GetCoordinates(int x) {
        return new Vector3(x - (arrayResolution - 1) * 0.5f, 0, 0);
    }
}
