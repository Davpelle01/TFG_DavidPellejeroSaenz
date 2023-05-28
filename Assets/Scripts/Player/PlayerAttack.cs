using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking == true) 
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet"))
            {
                if (collision.CompareTag("BigBullet"))
                {
                    gameObject.SendMessageUpwards("AddPoints", 100);// Puntos por ejecutar un parry
                }
                else
                {
                    gameObject.SendMessageUpwards("AddPoints", 25); // Puntos por eliminar un enemigo un parry
                }
                
                collision.SendMessageUpwards("AddDamage");
            }
        }
    }

}

