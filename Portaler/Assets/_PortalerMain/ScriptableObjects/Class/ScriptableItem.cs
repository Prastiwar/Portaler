using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable/Item", fileName = "New Item")]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] GameObject _itemContentLayout;
    [SerializeField] Image[] icons;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] Button[] buttons;

    GameObject _itemParent;
    List<Button> buttonList = new List<Button>();
    List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();
    List<Image> iconList = new List<Image>();

    // Check what array is highest, set it to for _Length
    int GetHighestLength()
    {
        int _textsLength = texts.Length;
        int _buttonsLength = buttons.Length;
        int _iconsLength = icons.Length;
        int _Length = Mathf.Max(_textsLength, Mathf.Max(_buttonsLength, _iconsLength));
        return _Length;
    }

    void ClearLists()
    {
        iconList.Clear();
        buttonList.Clear();
        textList.Clear();
    }

    public void SpawnItem(Transform _parent)
    {
        _itemParent = Instantiate(_itemContentLayout, _parent);
        Transform _Parent = _itemParent.transform;
        
        int _textsLength = texts.Length;
        int _buttonsLength = buttons.Length;
        int _iconsLength = icons.Length;
        ClearLists();
        for (int i = 0; i < GetHighestLength(); i++)
        {
            if (i < _iconsLength)
                iconList.Add(Instantiate(icons[i], _Parent));
            if (i < _textsLength)
                textList.Add(Instantiate(texts[i], _Parent));
            if (i < _buttonsLength)
                buttonList.Add(Instantiate(buttons[i], _Parent));
        }
    }

    public Image GetIcon(int index){ return iconList[index]; }
    public TextMeshProUGUI GetText(int index){ return textList[index]; }
    public Button GetButton(int index){ return buttonList[index]; }

    public void DeactivAllItem()
    {
        _itemParent.SetActive(false);
    }

    public void SetIcon(int index, Sprite _sprite)
    {
        iconList[index].sprite = _sprite;
    }

    public void SetText(int index, string _text)
    {
        textList[index].text = _text;
    }

    public void AddListenerOnButton(int index, UnityAction method)
    {
        buttonList[index].onClick.AddListener(method);
    }
    public void AddListenerOnButton(int index, UnityAction method, bool removeAll)
    {
        if (removeAll)
            buttonList[index].onClick.RemoveAllListeners();
        AddListenerOnButton(index, method);
    }
}