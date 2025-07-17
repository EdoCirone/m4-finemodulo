using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size = 10;
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab.gameObject);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public IPooledObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool con tag {tag} non trovata!");
            return null;
        }

        Queue<GameObject> poolQueue = _poolDictionary[tag];

        GameObject objToSpawn;

        if (poolQueue.Count == 0)
        {
            // Pool esaurita, istanzio uno nuovo
            Debug.LogWarning($"Pool '{tag}' esaurita! Creo un nuovo oggetto.");

            // Trova il prefab corrispondente
            Pool poolData = _pools.Find(p => p.tag == tag);
            if (poolData == null || poolData.prefab == null)
            {
                Debug.LogError($"Nessun prefab trovato per la pool con tag '{tag}'");
                return null;
            }

            objToSpawn = Instantiate(poolData.prefab);
        }
        else
        {
            objToSpawn = poolQueue.Dequeue();
        }

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objToSpawn.GetComponent<IPooledObject>();
        pooledObj?.SetPoolTag(tag);
        pooledObj?.OnObjectSpawn();

        return pooledObj;
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            Debug.LogWarning("Tentativo di ritornare un oggetto con tag nullo o vuoto.");
            return;
        }

        if (_poolDictionary.ContainsKey(tag))
        {
            _poolDictionary[tag].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning($"Nessuna pool trovata per {tag}");
        }
    }
}
