using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;

public class RecoverPassword : MonoBehaviour
{

    [SerializeField]
    private Button _buttonRecovery;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void resetpassword()
    {
        var emailInput = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>();
        string email = emailInput.text;
        Debug.Log(email);
        FirebaseAuth.DefaultInstance.SendPasswordResetEmailAsync(email).ContinueWith(task =>

        {
            if (task.IsCanceled)
            {
                Debug.LogError("Password reset was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Password reset encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }
}
