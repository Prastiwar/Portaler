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

        string retryText = "Try Again";
        string nextLevelText = "Next Level";
        int randI = Random.Range(0, textData.WinText.Length);
        int randJ = Random.Range(0, textData.LoseText.Length);
        string loseText = textData.LoseText[randI];
        string winText = textData.WinText[randJ];
        
        _ToLevelButtonText.text = hasLose ? retryText : nextLevelText;
        _HeaderText.text = hasLose ? loseText : winText;
        _MoneyBalanceText.text = GameState._player.money.ToString();
        _StarScoreImage.fillAmount = CalculateScore();
        _StarScoreText.text = (CalculateScore() * 100).ToString();

        int i = GameState.lvlIndex;
        int _Length = data.Levels.Length;
        data.Levels[i].starScoreAmount = CalculateScore();
        if((i + 1) < _Length) // Check if next level exist
            data.Levels[i + 1].isUnlocked = true;
    }

    float CalculateScore()
    {
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