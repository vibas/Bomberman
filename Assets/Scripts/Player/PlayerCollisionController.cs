using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Events.OnPlayerDeathEvent?.Invoke();
        }
    }
}
