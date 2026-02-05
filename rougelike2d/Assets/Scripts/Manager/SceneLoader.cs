using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string name)
    {
        SoundManager.Instance.Play("button");
        SceneManager.LoadScene(name);
    }

    public IEnumerator Enum_LoadAsynchronously(string levelName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(4f);
        operation.allowSceneActivation = true;

    }

}
