using UnityEngine;

public class EnemyRaycastController : MonoBehaviour
{
    [SerializeField]
    Transform raycastSourceTransform;

    Enemy _enemy;
    [SerializeField] float _rayLength;
    [SerializeField]LayerMask _layerMask;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public (bool, RaycastHit2D) Raycast(Vector2 rayDirection, float rayLength, LayerMask layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastSourceTransform.position, rayDirection, rayLength, layerMask);
        // Draw ray
        Debug.DrawRay(raycastSourceTransform.position, rayDirection * rayLength, Color.green);

        if (hit.collider != null)
        {
            Debug.DrawRay(raycastSourceTransform.position, rayDirection * rayLength, Color.red);

            return (true, hit);
        }
        return (false, hit);
    }

    private void FixedUpdate()
    {
        if(_enemy != null)
        {
            if(_enemy.Direction == Vector2.zero)
            {
                return;
            }
            var raycastResult = Raycast(_enemy.Direction, _rayLength, _layerMask);
            if(raycastResult.Item1 == true)
            {
                RaycastHit2D raycastHit = raycastResult.Item2;
                Events.OnEnemyFoundBlockerAheadEvent?.Invoke(this.gameObject,raycastHit.collider.gameObject);
            }
            else
            {
                Events.OnEnemyFoundNoBlockerAheadEvent?.Invoke(this.gameObject);
            }
        }
    }
}