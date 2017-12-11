using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopState : MonoBehaviour
{
    [SerializeField] ScriptableData data;
    [SerializeField] Transform _ShopContent;
    [SerializeField] ScriptableItem _ShopItem;
    StateMachineManager _stateManager;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _stateManager = StateMachineManager.Instance;
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
        _stateManager.ChangeSceneTo(_SceneName);
    }

    void SpawnShopItems()
    {
        int _Length = _stateManager.data.Weapons.Length;
        for (int i = 0; i < _Length; i++)
        {
            _ShopItem.SpawnItem(_ShopContent);
            _ShopItem.SetIcon(0, _stateManager.data.Weapons[i].sprite);
            _ShopItem.SetText(0, _stateManager.data.Weapons[i].maxAmmo.ToString());
            _ShopItem.SetText(1, _stateManager.data.Weapons[i].distance.ToString());
            UpdateButton(i);
        }
    }

    void UpdateButton(int i)
    {
        string buyText = "Buy";
        string wearText = "Wear";
        string clothedText = "Clothed";

        _ShopItem.SetText(2, _stateManager.data.Weapons[i].isPurchased ? string.Empty : _stateManager.data.Weapons[i].price.ToString());
        _ShopItem.AddListenerOnButton(0, () => OnItemButtonClick(i), true);
        _ShopItem.GetButton(0).GetComponentInChildren<TextMeshProUGUI>().text =
            GameState.weaponIndex == i ? clothedText : (_stateManager.data.Weapons[i].isPurchased ? wearText : buyText);
    }

    void OnItemButtonClick(int i)
    {
        if (_stateManager.data.Weapons[i].isPurchased) // On Wearing
        {
            GameState.weaponIndex = i;

            int _Length = _stateManager.data.Weapons.Length;
            for (int j = 0; j < _Length; j++)
            {
                UpdateButton(j);
            }
        }
        else
        {
            if (GameState.Player.money >= _stateManager.data.Weapons[i].price)
            {
                GameState.Player.money -= _stateManager.data.Weapons[i].price;
                _stateManager.data.Weapons[i].isPurchased = true;
            }
            else
            {
                Debug.Log("You don't have money");
            }
        }
    }
}