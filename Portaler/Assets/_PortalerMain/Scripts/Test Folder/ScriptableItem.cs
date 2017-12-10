using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable/Item", fileName = "New Item")]
public class ScriptableItem : ScriptableObject
{
    public GameObject _itemContent;
    public Image[] icons;
    public TextMeshProUGUI[] texts;
    public Button[] buttons;

    List<Button> buttonList = new List<Button>();

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
            buttonList[index].onClick.RemoveAllListeners();
        buttonList[index].onClick.AddListener(() => method());
    }

    public void SpawnItem(Transform _parent)
    {
        GameObject itemContent = Instantiate(_itemContent, _parent);
        Transform _Parent = itemContent.transform;

        // Check what array is highest, set it to for _Length
        int _textsLength = texts.Length;
        int _buttonsLength = buttons.Length;
        int _iconsLength = icons.Length;
        int _Length = Mathf.Max(_textsLength, Mathf.Max(_buttonsLength, _iconsLength));
        
        for (int i = 0; i < _Length; i++)
        {
            if(i < _iconsLength)
                Instantiate(icons[i], _Parent);
            if (i < _textsLength)
                Instantiate(texts[i], _Parent);
            if (i < _buttonsLength)
                buttonList.Add(Instantiate(buttons[i], _Parent));
        }
    }
}