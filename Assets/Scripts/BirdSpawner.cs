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
            yield return new WaitForSeconds(Random.Range(5f, 13f));
            float offsetX = Random.Range(-75f, 75f);
            float offsetZ = Random.Range(-75f, 120f);
            Vector3 spawnPosition = new Vector3(Player.transform.position.x + offsetX, Random.Range(60f, 80f), Player.transform.position.z + offsetZ);
            Instantiate(Birds[Random.Range(0, Birds.Count)], spawnPosition, Quaternion.identity);
        }
    }
}
