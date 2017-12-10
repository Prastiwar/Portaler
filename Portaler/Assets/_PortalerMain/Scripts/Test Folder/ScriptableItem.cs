using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable/Item", fileName = "New Item")]
public class ScriptableItem : ScriptableObject
{
    public Image[] icons;
    public TextMeshProUGUI[] texts;
    public Button[] buttons;

    public void SetIcon(int index, Sprite _sprite)
    {
        icons[index].sprite = _sprite;
    }

    public void SetText(int index, string _text)
    {
        texts[index].text = _text;
    }

    public void AddListenerOnButton(int index, UnityAction method, bool removeAll)
    {
        if(removeAll)
            buttons[index].onClick.RemoveAllListeners();
        buttons[index].onClick.AddListener(() => method());
    }

    public void SpawnItem(Transform _Parent)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            Instantiate(icons[i], _Parent);
        }
        for (int i = 0; i < texts.Length; i++)
        {
            Instantiate(texts[i], _Parent);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            Instantiate(buttons[i], _Parent);
        }
    }
}