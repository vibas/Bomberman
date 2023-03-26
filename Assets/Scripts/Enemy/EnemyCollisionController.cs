using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Events.OnEnemyDeathEvent?.Invoke(this.gameObject);
        }
    }
}