using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public GameObject AudioSource;
    public GameObject AudioSourceP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(AudioSource, AudioSourceP.transform);
        AudioSource.GetComponent<AudioSource>().playOnAwake = true;

    }
}
