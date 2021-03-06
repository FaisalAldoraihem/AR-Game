using UnityEngine;
using Firebase.Extensions;
using Firebase.Auth;

public class AuthSetup : MonoBehaviour
{
    public AuthManager authManager;
    private Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

    public FirebaseAuth GetAuth()
    {
        return authManager.auth;
    }
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Initilizing Firebase");
                authManager.InitializeFireBase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
}
