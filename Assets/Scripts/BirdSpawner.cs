using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    private BoidController Player;
    public List<GameObject> Birds = new List<GameObject>();
    private void Start()
    {
        Player = FindObjectOfType<BoidController>();
        StartCoroutine(spawnBirds());
    }

    IEnumerator spawnBirds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 10f));
            Instantiate(Birds[Random.Range(0,Birds.Count-1)],new Vector3(Random.Range(Player.transform.position.x-100, Player.transform.position.x + 100), Random.Range(50,150), Random.Range(Player.transform.position.x - 100, Player.transform.position.x + 100)),Quaternion.identity);
        }
    }
}
