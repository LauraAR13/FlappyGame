using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class SetScore : MonoBehaviour 
{
	public TextMesh bestScoreLabel;
	public TextMesh scoreLabel;

	void Start () 
	{
		scoreLabel.text = "Score: " + GameManager.score.ToString();

		if (GameManager.score > 0)
		{
			if (PlayerPrefs.GetInt("Score", 0) < GameManager.score)
			{
                FirebaseDatabase.DefaultInstance.RootReference
					.Child("users").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
					.Child("score").SetValueAsync(GameManager.score);
                PlayerPrefs.SetInt("Score", GameManager.score);
				PlayerPrefs.Save();
			}
		}

		bestScoreLabel.text = "HighScore: " + PlayerPrefs.GetInt("Score", 0).ToString();
		GameManager.score = 0;
	}
}