using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject player;

    public GameObject asteroids;
    public Text waveCount;
    public Text endgame;
    public int hazardCount;

    public static float spawnDistance = 100f;

    public float startWait;
    public float spawnWait;
    public float explosionDuration = 1f;

    public static int numOfAsteroids = 0;

    private int waveNum = 1;

    private static Queue<ParticleSystem> explosions = new Queue<ParticleSystem>();
    private static Queue<float> explosionTimes = new Queue<float>();

    private static Queue<ParticleSystem> deadExplosions = new Queue<ParticleSystem>();
    private static Queue<float> deadExplosionTimes = new Queue<float>();
    private float deadExplosionDuration = 5f;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) {
            waveCount.text = "<color=#72cae8ff>Wave</color> " + waveNum++;
            GameController.numOfAsteroids = 0;
            for (int i = 0; i < hazardCount; i++)
            {
                GameController.numOfAsteroids++;
                Vector3 spawnPosition = Random.insideUnitSphere * spawnDistance;
                Quaternion spawnRotation = Quaternion.identity;
                GameObject asteroid = Instantiate(asteroids, spawnPosition, spawnRotation) as GameObject;
                yield return new WaitForSeconds(spawnWait);
            }
            hazardCount++;
            while (GameController.numOfAsteroids != 0)
                yield return null;
        }

    }

    void Update()
    {
        ManageExplosions();
        if(Health.health <= 0)
        {
            StopCoroutine(SpawnWaves());
            GameObject[] tmp = GameObject.FindGameObjectsWithTag("Asteroid");
            foreach(GameObject a in tmp){
                Destroy(a);
            }
            Color c = endgame.color;
            c.a = 1;
            endgame.color = c;
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
                Health.health = 100f;
                Health.score = 0;
            }
        }
    }

    public static void AddExplosion(ParticleSystem explosion)
    {
        explosions.Enqueue(explosion);
        explosionTimes.Enqueue(Time.time);
    }

    private void ManageExplosions()
    {
        if(explosions.Count > 0)
        {
            if(Time.time - explosionTimes.Peek() > explosionDuration)
            {
                explosionTimes.Dequeue();
                ParticleSystem explosion = explosions.Dequeue();
                explosion.Stop();
                deadExplosions.Enqueue(explosion);
                deadExplosionTimes.Enqueue(Time.time);
            }
        }
        if(deadExplosions.Count > 0)
        {
            if(Time.time - deadExplosionTimes.Peek() > deadExplosionDuration)
            {
                Destroy(deadExplosions.Dequeue());
                deadExplosionTimes.Dequeue();
            }
        }
    }
}
