using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ResultScreen _resultScreen;

    private void OnEnable()
    {
        Events.OnGameOverEvent += ShowResultScreen;
    }

    private void OnDisable()
    {
        Events.OnGameOverEvent -= ShowResultScreen;
    }

    private void ShowResultScreen(bool didPlayerWin)
    {
        _resultScreen.gameObject.SetActive(true);
        string resultStr = didPlayerWin ? "WIN" : "LOST";
        _resultScreen.SetResult(resultStr);
    }
}
