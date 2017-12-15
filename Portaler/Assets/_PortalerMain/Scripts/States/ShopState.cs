using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopState : MonoBehaviour
{
    [SerializeField] Transform _ShopContent;
    [SerializeField] AudioClip[] audioClips;
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
            var item = _stateManager.data.Weapons[i];
            item.InitializeItem();
            item.SpawnItem(_ShopContent);
            item.SetIcon(0, _stateManager.data.Weapons[i].sprite);
            item.SetText(0, _stateManager.data.Weapons[i].maxAmmo.ToString());
            item.SetText(1, _stateManager.data.Weapons[i].distance.ToString());
        }
        UpdateButton();
    }

    void UpdateButton()
    {
        string buyText = "Buy";
        string wearText = "Wear";
        string clothedText = "Clothed";

        int _Length = _stateManager.data.Weapons.Length;
        for (int i = 0; i < _Length; i++)
        {
            int x = i;
            var item = _stateManager.data.Weapons[i];
            item.GetText(2).gameObject.SetActive(_stateManager.data.Weapons[i].isPurchased ? false : true);
            item.SetText(2, _stateManager.data.Weapons[i].price.ToString());
            item.AddListenerOnButton(0, () => OnItemButtonClick(x), true);
            item.GetButton(0).GetComponentInChildren<TextMeshProUGUI>().text =
                GameState.player.weaponIndex == i ? clothedText : (_stateManager.data.Weapons[i].isPurchased ? wearText : buyText);
        }
    }

    void OnItemButtonClick(int i)
    {
        if (_stateManager.data.Weapons[i].isPurchased) // On Wearing
        {
            SoundManager.Instance.PlaySound(audioClips[0], 1); // wear sound
            GameState.player.weaponIndex = i;

            UpdateButton();
        }
        else
        {
            if (GameState.player.money >= _stateManager.data.Weapons[i].price)
            {
                SoundManager.Instance.PlaySound(audioClips[1], 1); // buy sound
                GameState.player.money -= _stateManager.data.Weapons[i].price;
                _stateManager.data.Weapons[i].isPurchased = true;
            }
            else
            {
                SoundManager.Instance.PlaySound(audioClips[2], 1); // reject sound
                Debug.Log("You don't have money");
            }
        }
    }
}