using UnityEngine;
using System.Collections;

public class MoveLaser : MonoBehaviour {

    public float speed = 300f;

    private Vector3 startpos;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        startpos = rb.position;
        rb.velocity = transform.forward * speed;
	}

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(rb.position, startpos) > GameController.spawnDistance) {
            Destroy(this.gameObject);
        }
	}
}
