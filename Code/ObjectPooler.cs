using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> _pool;
    private GameObject _poolContainer;

    //Makes sure we have a group of enemies in a list and place to hold them
    private void Awake()
    {
        _pool = new List<GameObject>();
        _poolContainer = new GameObject($"Pool -{prefab.name}");
        CreatePooler();
    }

    //If we want to add more enemies, this allows us to increase that
    private void CreatePooler()
    {
        for ( int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInstance());
        }
    }

    //This is the fucntion that creates the instantiation of the enemy, and will then be called in Spawner.cs
    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }

    //This should be the moment where we gather all the connections from the instance to create them
    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        
        }

       return CreateInstance();
    }
    
    //Throws the instance back
    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }

    //creates the delay for the kbjects in the pool
    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }
}
