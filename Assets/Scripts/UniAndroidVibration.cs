using UnityEngine;

public class UniAndroidVibration : MonoBehaviour
{
    private const string PackageClassPath = "net.sanukin.vibration.UniVibration";
    

    public void VibrateCallback(float a, Vector3 b){
        Vibrate(1000);
    }

    public void Vibrate(int milliseconds)
    {
// // #if !UNITY_EDITOR && UNITY_ANDROID
//         Debug.Log("hoge");
//         var javaClass = new AndroidJavaClass(PackageClassPath);
//         javaClass.CallStatic("vibrate", milliseconds);
// // #endif
        Handheld.Vibrate ();
    }
}
