using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class GooglePlayGamesConnector : MonoBehaviour
{
	[SerializeField] private string leaderboard;

	private void Start()
	{
		PlayGamesPlatform.Activate();
		Login();
	}

	private void Login()
	{
		Social.localUser.Authenticate((bool success) =>
		{
			
		});
	}

	public void ShowLeaderboard()
	{
		PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboard);	
	}
}
