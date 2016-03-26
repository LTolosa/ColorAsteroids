using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
    private Transform player;
    public GameObject smallAsteroids;
    public float speed = 1f;

    public int type = 1;

    public Color color;


    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 5;
        if (type == 1)
        {
            player = GameObject.Find("OVRCameraRig").transform;
            this.transform.LookAt(player);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
            

    }

    // Update is called once per frame
    void Update () {
        if(this.transform.position.magnitude > GameController.spawnDistance + 5f)
        {
            this.transform.position -= 2 * transform.position;
        }

	}

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot"))
        {
            if (type == 1)
            {
                GameObject l_Asteroid = Instantiate(smallAsteroids, this.transform.position, this.transform.rotation) as GameObject;
                GameObject r_Asteroid = Instantiate(smallAsteroids, this.transform.position, this.transform.rotation) as GameObject;
                l_Asteroid.transform.Rotate(0, (float)Random.Range(30, 60), 0);
                r_Asteroid.transform.Rotate(0, (float)Random.Range(-60, -30), 0);
                l_Asteroid.GetComponent<Rigidbody>().velocity = l_Asteroid.transform.forward * speed / 2;
                r_Asteroid.GetComponent<Rigidbody>().velocity = r_Asteroid.transform.forward * speed / 2;
                l_Asteroid.GetComponent<Asteroid>().type++;
                r_Asteroid.GetComponent<Asteroid>().type++;
                GameController.numOfAsteroids++;
            }
            GameController.numOfAsteroids--;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
        }

        else
        {
            return;
        }
    }
   

}
