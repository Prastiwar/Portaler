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

    [SerializeField] GameObject _StealItemPopupPrefab;
    [SerializeField] Transform _ItemParent;
    [SerializeField] GameObject _DecisionPopup;
    [SerializeField] TextMeshProUGUI _popupDescription;
    [SerializeField] Button _popupOKButton;

    // Test region
    [SerializeField] ScriptableItem _ItemTest;

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
            //GameObject _StealItem = Instantiate(_StealItemPopupPrefab, _ItemParent);
            //StealItem _Item = _StealItem.GetComponent<StealItem>();
            //UpdateItemText(_Item, _StealItem);

            //Test region
            CreateItemTest();
        }
        else if(GameState.itemIndex > -1 && hasLose)
        {
            //always get fee => SetPopup(false, null);
        }
    }

    // Test void
    void CreateItemTest()
    {
        int i = GameState.itemIndex;
        var item = _ItemTest;
        item.SpawnItem(_ItemParent);
        item.SetIcon(0, data.StealItems[i].image);
        item.SetText(0, data.StealItems[i].nameItem);
        item.SetText(0, data.StealItems[i].moneyValue.ToString());
        item.SetText(0, data.StealItems[i].description);
        item.AddListenerOnButton(0, ()=> OnSell(_ItemParent.gameObject), false);
        item.AddListenerOnButton(1, ()=> OnCancelButton(_ItemParent.gameObject), false);
    }

    void UpdateItemText(StealItem _Item, GameObject _gameObject)
    {
        int i = GameState.itemIndex;
        _Item.icon.sprite = data.StealItems[i].image;
        _Item.nameText.text = data.StealItems[i].nameItem;
        _Item.moneyValueText.text = data.StealItems[i].moneyValue.ToString();
        _Item.descriptionText.text = data.StealItems[i].description;
        _Item.sellButton.onClick.AddListener(()=> OnSell(_gameObject));
        _Item.leaveButton.onClick.AddListener(()=> OnCancelButton(_gameObject));
    }

    void OnSell(GameObject _gameObject)
    {
        bool isDone = true;
        int randBool = Random.Range(0,100);
        isDone = (randBool > 0 && randBool < 50) ? true : false;

        SetPopup(isDone, _gameObject);
    }

    void SetPopup(bool isDone, GameObject _gameObject)
    {
        _DecisionPopup.SetActive(true);
        string decision;
        int doneText = Random.Range(0, textData.sellDoneText.Length);
        int failureText = Random.Range(0, textData.sellFailureText.Length);

        decision = isDone ? textData.sellDoneText[doneText] : textData.sellFailureText[failureText];
        _popupDescription.text = decision;
        _popupOKButton.onClick.AddListener(()=> OnCancelButton(_gameObject));

        if (isDone)
            GameState.Player.money += data.StealItems[GameState.itemIndex].moneyValue;
        else
            GameState.Player.money -= Mathf.FloorToInt(data.StealItems[GameState.itemIndex].moneyValue * Random.Range(0.5f, 1f));

        _MoneyBalanceText.text = GameState.Player.money.ToString();
    }

    void OnCancelButton(GameObject _gameObject)
    {
        _DecisionPopup.SetActive(false);
        _gameObject.SetActive(false);
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