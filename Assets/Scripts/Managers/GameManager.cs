using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    private void OnEnable()
    {
        Events.OnPlayerDeathEvent += CheckWinState;
        Events.OnAllEnemiesClearedEvent += CheckWinState;
    }

    private void OnDisable()
    {
        Events.OnPlayerDeathEvent -= CheckWinState;
        Events.OnAllEnemiesClearedEvent -= CheckWinState;
    }

    public void Start()
    {
        NewRound();
        Events.OnGameStartedEvent?.Invoke();
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

        if (aliveCount <= 1) 
        {
            Invoke(nameof(ResetRound), 3f);
        }
        else
        {
            // Check if all enemies are dead
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                Debug.LogError("ALL ENEMIES CLEARED");
                Invoke(nameof(ResetRound), 3f);
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
