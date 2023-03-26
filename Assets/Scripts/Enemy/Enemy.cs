using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public Vector2 Direction;

    private void OnEnable()
    {
        Events.OnGameStartedEvent += Activate;
        Events.OnEnemyDeathEvent += DeathSequence;
    }

    private void OnDisable()
    {
        Events.OnGameStartedEvent -= Activate;
        Events.OnEnemyDeathEvent -= DeathSequence;
    }

    public void Activate()
    {
        SetRandomMovementDirection();
    }

    public void SetRandomMovementDirection()
    {
        // set CurrentMovementDir a random value
        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 0:
                Direction = Vector2.left;
                break;
            case 1:
                Direction = Vector2.right;
                break;
            case 2:
                Direction = Vector2.up;
                break;
            case 3:
                Direction = Vector2.down;
                break;
        }
    }

    private void DeathSequence(GameObject enemyGO)
    {
        if(enemyGO != gameObject)
        {
            return;
        }
        enabled = false;
        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        Events.OnEnemyDeathSequenceCompleteEvent?.Invoke();
    }
}
