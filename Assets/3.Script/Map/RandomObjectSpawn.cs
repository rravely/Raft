using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] itemObjectPrefabs;

    int randomIndex = 0;
    float randomPositionX = 0f;

    private void Start()
    {
        StartCoroutine(SpawnDropItems_co());
    }

    IEnumerator SpawnDropItems_co()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            randomIndex = Random.Range(0, 4);
            randomPositionX = Random.Range(-100, 100) * 0.001f;

            GameObject dropItem = Instantiate(itemObjectPrefabs[randomIndex], new Vector3(randomPositionX, transform.position.y, transform.position.z), Quaternion.identity);
            dropItem.transform.SetParent(transform);
        }
    }
}
