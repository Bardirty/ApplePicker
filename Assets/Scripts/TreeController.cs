using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private int applesLimit;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float radius;
    [SerializeField] private float height;
    [SerializeField] private float treeLifeDuration;
    [SerializeField] private float appleLifeDuration;
    private Transform apples;
    void Start()
    {
        apples = GameObject.Find("Apples").transform;
        Destroy(gameObject, treeLifeDuration);
        StartCoroutine(GenerateApple());
    }
    private IEnumerator GenerateApple()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (apples.childCount < applesLimit)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-radius, radius), height, Random.Range(-radius, radius));
            GameObject curApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, apples);
            curApple.transform.localScale = Vector3.one * 0.5f;
            Destroy(curApple, appleLifeDuration);
        }
        StartCoroutine(GenerateApple());
    }
}