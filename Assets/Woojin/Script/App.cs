using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum eSceneType
{
    App, Title, Lobby, InGame
}
public class App : MonoBehaviour
{
    private eSceneType sceneType;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
    }

    private void Start()
    {
        Application.targetFrameRate = 30;

        DontDestroyOnLoad(this.gameObject);

        this.ChangeScene(eSceneType.Title);
    }

    public void ChangeScene(eSceneType sceneType)
    {

        switch (sceneType)
        {
            case eSceneType.Title:
                {
                    AsyncOperation ao = SceneManager.LoadSceneAsync("Title");
                    ao.completed += (obj) =>
                    {
                        Debug.Log(obj.isDone);

                        var title = GameObject.FindObjectOfType<Title>();
                        title.onComplete = () =>
                        {
                            Debug.Log("Title");
                            this.ChangeScene(eSceneType.Lobby);
                        };

                        title.Init();

                    };
                }
                break;

            case eSceneType.Lobby:
                {
                    AsyncOperation ao = SceneManager.LoadSceneAsync("Lobby");
                    ao.completed += (obj) =>
                    {
                        Debug.Log(obj.isDone);

                        var lobby = GameObject.FindObjectOfType<Lobby>();

                        lobby.OnGameStart = () =>
                         {
                             this.ChangeScene(eSceneType.InGame);
                         };

                    };
                }
                break;

            case eSceneType.InGame:
                {
                    Debug.Log("InGame");
                    var Ingame = GameObject.FindObjectOfType<InGame>();

                }
                break;
        }

    }
}