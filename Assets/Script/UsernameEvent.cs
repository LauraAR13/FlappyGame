using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameEvent : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;
    // Start is called before the first frame update
    void Reset()
    {
        _label = GetComponent<TMP_Text>();
    }
    void Start()
    {
        FirebaseDatabase.DefaultInstance
           .GetReference($"users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/username")
        .ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }

        _label.text = "Welcome "+ (string)args.Snapshot.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
