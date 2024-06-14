using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTheme : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource source;
    private int indexOfAuido;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            indexOfAuido = Random.Range(0, clips.Length);
            source.clip = clips[indexOfAuido];
            source.Play();
        }

    }
}
