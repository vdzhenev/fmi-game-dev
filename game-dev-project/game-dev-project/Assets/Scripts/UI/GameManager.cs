using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneAsset mainScene;
    public SceneAsset mapScene;
    public SceneAsset battleScene;
    public SceneAsset shopScene;
    public SceneAsset eventScene;
    public SceneAsset restScene;
    //public SceneAsset optionsScene;

    public void LoadMenu()
    {
        ShowHideMap(false);
        ShowHidePlayers(false);
        SceneManager.LoadScene(mainScene.name);
    }

    public void LoadMap()
    {
        ShowHideMap(true);
        ShowHidePlayers(false);
        SceneManager.LoadScene(mapScene.name);
    }

    public void LoadRoom(Node.Type type)
    {
        ShowHideMap(false);
        ShowHidePlayers(true);
        switch (type)
        {
            case Node.Type.Battle:
                SceneManager.LoadScene(battleScene.name);
                break;
            case Node.Type.Elite:
                break;
            case Node.Type.Mystery:
                SceneManager.LoadScene(eventScene.name);
                break;
            case Node.Type.Rest:
                SceneManager.LoadScene(restScene.name);
                break;
            case Node.Type.Shop:
                SceneManager.LoadScene(shopScene.name);
                break;
            default:
                Debug.Log("Couldn't identify room type!");
                break;
        }
        //Debug.Log(type);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ShowHidePlayers(bool show)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject p in players)
        {
            p.SetActive(show);
        }
    }

    private void ShowHideMap(bool show)
    {
        GameObject MapCanvas = GameObject.FindGameObjectWithTag("MapCanvas");
        if(MapCanvas != null)
        {
            MapCanvas.GetComponent<Canvas>().enabled = show;
        }
    }
}
