using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    private void OnEnable()
    {
        Events.OnPlayerDeathSequenceCompleteEvent += CheckWinState;
        Events.OnAllEnemiesClearedEvent += CheckWinState;
        Events.OnGameResetRequestedEvent += ResetRound;
    }

    private void OnDisable()
    {
        Events.OnPlayerDeathSequenceCompleteEvent -= CheckWinState;
        Events.OnAllEnemiesClearedEvent -= CheckWinState;
        Events.OnGameResetRequestedEvent -= ResetRound;
    }

    private void Start()
    {
        NewRound();
    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf) {
                aliveCount++;
            }
        }

        if (aliveCount < 1) 
        {
            Events.OnGameOverEvent?.Invoke(false);
        }
        else
        {
            // Check if all enemies are dead
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                Events.OnGameOverEvent?.Invoke(true);
            }
        }
    }

    private void NewRound()
    {   
        Events.OnNewRoundRequestedEvent?.Invoke();
    }

    private void ResetRound()
    {
        SceneManager.LoadScene("Bomberman");
    }
}
