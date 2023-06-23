using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawn : MonoBehaviour
{
    //[SerializeField] GameObject[] itemObjectPrefabs;
    [SerializeField] GameObject[] dropItems;

    int randomIndex = 0;
    float randomPositionX = 0f;

    int inactiveIndex = 0;

    private void Start()
    {
        //StartCoroutine(SpawnDropItems_co());
        StartCoroutine(SetActiveDropItem_co());
    }

    private void Update()
    {
        
    }

    IEnumerator SetActiveDropItem_co()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < dropItems.Length; i++)
            {
                if (!dropItems[i].activeSelf)
                {
                    inactiveIndex = i;
                    break;
                }
            }

            dropItems[inactiveIndex].transform.localPosition = new Vector3(dropItems[inactiveIndex].transform.localPosition.x, dropItems[inactiveIndex].transform.localPosition.y, 0f);
            dropItems[inactiveIndex].SetActive(true);
        }
    }

    /*
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
    */
}
