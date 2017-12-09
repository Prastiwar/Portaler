using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopState : MonoBehaviour
{
    //[SerializeField] ScriptableData data;
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
        int _Length = StateMachineManager.data.Weapons.Length;
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
        _GunItemList[i].icon.sprite = StateMachineManager.data.Weapons[i].sprite;
        _GunItemList[i].ammoText.text = StateMachineManager.data.Weapons[i].maxAmmo.ToString();
        _GunItemList[i].distanceText.text = StateMachineManager.data.Weapons[i].distance.ToString();
        UpdateButton(i);
    }

    void UpdateButton(int i)
    {
        string buyText = "Buy";
        string wearText = "Wear";
        string clothedText = "Clothed";

        _GunItemList[i].priceText.text = StateMachineManager.data.Weapons[i].isPurchased ? string.Empty : StateMachineManager.data.Weapons[i].price.ToString();
        _GunItemList[i].itemButton.onClick.RemoveAllListeners();
        _GunItemList[i].itemButton.onClick.AddListener(() => OnItemButtonClick(i));
        _GunItemList[i].buttonText.text = GameState.weaponIndex == i ? clothedText : (StateMachineManager.data.Weapons[i].isPurchased ? wearText : buyText);
    }

    void OnItemButtonClick(int i)
    {
        if (StateMachineManager.data.Weapons[i].isPurchased)
            OnWear(i);
        else
            OnBuy(i);
    }

    void OnBuy(int i)
    {
        if (GameState.Player.money >= StateMachineManager.data.Weapons[i].price)
        {
            GameState.Player.money -= StateMachineManager.data.Weapons[i].price;
            StateMachineManager.data.Weapons[i].isPurchased = true;
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