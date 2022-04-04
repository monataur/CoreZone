using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using UnityEngine.SceneManagement;

public class DiscordController : MonoBehaviour {

	public Discord.Discord discord;
	public string currentScene;
	public string sceneState;
	public bool presenceEnabled = true;
	public Discord.Activity bruh;



	// Use this for initialization
	void Start() 
		{
		discord = new Discord.Discord(841571937300905984, (System.UInt64)Discord.CreateFlags.Default);
		var activityManager = discord.GetActivityManager();
		var activity = new Discord.Activity
		{
			State = sceneState,
			Details = sceneState,
			Assets =
            {
				LargeImage = "cz2"
            },
			
			
		};
		activityManager.UpdateActivity(activity, (res) =>
		{
			if (res == Discord.Result.Ok)
			{
				Debug.Log("Success!");
			}
            else
            {
				Debug.Log("Failed");
            }
		});
	}

	public void Quit()
    {
		var activityManager = discord.GetActivityManager();
		var activity = new Discord.Activity
		{
		};
		activityManager.ClearActivity((result) =>
		{
			discord.Dispose();
		});
    }

	public void SceneState()
	{
		switch (currentScene)
		{
			case ("CharacterInitialization"):
				sceneState = "Creating a character";
				break;

			case ("Main"):
				sceneState = "Exploring the world";
				break;

			case ("Battle"):
				sceneState = "Battling a foe";
				break;

			case (null):
				sceneState = "Bugged! Sorry.";
				break;
		}
	}

	// Update is called once per frame
	void Update () {
		discord.RunCallbacks();
		currentScene = SceneManager.GetActiveScene().name;
		SceneState();
	}
}
