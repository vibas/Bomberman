using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public Vector2 Direction;

    private void OnEnable()
    {
        Events.OnEnemyDeathEvent += DeathSequence;
    }

    private void OnDisable()
    {
        Events.OnEnemyDeathEvent -= DeathSequence;
    }

    private void Start()
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
        Direction = Vector2.zero;
        Invoke(nameof(OnDeathSequenceEnded), 0.5f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        Events.OnEnemyDeathSequenceCompleteEvent?.Invoke();
    }
}
