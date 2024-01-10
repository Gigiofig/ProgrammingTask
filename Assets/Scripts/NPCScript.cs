using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPCScript : MonoBehaviour
{
    int balance = 100;
    int hairBalance = 0;
    int hatBalance = 0;
    int clothesBalance = 0;
    [Header("Shop Events")]
    [SerializeField] Button checkoutButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] GameObject shopUI;
    [SerializeField] TMPro.TMP_Text balanceText;
    [SerializeField] Image hairPreview;
    [SerializeField] Image hatPreview;
    [SerializeField] Image clothesPreview;
    [SerializeField] PlayerScript player;
    [SerializeField] private SceneInfo sceneInfo;
    public SpriteRenderer inAreaNotification;
    private int hairIndex = 0;
    private int hatIndex = 0;
    private int clothesIndex = 0;

    private void Start()
    {
        UpdateBalance();
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void OnShopButtonClick()
    {
        GameObject ClickedButton = EventSystem.current.currentSelectedGameObject;
        if (ClickedButton.name.Contains("Hair"))
        {
            if (!hairPreview.enabled)
            {
                hairBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                balance -= hairBalance;
                hairPreview.enabled = true;
            }
            else if (hairPreview.sprite == ClickedButton.transform.parent.GetChild(1).GetComponent<Image>().sprite)
            {
                hairBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                hairPreview.enabled = false;
                balance += hairBalance;
            }
            else
            {
                balance += hairBalance;
                hairBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                balance -= hairBalance;
            }
            hairPreview.sprite = ClickedButton.transform.parent.GetChild(1).GetComponent<Image>().sprite;
            string buttonName = ClickedButton.transform.parent.name;
            int pos = buttonName.LastIndexOf('r');
            hairIndex = int.Parse(buttonName.Remove(0, pos + 1)) - 1;
        }
        else if (ClickedButton.name.Contains("Hat"))
        {
            if (!hatPreview.enabled)
            {
                hatBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                hatPreview.enabled = true;
                balance -= hatBalance;
            }
            else if (hatPreview.sprite == ClickedButton.transform.parent.GetChild(1).GetComponent<Image>().sprite)
            {
                hatBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                hatPreview.enabled = false;
                balance += hatBalance;
            }
            else
            {
                balance += hatBalance;
                hatBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                balance -= hatBalance;
            }
            hatPreview.sprite = ClickedButton.transform.parent.GetChild(1).GetComponent<Image>().sprite;
            string buttonName = ClickedButton.transform.parent.name;
            int pos = buttonName.LastIndexOf('t');
            hatIndex = int.Parse(buttonName.Remove(0, pos + 1)) - 1;
        }
        else if (ClickedButton.name.Contains("Clothes"))
        {
            if (!clothesPreview.enabled)
            {
                clothesBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                clothesPreview.enabled = true;
                balance -= clothesBalance;
            }
            else if (clothesPreview.sprite == ClickedButton.transform.parent.GetChild(1).GetComponent<Image>().sprite)
            {
                clothesPreview.enabled = false;
                clothesBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                balance += clothesBalance;
            }
            else
            {
                balance += clothesBalance;
                clothesBalance = int.Parse(ClickedButton.transform.parent.GetChild(3).GetComponent<TMPro.TMP_Text>().text);
                balance -= clothesBalance;
            }
            clothesPreview.sprite = ClickedButton.transform.parent.GetChild(1).GetComponent<Image>().sprite;
            string buttonName = ClickedButton.transform.parent.name;
            int pos = buttonName.LastIndexOf('e');
            clothesIndex = int.Parse(buttonName.Remove(0, pos + 1)) - 1;
        }
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        balanceText.text = "$" + balance.ToString();
    }

    public void Checkout()
    {
        if (balance >= 0)
        {
            CloseShop();
            UpdatePlayerVisuals();
        }
        else
        {
            Debug.Log("not enough money");
        }
    }

    public void UpdatePlayerVisuals()
    {
        if (clothesPreview.enabled)
        {
            sceneInfo.hasClothes = true;
            sceneInfo.clothesIndex = clothesIndex;
            player.ClothesComponent.enabled = true;
            player.customClothes.skinNum = clothesIndex;
            player.customClothes.SkinChoice();
        }
        else
        {
            sceneInfo.hasClothes = false;
            sceneInfo.clothesIndex = 0;
            player.ClothesComponent.enabled = false;
            player.customClothes.skinNum = 0;
        }
        if (hatPreview.enabled)
        {
            sceneInfo.hasHat = true;
            sceneInfo.hatIndex = hatIndex;
            player.HatComponent.enabled = true;
            player.customHat.skinNum = hatIndex;
            player.customHat.SkinChoice();
        }
        else
        {
            sceneInfo.hasHat = false;
            sceneInfo.hatIndex = 0;
            player.HatComponent.enabled = false;
            player.customHat.skinNum = 0;
        }
        if (hairPreview.enabled)
        {
            sceneInfo.hasHair = true;
            sceneInfo.hairIndex = hairIndex;
            player.HairComponent.enabled = true;
            player.customHair.skinNum = hairIndex;
            player.customHair.SkinChoice();
        }
        else
        {
            sceneInfo.hasHair = false;
            sceneInfo.hairIndex = 0;
            player.HairComponent.enabled = false;
            player.customHair.skinNum = 0;
        }
    }
}
