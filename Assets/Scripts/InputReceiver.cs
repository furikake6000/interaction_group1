using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public void InputCallbackSample(float range, Vector3 axis){
        Debug.Log("Accel Input detected! range: " + range + " axis: " + axis);
    }
}
