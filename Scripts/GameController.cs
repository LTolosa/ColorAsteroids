using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject player;

    public GameObject asteroids;
    public int hazardCount;

    public static float spawnDistance = 500f;

    public float startWait;
    public float spawnWait;
    public float waveWait;

    public static int numOfAsteroids = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves()); ;
	}
	
	IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) {
            GameController.numOfAsteroids = 0;
            for (int i = 0; i < hazardCount; i++)
            {
                GameController.numOfAsteroids++;
                Vector3 spawnPosition = Random.insideUnitSphere * spawnDistance;
                Quaternion spawnRotation = Quaternion.identity;
                GameObject asteroid = Instantiate(asteroids, spawnPosition, spawnRotation) as GameObject;
                yield return new WaitForSeconds(spawnWait);
            }

            while (GameController.numOfAsteroids != 0)
                yield return null;
        }

    }


}
