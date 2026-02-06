using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;

 
    string leaderboardID = "32957";

    public TextMeshProUGUI playerNamesTxt;
    public TextMeshProUGUI playerScoresTxt;
    public TextMeshProUGUI personalNameTxt;
    public TextMeshProUGUI personalScoreTxt;

    public GameObject loadingGo;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerManager.Instance.GetPlayerID();
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        }));
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetechLeaderboardRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 8, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully feteched leaderboard");

                string tempPlayerNames = "Ranks\n <br>";
                string tempPlayerScores = "Highscores\n <br>";

                LootLockerLeaderboardMember[] member = response.items;

                for (int i = 0; i < member.Length; i++)
                {
                    tempPlayerNames += member[i].rank + ". ";
                    if (member[i].player.name != "")
                    {
                        tempPlayerNames += member[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += member[i].player.id;
                    }
                    tempPlayerScores += member[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNamesTxt.text = tempPlayerNames;
                playerScoresTxt.text = tempPlayerScores;
                
                if(loadingGo != null)
                {
                    loadingGo.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Failed fetching leaderboard" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void FetechPersonalScore()
    {
        StartCoroutine(FetechPersonalScoreRoutine());
    }

    public IEnumerator FetechPersonalScoreRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetMemberRank(leaderboardID, PlayerManager.Instance.GetPlayerID(), (response) =>
        {
            if (response.success)
            {
                Debug.Log("Sucessfully feteched player's perosnal score");
                Debug.Log("Player's score: " + response.score);
                Debug.Log("Player's rank: " + response.rank);

                personalNameTxt.text = "Your Rank\n <br>" + response.rank.ToString() + ". " + response.player.name;
                personalScoreTxt.text = "Your HighScore\n <br>" + response.score.ToString();
                //personalNameAndScoreTxt.text = "Personal :- " + response.rank + ". " + response.player.name + " = " + response.score;

                done = true;
            }
            else
            {
                Debug.Log("Failed fetching personal score " + response.errorData.message);

                Debug.Log("Failed to get player's score");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}