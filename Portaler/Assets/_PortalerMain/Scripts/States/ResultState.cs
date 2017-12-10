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


    bool hasLose = GameState.isSpotted;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
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
        StateMachineManager.ChangeSceneTo(_SceneName);
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
        if (GameState.itemIndex > -1 && !hasLose)
        {
            CreateItem();
        }
        else if(GameState.itemIndex > -1 && hasLose)
        {
            //always get fee => SetPopup(false, null);
        }
    }
    
    void CreateItem()
    {
        int i = GameState.itemIndex;
        var stealItem = data.StealItems[i];

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
        _Popup.DeactiveItem();
        _Item.DeactiveItem();
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
            GameState.Player.money += data.StealItems[GameState.itemIndex].moneyValue;
        else
            GameState.Player.money -= Mathf.FloorToInt(data.StealItems[GameState.itemIndex].moneyValue * Random.Range(0.5f, 1f));

        _MoneyBalanceText.text = GameState.Player.money.ToString();
    }

    void SetLevelDataInfo()
    {
        int i = GameState.lvlIndex;
        int _Length = data.Levels.Length;
        data.Levels[i].starScoreAmount = CalculateScore();
        if ((i + 1) < _Length) // Check if next level exist
            data.Levels[i + 1].isUnlocked = true;
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
        // im mniej wystrzelisz - tym lepszy wynik, 100% jest niemożliwe.. 

        int i = GameState.weaponIndex;
        float result = data.Weapons[i].ammo / data.Weapons[i].maxAmmo;
        return result;
    }

    public void OnLevelButton()
    {
        if (!hasLose)
            GameState.lvlIndex += 1;

        StateMachineManager.ChangeSceneTo("Game");
    }

}