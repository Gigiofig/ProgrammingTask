using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    [Header("Shop Events")]
    [SerializeField] Button CheckoutButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] GameObject shopUI;
    [SerializeField] Image HairPreview;
    [SerializeField] Image HatPreview;
    [SerializeField] Image ClothesPreview;

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

    public void OnButtonClick()
    {
        if (Button.)
    }
}
