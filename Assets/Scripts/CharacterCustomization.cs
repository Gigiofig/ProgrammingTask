using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    public int skinNum;
    public Skins[] skins;
    public string className;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite != null)
            SkinChoice();
    }

    void Update()
    {
        if (skinNum > skins.Length - 1) skinNum = 0;
        else if (skinNum < 0) skinNum = skins.Length - 1;
    }

    public void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains(className))
        {
            string spriteName = spriteRenderer.sprite.name;
            int pos = spriteName.LastIndexOf('_');
            spriteName = spriteName.Remove(0, pos + 1);
            int spriteNum = int.Parse(spriteName);
            spriteRenderer.sprite = skins[skinNum].sprites[spriteNum];
        }
    }

    public void SetSkin(int spriteNum)
    {
        spriteRenderer.sprite = skins[skinNum].sprites[spriteNum];
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}