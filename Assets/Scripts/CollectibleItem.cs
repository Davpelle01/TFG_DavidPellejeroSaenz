using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int regeneration = 1;
    public GameObject lightingParticles;
    public GameObject burstParticles;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Curar al jugador
            collision.SendMessageUpwards("AddHealth", regeneration);

            // Deshabilitar el objeto
            _collider.enabled = false;

            // Particulas y efectos visuales
            _spriteRenderer.enabled = false;
            lightingParticles.SetActive(false);
            burstParticles.SetActive(true);

            // Destruir el objeto
            Destroy(gameObject, 2f);
        }
        
    }
}
