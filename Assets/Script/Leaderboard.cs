using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using TMPro;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    public TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        GetAllScores();
    }

    // Update is called once per frame
   private void GetAllScores()
    {
        
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch user data: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                
                DataSnapshot snapshot = task.Result;
                var orderedUsers = snapshot.Children.Cast<DataSnapshot>()
                    .OrderByDescending(userSnapshot => int.Parse(userSnapshot.Child("score").Value.ToString()));
                int userscount = 0;

                foreach (DataSnapshot userSnapshot in orderedUsers)
                {
                    userscount++;
                    if(userscount < 10) {
                        string username = userSnapshot.Child("username").Value.ToString();
                        string score = userSnapshot.Child("score").Value.ToString();


                        Debug.Log("Username: " + username + ", Score: " + score + "\n");
                        label.text += "Username: " + username + ", Score: " + score + "\n";
                    }
                   
                }
            }
        });
    }
}

