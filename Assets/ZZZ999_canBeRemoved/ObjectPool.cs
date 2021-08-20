using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // properties
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 10;
    [SerializeField] [Range(0.1f, 30f)] float spawnerTimer = 1f;

    // member variables
    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, this.transform);

            pool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnablePoolObject();

            yield return new WaitForSeconds(spawnerTimer);
        }
    }

    private void EnablePoolObject()
    {
        foreach (GameObject poolMember in pool)
        {
            if (!poolMember.activeInHierarchy)
            {
                poolMember.SetActive(true);
                return;
            }
        }
    }
}
