using UnityEngine;
using System.Collections;

public class MoveLaser : MonoBehaviour {

    public float speed = 10f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.position += speed * Time.deltaTime * transform.forward;
        //rb.AddForce(transform.forward * speed * Time.deltaTime);
	}
}
