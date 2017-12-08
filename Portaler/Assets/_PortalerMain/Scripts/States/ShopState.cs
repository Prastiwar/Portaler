using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopState : MonoBehaviour
{
    [SerializeField] ScriptableWeapon[] weapons;
    [SerializeField] Transform _ShopContent;
    [SerializeField] GameObject _GunItemPrefab;
    List<GunItem> _GunItemList = new List<GunItem>();

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        SpawnShopItems();
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public void OnChangeSceneButton(string _SceneName)
    {
        StateMachineManager.ChangeSceneTo(_SceneName);
    }

    public void SpawnShopItems()
    {
        int _Length = weapons.Length;
        for (int i = 0; i < _Length; i++)
        {
            GameObject _GunItem = Instantiate(_GunItemPrefab, _ShopContent);
            GunItem _Item = _GunItem.GetComponent<GunItem>();
            _GunItemList.Add(_Item);
            UpdateAllText(i);
        }
    }

    void UpdateAllText(int i)
    {
        _GunItemList[i].icon.sprite = weapons[i].sprite;
        _GunItemList[i].ammoText.text = weapons[i].maxAmmo.ToString();
        _GunItemList[i].distanceText.text = weapons[i].distance.ToString();
        UpdateButton(i);
    }

    void UpdateButton(int i)
    {
        string buyText = "Buy";
        string wearText = "Wear";
        string clothedText = "Clothed";

        _GunItemList[i].priceText.text = weapons[i].isPurchased ? string.Empty : weapons[i].price.ToString();
        _GunItemList[i].itemButton.onClick.RemoveAllListeners();
        _GunItemList[i].itemButton.onClick.AddListener(() => OnItemButtonClick(i));
        _GunItemList[i].buttonText.text = GameState.weaponIndex == i ? clothedText : (weapons[i].isPurchased ? wearText : buyText);
    }

    void OnItemButtonClick(int i)
    {
        if (weapons[i].isPurchased)
            OnWear(i);
        else
            OnBuy(i);
    }

    void OnBuy(int i)
    {
        if (GameState._player.money >= weapons[i].price)
        {
            GameState._player.money -= weapons[i].price;
            weapons[i].isPurchased = true;
            OnWear(i);
        }
        else
        {
            Debug.Log("You don't have money");
        }
    }

    void OnWear(int i)
    {
        GameState.weaponIndex = i;

        int _Length = _GunItemList.Count;
        for (int j = 0; j < _Length; j++)
        {
            UpdateButton(j);
        }
    }
}