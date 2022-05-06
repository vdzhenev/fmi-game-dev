using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public SceneAsset mainScene;
    public SceneAsset gameScene;
    //public SceneAsset optionsScene;

    public void LoadMenu()
    {
        SceneManager.LoadScene(mainScene.name);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameScene.name);
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
