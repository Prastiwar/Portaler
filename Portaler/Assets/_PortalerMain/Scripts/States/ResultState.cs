using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultState : MonoBehaviour
{
    [SerializeField] GameObject _WinPanel;
    [SerializeField] GameObject _LosePanel;
    [SerializeField] Button _ToLevelButton;

    [SerializeField] TextMeshProUGUI _ToLevelButtonText;
    [SerializeField] TextMeshProUGUI _HeaderText;
    [SerializeField] TextMeshProUGUI _MoneyBalanceText;

    [SerializeField] Image _StarScoreImage;
    [SerializeField] TextMeshProUGUI _StarScoreText;

    [SerializeField] ScriptableData data;
    [SerializeField] ScriptableTextData textData;
    
    [SerializeField] Transform _ItemParent;
    [SerializeField] ScriptableItem _Item;
    [SerializeField] Transform _PopupParent;
    [SerializeField] ScriptableItem _Popup;
    StateMachineManager _stateManager;
    int itemIndex = GameState.itemIndex;
    int weaponIndex = GameState.weaponIndex;
    int lvlIndex = GameState.lvlIndex;
    bool hasLose = GameState.isSpotted;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _stateManager = StateMachineManager.Instance;
        SetResultScreen();
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

    void SetResultScreen()
    {
        if (hasLose)
            _LosePanel.SetActive(true);
        else
            _WinPanel.SetActive(true);

        SetTexts();

        SetLevelDataInfo();

        // Setting item popup
        if (itemIndex > -1 && !hasLose)
        {
            CreateItem();
        }
        else if(itemIndex > -1 && hasLose)
        {
            SetPopup(false);
        }
    }
    
    void CreateItem()
    {
        var stealItem = data.StealItems[itemIndex];

        _Item.SpawnItem(_ItemParent);
        _Item.SetIcon(0, stealItem.image);
        _Item.SetText(0, stealItem.nameItem);
        _Item.SetText(1, stealItem.moneyValue.ToString());
        _Item.SetText(2, stealItem.description);
        _Item.AddListenerOnButton(0, () => OnSell());
        _Item.AddListenerOnButton(1, () => OnCancelButton());
    }

    void OnCancelButton()
    {
        _Popup.DeactivAllItem();
        _Item.DeactivAllItem();
    }

    void OnSell()
    {
        bool isDone = true;
        int randBool = Random.Range(0,100);
        isDone = (randBool > 0 && randBool < 50) ? true : false;

        SetPopup(isDone);
    }

    void SetPopup(bool isDone)
    {
        string decision;
        int doneText = Random.Range(0, textData.sellDoneText.Length);
        int failureText = Random.Range(0, textData.sellFailureText.Length);
        decision = isDone ? textData.sellDoneText[doneText] : textData.sellFailureText[failureText];

        _Popup.SpawnItem(_PopupParent);
        _Popup.SetText(0, decision);
        _Popup.AddListenerOnButton(0, ()=> OnCancelButton());

        if (isDone)
            GameState.Player.money += data.StealItems[itemIndex].moneyValue;
        else
            GameState.Player.money -= Mathf.FloorToInt(data.StealItems[itemIndex].moneyValue * Random.Range(0.5f, 1f));

        _MoneyBalanceText.text = GameState.Player.money.ToString();
    }

    void SetLevelDataInfo()
    {
        int _Length = data.Levels.Length;
        data.Levels[lvlIndex].starScoreAmount = CalculateScore();
        if ((lvlIndex + 1) < _Length) // Check if next level exist
            data.Levels[lvlIndex + 1].isUnlocked = true;
    }

    void SetTexts()
    {
        string retryText = "Try Again";
        string nextLevelText = "Next Level";
        int randWin = Random.Range(0, textData.WinText.Length);
        int randLose = Random.Range(0, textData.LoseText.Length);
        string winText = textData.WinText[randWin];
        string loseText = textData.LoseText[randLose];

        _ToLevelButtonText.text = hasLose ? retryText : nextLevelText;
        _HeaderText.text = hasLose ? loseText : winText;
        _MoneyBalanceText.text = GameState.Player.money.ToString();
        _StarScoreImage.fillAmount = CalculateScore();
        _StarScoreText.text = (CalculateScore() * 100).ToString();
    }

    float CalculateScore()
    {
        if (hasLose)
            return 0;

        float result = (float)data.Weapons[weaponIndex].ammo / (float)(data.Weapons[weaponIndex].maxAmmo / 2);
        if (result > 1f)
            result = 1f;

        return result;
    }

    public void OnLevelButton()
    {
        if (!hasLose)
            GameState.lvlIndex += 1;

        _stateManager.ChangeSceneTo("Game");
    }

}