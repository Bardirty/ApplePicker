using System.Collections;
using UnityEngine;
public class LevelController : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private float radius;
    [SerializeField] private int treeLimit;
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private Transform trees;
    [SerializeField] private Terrain terrain;
    private void Start()
    {
        StartCoroutine(GenerateTree());
    }

    private IEnumerator GenerateTree()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (trees.childCount < treeLimit)
        {
            GameObject curPrefab = treePrefabs[0];
            if (Random.Range(0, 10) == 5) curPrefab = treePrefabs[1];
            Vector3 spawnPosition = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            spawnPosition.y = terrain.SampleHeight(spawnPosition) + terrain.transform.position.y;
            Instantiate(curPrefab, spawnPosition, Quaternion.identity, trees);
        }
        StartCoroutine(GenerateTree());
    }
}