using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ColorPair
{
    public Color _color1;
    public Color _color2;
    public ColorPair(Color color1, Color color2)
    {
        _color1 = color1;
        _color2 = color2;
    }
}

public class Asteroid : MonoBehaviour {
    public GameObject bigAsteroids;
    public GameObject smallAsteroids;
    public GameObject shot;
    public float speed = 1f;
    public ParticleSystem explosion;

    public int type = 1;

    public Color color;

    private static Color[] colors = { Color.red, Color.cyan, Color.yellow };

    public static Dictionary<ColorPair, Color> secondaryColors = new Dictionary<ColorPair, Color>
    {
        {new ColorPair(Color.red, Color.cyan), Color.magenta},
        { new ColorPair(Color.red, Color.yellow), new Color(1.0f, 0.5f, 0) },
        {new ColorPair(Color.cyan, Color.yellow), Color.green }/*
        {new ColorPair(Color.yellow, Color.red), new Color(1.0f, 0.5f, 0) }
        {new ColorPair(Color.cyan, Color.red), Color.magenta },
        {new ColorPair(Color.yellow, Color.cyan), Color.green }*/

    };

    public static Dictionary<Color, ColorPair> secondaryColors_B = new Dictionary<Color, ColorPair>
    {
        {Color.magenta, new ColorPair(Color.red, Color.cyan)},
        { new Color(1.0f, 0.5f, 0), new ColorPair(Color.red, Color.yellow )},
        { Color.green , new ColorPair(Color.cyan, Color.yellow)},
        {Color.red, new ColorPair(Color.red, Color.red) }

    };

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        Vector3 otherPoint = Random.insideUnitSphere * GameController.spawnDistance;
        //this.transform.LookAt(otherPoint);
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        
        if(type == 1)
        {
            int randColor = Random.Range(0, 3);
            this.GetComponent<Flashy>().lineColor = colors[randColor];

            color = this.GetComponent<Flashy>().lineColor;
        }
       
    }

    // Update is called once per frame
    void Update () {
        if(this.transform.position.magnitude > GameController.spawnDistance + 5f)
        {
            this.transform.position -= 1.99f * transform.position;
        }
	}

    
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
        Collider other = collision.collider;
        if (other.CompareTag("Shot"))
        {
           
            if (type == 1 || type == 3)
            {
                Health.score++;
                if (color == Color.red || secondaryColors.ContainsValue(color))
                {
                    GameObject l_Asteroid = Instantiate(smallAsteroids, this.transform.position, this.transform.rotation) as GameObject;
                    GameObject r_Asteroid = Instantiate(smallAsteroids, this.transform.position, this.transform.rotation) as GameObject;
                    l_Asteroid.transform.Rotate(0, (float)Random.Range(30, 60), 0);
                    r_Asteroid.transform.Rotate(0, (float)Random.Range(-60, -30), 0);
                    l_Asteroid.GetComponent<Rigidbody>().velocity = l_Asteroid.transform.forward * speed;
                    r_Asteroid.GetComponent<Rigidbody>().velocity = r_Asteroid.transform.forward * speed;
                    l_Asteroid.GetComponent<Asteroid>().type++;
                    r_Asteroid.GetComponent<Asteroid>().type++;
                    GameController.numOfAsteroids += 2;
                    

                    r_Asteroid.GetComponent<Flashy>().lineColor = secondaryColors_B[color]._color1;
                    l_Asteroid.GetComponent<Flashy>().lineColor = secondaryColors_B[color]._color2;
                }
                else if(color == Color.cyan)
                {
                    GameObject l_Asteroid = Instantiate(smallAsteroids, this.transform.position, this.transform.rotation) as GameObject;
                    GameObject r_Asteroid = Instantiate(smallAsteroids, this.transform.position, this.transform.rotation) as GameObject;
                    l_Asteroid.transform.Rotate(0, (float)Random.Range(30, 60), 0);
                    r_Asteroid.transform.Rotate(0, (float)Random.Range(-60, -30), 0);
                    l_Asteroid.GetComponent<Rigidbody>().velocity = l_Asteroid.transform.forward * speed * 1.5f;
                    r_Asteroid.GetComponent<Rigidbody>().velocity = r_Asteroid.transform.forward * speed * 1.5f;
                    l_Asteroid.GetComponent<Asteroid>().type++;
                    r_Asteroid.GetComponent<Asteroid>().type++;
                    GameController.numOfAsteroids += 2;


                    r_Asteroid.GetComponent<Flashy>().lineColor = colors[2];
                    l_Asteroid.GetComponent<Flashy>().lineColor = colors[1];
                }
                else if(color == Color.yellow)
                {
                    GameObject l_Shot = Instantiate(shot, this.transform.position, this.transform.rotation) as GameObject;
                    GameObject r_Shot = Instantiate(shot, this.transform.position, this.transform.rotation) as GameObject;
                    l_Shot.transform.Rotate(0, (float)Random.Range(30, 60), 0);
                    r_Shot.transform.Rotate(0, (float)Random.Range(-60, -30), 0);
                    l_Shot.AddComponent<MoveLaser>();
                    r_Shot.AddComponent<MoveLaser>();
                }

            }
            GameController.numOfAsteroids--;

            ParticleSystem newExplosion = Instantiate<ParticleSystem>(explosion);
            newExplosion.transform.position = transform.position;
            GameController.AddExplosion(newExplosion);

            this.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
        }
        else if (other.CompareTag("Asteroid")){
            ColorPair colorPair = new ColorPair(color, other.GetComponent<Asteroid>().color);

            if (secondaryColors.ContainsKey(colorPair)) { 
                GameObject child = Instantiate(bigAsteroids, other.transform.position, other.transform.rotation) as GameObject;
                child.GetComponent<Asteroid>().type = 3;
                child.GetComponent<Asteroid>().color = secondaryColors[colorPair];
                child.GetComponent<Rigidbody>().velocity = Vector3.zero;

                child.GetComponent<Flashy>().lineColor = child.GetComponent<Asteroid>().color;
                GameController.numOfAsteroids--;
                Destroy(other.gameObject);
                Destroy(this.gameObject);
               
            }
            else return;
        }
        else
        {
            return;
        }
    }
}
