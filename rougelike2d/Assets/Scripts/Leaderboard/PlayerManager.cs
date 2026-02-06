using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    public Leaderboard leaderboard;
    private string playerID;

   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetechLeaderboardRoutine();
        yield return SetPlayerNameRoutine(); //set name
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                playerID = response.player_id.ToString();
                //PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Cloud not start sesssion");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void SetPlayerName(string name)
    {
        //string currentUsername = PlayerPrefs.GetString("InputFieldValue");
        LootLockerSDKManager.SetPlayerName(name, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully Set Player Name");
                //FindObjectOfType<ScreenLoader>().LoadScene("SampleScene");
            }
            else
            {
                Debug.Log("Failed to change name" + response.errorData);
                //FindObjectOfType<ScreenLoader>().LoadScene("SampleScene");
            }
        });
    }

    IEnumerator SetPlayerNameRoutine()
    {
        bool done = false;
        string currentUsername = PlayerPrefs.GetString("InputFieldValue", playerID.ToString());
        LootLockerSDKManager.SetPlayerName(currentUsername, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully Set Player Name");

            }
            else
            {
                Debug.Log("Failed to change name" + response.errorData);

            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public string GetPlayerID()
    {
        return playerID;
    }
}
