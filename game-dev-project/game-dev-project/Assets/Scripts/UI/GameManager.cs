using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneAsset mainScene;
    public SceneAsset mapScene;
    public SceneAsset roomScene;
    //public SceneAsset optionsScene;

    public void LoadMenu()
    {
        SceneManager.LoadScene(mainScene.name);
        GameObject.Find("MapCanvas").SetActive(false);
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(mapScene.name);
        GameObject.Find("MapCanvas").SetActive(true);
    }

    public void LoadRoom()
    {
        SceneManager.LoadScene(roomScene.name);
        GameObject.Find("MapCanvas").SetActive(false);
    }

    //public void LoadOptionsMenu()
    //{
    //    SceneManager.LoadScene(optionsScene.name);
    //}

    public void ExitGame()
    {
        Application.Quit();
    }
}
