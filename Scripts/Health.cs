using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public float health = 100f;
    public static int score = 0;

    public Text scoreText;
    public Text display;
    public Material start;
    public Material end;
    public GameObject sphere;
    public AudioSource[] sources;
    private bool hit = false;
    private float t = 1.0f;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        display.text = health + " HP";
        scoreText.text = "Score: " + score;
        sphere.GetComponent<Renderer>().material.SetColor("_TintColor", Color.Lerp(Color.red, Color.black, t));

        t += Time.deltaTime;
        if(health <= 0)
        {
            sources[1].Play();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            health -= 2f;
            t = 0.0f;
            sources[0].Play();
            sphere.GetComponent<Renderer>().material.color = Color.red;
            Destroy(other.gameObject);
            GameController.numOfAsteroids--;
        }
        else
        {
            return;
        }
    }
}
