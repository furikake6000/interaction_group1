using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccelInputEvent : UnityEngine.Events.UnityEvent<float, Vector3> { }

public class AccelInput : MonoBehaviour
{
    [SerializeField]
    AccelInputEvent events;
    [SerializeField, Range(0.0f, 10.0f)]
    float threshold;

    public static int perfectCounter = 0;
    public static int goodCounter = 0;
    public static int missCounter = 0;

    public static int getPerfect() {
        return perfectCounter;
    }

    public static int getGood() {
        return goodCounter;
    }

    public static int getMiss() {
        return missCounter;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 accel = Input.acceleration;
        if (accel.magnitude > threshold) {
            events.Invoke(accel.magnitude, accel.normalized);
        }
    }
}
