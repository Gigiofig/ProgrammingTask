using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorNPCScript : MonoBehaviour
{
    int recolorAttempts = 6;
    int failCounter = 0;
    [Header("Shop Events")]
    [SerializeField] TMPro.TMP_Text welcomeText;
    [SerializeField] Button checkoutButton, closeShopButton, hatBTN, hairBTN, clothesBTN;
    [SerializeField] GameObject shopUI;
    [SerializeField] TMPro.TMP_Text balanceText;
    [SerializeField] Image hairPreview, hatPreview, clothesPreview;
    [SerializeField] PlayerScript player;
    public SpriteRenderer inAreaNotification;
    private Color hairIndex = new Color(1, 1, 1, 1);
    private Color hatIndex = new Color(1, 1, 1, 1);
    private Color clothesIndex = new Color(1, 1, 1, 1);
    private GameObject selectedObject;
    private Image currentSelection;
    private float R = 0;
    private float G = 0;
    private float B = 0;

    private void Start()
    {
        UpdateBalance();
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        if (!player.ClothesComponent.enabled)
        {
            clothesPreview.enabled = false;
            clothesBTN.interactable = false;
            failCounter += 1;
        }
        else
        {
            clothesPreview.enabled = true;
            clothesBTN.interactable = true;
            clothesPreview.sprite = player.ClothesComponent.sprite;
        }
        if (!player.HairComponent.enabled)
        {
            hairPreview.enabled = false;
            hairBTN.interactable = false;
            failCounter += 1;
        }
        else
        {
            hairPreview.enabled = true;
            hairBTN.interactable = true;
            hairPreview.sprite = player.HairComponent.sprite;
        }
        if (!player.HatComponent.enabled)
        {
            hatPreview.enabled = false;
            hatBTN.interactable = false;
            failCounter += 1;
        }
        else
        {
            hatBTN.interactable = true;
            hatPreview.enabled = true;
            hatPreview.sprite = player.HatComponent.sprite;
        }
        if (failCounter == 3)
        {
            welcomeText.text = "Looks like you have nothing on you to recolor. Go talk to my brother and come back here.";
        }
    }

    public void SetRed(float sliderValue)
    {
        R = sliderValue;
        SetColor();
    }
    public void SetGreen(float sliderValue)
    {
        G = sliderValue;
        SetColor();
    }
    public void SetBlue(float sliderValue)
    {
        B = sliderValue;
        SetColor();
    }

    public void SetColor()
    {
        currentSelection.color = new Color(R, G, B, 1);
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void SelectAccessory()
    {
        GameObject ClickedButton = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null) selectedObject.GetComponent<Image>().color = new Color(0.2250406f, 0.5660378f, 0.09344962f, 1f);
        selectedObject = ClickedButton;
        selectedObject.GetComponent<Image>().color = new Color(0.5682794f, 0.8962264f, 0.4438857f, 1f);
        if (ClickedButton.name.Contains("Hair"))
        {
            currentSelection = hairPreview;
            hatPreview.color = hatIndex;
            clothesPreview.color = clothesIndex;
        }
        else if (ClickedButton.name.Contains("Hat"))
        {
            currentSelection = hatPreview;
            hairPreview.color = hairIndex;
            clothesPreview.color = clothesIndex;
        }
        else if (ClickedButton.name.Contains("Clothes"))
        {
            currentSelection = clothesPreview;
            hatPreview.color = hatIndex;
            hairPreview.color = hairIndex;
        }
    }

    public void ConfirmSelection()
    {
        if (new Color(1, 1, 1, 1) == new Color(R, G, B, 1) && recolorAttempts < 7)
        {
            recolorAttempts += 1;
        }
        else if (recolorAttempts > 0 && new Color(1, 1, 1, 1) != new Color(R, G, B, 1))
        {
            recolorAttempts -= 1;
        }
        if (selectedObject != null && recolorAttempts >= 0)
        {
            if (selectedObject.name.Contains("Clothes") && clothesPreview.color != new Color(R, G, B, 1))
            {
                clothesPreview.color = new Color(R, G, B, 1);
                clothesIndex = clothesPreview.color;
            }
            else if (selectedObject.name.Contains("Hat") && hatPreview.color != new Color(R, G, B, 1))
            {
                hatPreview.color = new Color(R, G, B, 1);
                hatIndex = hatPreview.color;
            }
            else if (selectedObject.name.Contains("Hair") && hairPreview.color != new Color(R, G, B, 1))
            {
                hairPreview.color = new Color(R, G, B, 1);
                hairIndex = hairPreview.color;
            }
        }
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        balanceText.text = recolorAttempts.ToString();
    }

    public void Checkout()
    {
        CloseShop();
        UpdatePlayerVisuals();
    }

    public void UpdatePlayerVisuals()
    {
        if (clothesPreview.enabled)
        {
            player.ClothesComponent.enabled = true;
            player.ClothesComponent.color = clothesPreview.color;
        }
        if (hatPreview.enabled)
        {
            player.HatComponent.enabled = true;
            player.HatComponent.color = clothesPreview.color;
        }
        if (hairPreview.enabled)
        {
            player.HairComponent.enabled = true;
            player.HairComponent.color = clothesPreview.color;
        }
    }
}
