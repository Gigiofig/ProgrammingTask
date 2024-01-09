using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    public int skinNum;
    public Skins[] skins;
    public string className;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (skinNum > skins.Length - 1) skinNum = 0;
        else if (skinNum < 0) skinNum = skins.Length - 1;
    }

    void LateUpdate()
    {
        SkinChoice();
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains(className))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Remove(0, spriteName.Length - 1);
            int spriteNum = int.Parse(spriteName);
            spriteRenderer.sprite = skins[skinNum].sprites[spriteNum];
        }
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}