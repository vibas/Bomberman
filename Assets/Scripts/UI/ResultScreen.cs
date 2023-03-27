using UnityEngine;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetResult(string result)
    {
        _scoreText.text = result;
    }

    public void ReplayButtonAction()
    {
        Events.OnGameResetRequestedEvent?.Invoke();
        gameObject.SetActive(false);
    }
}