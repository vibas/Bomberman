using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    public AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake()
    {
        activeSpriteRenderer = spriteRendererDown;
    }

    private void OnEnable()
    {
        Events.OnPlayerDeathEvent += DeathSequence;
    }

    private void OnDisable()
    {
        Events.OnPlayerDeathEvent -= DeathSequence;
    }

    public void UpdatePlayerSpriteRenderer(AnimatedSpriteRenderer spriteRenderer, bool isIdle)
    {
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = isIdle;
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        Events.OnPlayerDeathSequenceCompleteEvent?.Invoke();
    }
}
