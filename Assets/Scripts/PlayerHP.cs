using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int totalHealth = 3;
    public RectTransform healthUI;

    //Game Over
    public RectTransform gameOverMenu;
    public GameObject spawns;

    private int health;
    private float heartSize = 16f;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PlayerController _playerController;
    private GameObject _soundtrack;
    private GameObject _menuSoundtrack;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _soundtrack = GameObject.Find("SoundTrack");
        _menuSoundtrack = GameObject.Find("MenuSoundTrack");
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        //Feedback de daño (Cambia de color 0.1 segundo)
        StartCoroutine("HealthFeedback");

        health = health - amount;

        //Game over
        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false); // Desactivar Jugador
        }
        healthUI.sizeDelta = new Vector2(heartSize*health, heartSize);
    }
    // Update is called once per frame
    public void AddHealth(int amount)
    {
        health = health + amount;

        // No superar la vida maxima
        if (health > totalHealth)
        {
            health = totalHealth;
        }
        healthUI.sizeDelta = new Vector2(heartSize * health, heartSize);
    }

    private IEnumerator HealthFeedback()
    {
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = Color.white;
    }
    private void OnEnable()
    {
        health = totalHealth;
    }
    private void OnDisable()
    {
        gameOverMenu.gameObject.SetActive(true); // Activar menu de game over
        spawns.SetActive(false);
        _animator.enabled = false;
        _playerController.enabled = false;

        _soundtrack.SetActive(false);
        _menuSoundtrack.SetActive(true);
    }
}

