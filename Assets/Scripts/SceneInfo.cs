using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{
    public int sceneNum = 0;
    public bool isNextScene = false;
    public bool hasHat = false;
    public bool hasHair = false;
    public bool hasClothes = false;
    public int hairIndex = 0;
    public int hatIndex = 0;
    public int clothesIndex = 0;
}
