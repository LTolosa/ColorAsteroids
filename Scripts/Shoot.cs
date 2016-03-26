using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    // Reference to Volumetric Line PreFab
    public GameObject linePrefab;
    // Position of Laser Origin
    public Transform laserOrigin;

    private const float DELAY_TIME = 0.1f;
    private float shotTime;

	// Use this for initialization
	void Start () {
        shotTime = 0.0f;
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && shotTime + DELAY_TIME  < Time.time)
        {
            shotTime = Time.time;

            GameObject laser = Instantiate(linePrefab, laserOrigin.position,
                                Quaternion.identity) as GameObject;
            laser.transform.parent = this.transform;
            laser.transform.localRotation = Quaternion.identity;
            laser.transform.parent = null;
            laser.AddComponent<MoveLaser>();
        }

	}
}
