using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour
{

    [SerializeField] float interval = 1f;
    [SerializeField] AudioSource pingpong;

    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= interval) {
            elapsedTime -= interval;
            pingpong.Play();
            Debug.Log("sound!");
        }
    }
}
