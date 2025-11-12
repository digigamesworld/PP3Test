using UnityEngine;

namespace UPatterns.Messaging
{
    public static class UToast
    {
        public static void ShowToast(string message)
        {
            // if (Application.platform == RuntimePlatform.Android)
            // {
            //     AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //     AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            //     currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            //     {
            //         AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
            //         AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);
            //         AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", currentActivity, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
            //         toast.Call("show");
            //     }));
            // }
            // else
            // {
            //     Debug.Log("Toast messages are only supported on Android.");
            // }
        }
    }
}