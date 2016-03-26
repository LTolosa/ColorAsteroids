using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    // Reference to Volumetric Line PreFab
    public GameObject linePrefab;

    private const float DELAY_TIME = 1f;
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

            GameObject laser = Instantiate(linePrefab, this.transform.position + Vector3.down, Quaternion.Euler(-10, 0, 0)) as GameObject;
            laser.AddComponent<MoveLaser>();
            laser.GetComponent<VolumetricLines.VolumetricLineBehavior>().LineWidth = 0.5f;
        }
	
	}
}
