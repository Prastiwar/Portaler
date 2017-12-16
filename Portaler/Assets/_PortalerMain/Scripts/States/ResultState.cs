using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultState : MonoBehaviour
{
    [SerializeField] ParticleSystem _Particles;
    [SerializeField] Color _LoseColor;
    [SerializeField] Color _WinColor;

    [SerializeField] Button _ToLevelButton;

    [SerializeField] TextMeshProUGUI _ToLevelButtonText;
    [SerializeField] TextMeshProUGUI _HeaderText;
    [SerializeField] TextMeshProUGUI _MoneyBalanceText;

    [SerializeField] Image _StarScoreImage;
    [SerializeField] TextMeshProUGUI _StarScoreText;

    [SerializeField] ScriptableData data;
    [SerializeField] ScriptableTextData textData;
    
    [SerializeField] Transform _ItemParent;

    [SerializeField] Transform _PopupParent;
    [SerializeField] ScriptableItem _Popup;

    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioClip[] audioMusic;

    StateMachineManager _stateManager;
    int itemIndex = GameState.player.itemIndex;
    int weaponIndex = GameState.player.weaponIndex;
    int lvlIndex = GameState.player.lvlIndex;
    bool hasLose = GameState.player.isSpotted;

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
        var main = _Particles.main;
        var mainChild = _Particles.GetComponentInChildren<ParticleSystem>().main;
        if (hasLose)
        {
            main.startColor = _LoseColor;
            mainChild.startColor = _LoseColor;
            SoundManager.Instance.PlayMusic(audioMusic[0], 0.1f);
        }
        else
        {
            main.startColor = _WinColor;
            mainChild.startColor = _WinColor;
            SoundManager.Instance.PlayMusic(audioMusic[1], 0.1f);
        }

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
        stealItem.InitializeItem();
        stealItem.SpawnItem(_ItemParent);
        stealItem.SetIcon(0, stealItem.image);
        stealItem.SetText(0, stealItem.nameItem);
        stealItem.SetText(1, stealItem.moneyValue.ToString());
        stealItem.SetText(2, stealItem.description);
        stealItem.AddListenerOnButton(0, () => OnSell());
        stealItem.AddListenerOnButton(1, () => OnCancelButton());
    }

    void OnCancelButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.audioClips[0], 1);
        _Popup.DeactivAllItem();
        data.StealItems[itemIndex].DeactivAllItem();
    }

    void OnSell()
    {
        bool isDone = true;
        int randBool = Random.Range(0,100);
        isDone = (randBool > 0 && randBool < 50) ? true : false;

        SoundManager.Instance.PlaySound(isDone ? audioClips[0] : audioClips[1], 1); // done or fail sound

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
            GameState.player.money += data.StealItems[itemIndex].moneyValue;
        else
            GameState.player.money -= Mathf.FloorToInt(data.StealItems[itemIndex].moneyValue * Random.Range(0.5f, 1f));

        _MoneyBalanceText.text = GameState.player.money.ToString();
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
        _MoneyBalanceText.text = GameState.player.money.ToString();
        _StarScoreImage.fillAmount = CalculateScore();

        _StarScoreText.text = CalculateScore() < 0 ? "0" : (CalculateScore() * 100).ToString();
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
        SoundManager.Instance.PlaySound(SoundManager.Instance.audioClips[0], 1);
        if (!hasLose)
            GameState.player.lvlIndex += 1;

        _stateManager.ChangeSceneTo("Game");
    }

}