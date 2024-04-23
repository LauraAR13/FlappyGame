using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class LoadSceneWhenAuthenticated : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad = "Game";

    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
    }

    private void HandleAuthStateChange(object sender, EventArgs e)
    {
       if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChange;
    }
}
