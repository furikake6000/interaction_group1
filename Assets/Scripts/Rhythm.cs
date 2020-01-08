using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour
{

    [SerializeField] float interval = 1f;
    [SerializeField] AudioSource pingpong;

    [SerializeField] float timeMargin = 0.1f;

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

    public void Shake(float x, Vector3 v){
        float slack = Mathf.Min(elapsedTime, 1.0f - elapsedTime);
        if (slack < timeMargin){
            Debug.Log("OK! " + slack);
        } else {
            Debug.Log("NG " + slack);
        }
    }
}
