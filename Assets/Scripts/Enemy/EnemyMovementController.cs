using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private Enemy _enemy;
    private new Rigidbody2D rigidbody;
    public float speed;
    public bool ShouldMove;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
        ShouldMove = true;
    }

    private void OnEnable()
    {
        Events.OnEnemyFoundBlockerAheadEvent += HandleEnemyFoundBlockerAheadEvent;
        Events.OnEnemyFoundNoBlockerAheadEvent += HandleEnemyFoundNoBlockerAheadEvent;
    }

    private void OnDisable()
    {
        Events.OnEnemyFoundBlockerAheadEvent -= HandleEnemyFoundBlockerAheadEvent;
        Events.OnEnemyFoundNoBlockerAheadEvent-= HandleEnemyFoundNoBlockerAheadEvent;
    }

    void HandleEnemyFoundBlockerAheadEvent(GameObject currentEnemy, GameObject blockerGO)
    {
        if(currentEnemy != gameObject)
        {
            return;
        }
        ShouldMove = false;

        // Set direction to opposite direction
        _enemy.SetRandomMovementDirection();
    }

    void HandleEnemyFoundNoBlockerAheadEvent(GameObject currentEnemy)
    {
        if (currentEnemy != gameObject)
        {
            return;
        }
        ShouldMove = true;
    }

    private void FixedUpdate()
    {
        if(!ShouldMove)
        {
            return;
        }

        Vector2 position = rigidbody.position;
        Vector2 translation = _enemy.Direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }
}
