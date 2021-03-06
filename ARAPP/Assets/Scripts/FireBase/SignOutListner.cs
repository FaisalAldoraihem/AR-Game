using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignOutListner : MonoBehaviour
{
    public Button button;
    protected Firebase.Auth.FirebaseAuth auth;

    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        button.onClick.AddListener(() => SignOut());
    }

    void SignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene("Main");
    }
}
