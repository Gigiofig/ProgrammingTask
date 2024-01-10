using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneNum = 0;
    public bool isNextScene = false;
    [SerializeField] public SceneInfo sceneInfo;
    public string sceneName;
    public void ChangeScene()
    {
        if (sceneNum == 1 && isNextScene)
        {
            sceneInfo.hasClothes = false;
            sceneInfo.hasHair = false;
            sceneInfo.hasHat = false;
            sceneInfo.hairIndex = 0;
            sceneInfo.hatIndex = 0;
            sceneInfo.clothesIndex = 0;
        }
        sceneInfo.isNextScene = isNextScene;
        sceneInfo.sceneNum = sceneNum;
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
