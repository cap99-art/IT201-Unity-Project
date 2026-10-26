using System.Runtime.CompilerServices;
using UnityEngine;

public class Week7LiveDemo : MonoBehaviour
{
    [Header("Select the current step")]
    [Range(1, 5)][SerializeField] private int demoStep = 1;

    [SerializeField] private GameObject objectPrefab;
    private GameObject[] spawnedObjects;// = new GameObject[5];

    void Start()
    {
        if (demoStep == 1) CreateOneCube();
        else if (demoStep == 2) CreateCubesWithLoop();
        else if (demoStep == 3) SpawnOnePrefab();
        else if (demoStep == 4)
        {
            spawnedObjects = new GameObject[5]; 
            SpawnPrefabsAndStoreArray();
        }
        else if (demoStep == 5)
        {
            spawnedObjects = new GameObject[5];
            SpawnPrefabsAndStoreArray();
            ScalePrefabs();
        }
    }

    private void ScalePrefabs()
    {
        if (spawnedObjects == null) return;
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            if (spawnedObjects[i] == null) continue;
            // spawnedObjects[i].transform.localScale = Vector3.one * (i + 1);
            spawnedObjects[i].transform.localScale = Vector3.one * Random.Range(1f, 10f);

        }
    }

    private void SpawnPrefabsAndStoreArray()
    {
        if (objectPrefab == null)
        {
            Debug.Log("Prefab is missing!");
            return;
        }


        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPos = new Vector3(i * 2f, 0f, 0f);
            spawnedObjects[i] = Instantiate(objectPrefab, spawnPos, Quaternion.identity);
        }
        Debug.Log("Array length " + spawnedObjects.Length);
    }

    private void SpawnOnePrefab()
    {
        if (objectPrefab != null)
        {
            Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.Log("Prefab is missing!");
        }
    }

    private void CreateCubesWithLoop()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(i * 2f, 0f, 0f);
            Debug.Log("Created one cube");
        }
    }
    private void CreateOneCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = Vector3.zero;
        Debug.Log("Created one cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
