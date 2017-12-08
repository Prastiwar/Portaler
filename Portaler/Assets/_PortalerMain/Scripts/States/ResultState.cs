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
    bool hasLose;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        hasLose = GameState.isSpotted;
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
        {
            _LosePanel.SetActive(true);
        }
        else
        {
            _WinPanel.SetActive(true);
        }

        string retryText = "Try Again";
        string nextLevelText = "Next Level";
        string loseText = "You're too low to do this!";
        string winText = "You dit it! Wow!";

        _ToLevelButtonText.text = hasLose ? retryText : nextLevelText;
        _HeaderText.text = hasLose ? loseText : winText;
        _MoneyBalanceText.text = GameState._player.money.ToString();
        _StarScoreImage.fillAmount = GameState._player.score;
    }

    public void OnLevelButton()
    {
        if (!hasLose)
            GameState.lvlIndex += 1;

        StateMachineManager.ChangeSceneTo("Game");
    }

}