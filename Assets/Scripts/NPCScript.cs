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
    public SpriteRenderer inAreaNotification;

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
    void AddShopEvents()
    {
        closeShopButton.onClick.RemoveAllListeners();

        closeShopButton.onClick.AddListener(CloseShop);
    }

    public void OnShopButtonClick()
    {
        GameObject ClickedButton = EventSystem.current.currentSelectedGameObject;
        if (ClickedButton.name.Contains("Hair"))
        {
            if (!hairPreview.enabled)
            {
                hairBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                balance -= hairBalance;
                hairPreview.enabled = true;
            }
            else if (hairPreview.sprite == ClickedButton.transform.parent.GetChild(0).GetComponent<Image>().sprite)
            {
                hairBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                hairPreview.enabled = false;
                balance += hairBalance;
            }
            else
            {
                balance += hairBalance;
                hairBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                balance -= hairBalance;
            }
            hairPreview.sprite = ClickedButton.transform.parent.GetChild(0).GetComponent<Image>().sprite;
        }
        else if (ClickedButton.name.Contains("Hat"))
        {
            if (!hatPreview.enabled)
            {
                hatBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                hatPreview.enabled = true;
                balance -= hatBalance;
            }
            else if (hatPreview.sprite == ClickedButton.transform.parent.GetChild(0).GetComponent<Image>().sprite)
            {
                hatBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                hatPreview.enabled = false;
                balance += hatBalance;
            }
            else
            {
                balance += hatBalance;
                hatBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                balance -= hatBalance;
            }
            hatPreview.sprite = ClickedButton.transform.parent.GetChild(0).GetComponent<Image>().sprite;
        }
        else if (ClickedButton.name.Contains("Clothes"))
        {
            if (!clothesPreview.enabled)
            {
                clothesBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                clothesPreview.enabled = true;
                balance -= clothesBalance;
            }
            else if (clothesPreview.sprite == ClickedButton.transform.parent.GetChild(0).GetComponent<Image>().sprite)
            {
                clothesPreview.enabled = false;
                clothesBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                balance += clothesBalance;
            }
            else
            {
                balance += clothesBalance;
                clothesBalance = int.Parse(ClickedButton.transform.parent.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
                balance -= clothesBalance;
            }
            clothesPreview.sprite = ClickedButton.transform.parent.GetChild(0).GetComponent<Image>().sprite;
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
        if (clothesPreview.sprite != null)
        {
            player.ClothesComponent.enabled = true;
            player.ClothesComponent.sprite = clothesPreview.sprite;
        }
        if (hatPreview.sprite != null)
        {
            player.HatComponent.enabled = true;
            player.HatComponent.sprite = hatPreview.sprite;
        }
        if (hairPreview.sprite != null)
        {
            player.HairComponent.enabled = true;
            player.HairComponent.sprite = hairPreview.sprite;
        }
    }
}
