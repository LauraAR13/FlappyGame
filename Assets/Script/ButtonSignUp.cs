using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSignUp : MonoBehaviour
{
    [SerializeField] 
    private Button _signupButton;
    [SerializeField]
    private TMP_InputField _usernameInputField;
    
    private DatabaseReference _databaseReference;
    // Start is called before the first frame update
    void Reset()
    {
        _signupButton = GetComponent<Button>();
        _usernameInputField = GameObject.Find("InputFieldUsername").GetComponent <TMP_InputField>();
    }

   private void Start ()
    {
        _signupButton.onClick.AddListener(HandleSignUpButton);
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void HandleSignUpButton()
    {
        Debug.Log("HandleRegisterButtonClicked");
       
            string email = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>().text;
            string password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;
        StartCoroutine(SignUpUser(email, password));
        
        

    }

    private IEnumerator SignUpUser (string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(()=> registerTask.IsCompleted);

        if (registerTask.IsCanceled )
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
        }

        else if (registerTask.IsFaulted )
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error"+registerTask.Exception);
        }
        else
        {
            //Firebase user was created
            AuthResult result = registerTask.Result;
            Debug.LogFormat("Firebase user created succesfully; {0}({1})",result.User.DisplayName,result.User.UserId);
            _databaseReference.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(_usernameInputField.text);
            _databaseReference.Child("users").Child(result.User.UserId).Child("score").SetValueAsync(0);

        }
    }
}
