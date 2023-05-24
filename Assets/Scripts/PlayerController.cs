using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float longIdleTime = 5f;
    public float speed = 2.5f;
    public float jumpForce = 2.5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // LongIdle
    private float _longIdleTimer;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;

    // Atacar
    private bool _isAttacking;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        if (_isAttacking == false) // No puedes atacar mientras te mueves
        {
            // Movimiento
            float horizontalInput = Input.GetAxisRaw("Horizontal"); // Recojo el input del jugador
            _movement = new Vector2(horizontalInput, 0f);
            // Girar personaje
            if (horizontalInput < 0 && _facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0 && _facingRight == false)
            {
                Flip();
            }
        } 

        // ¿Esta en el suelo?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ¿Quieres saltar?
        if (Input.GetButtonDown("Jump")&& _isGrounded == true && _isAttacking == false)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // ¿Quieres atacar?
        if(Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        if (_isAttacking == false) // No puedes atacar mientras te mueves
        {
            float horizontalVelocity = _movement.normalized.x * speed; // Cojemos el valor x y lo multiplicamos por la velocidad
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y); // Muevo el objeto fisico
        } 
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero); // Si esta quieto que se active la animacion Idle
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        // Si el estado del Animator es Attack que ataque
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        } else
        {
            _isAttacking = false;
        }

        // LongIdle
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _longIdleTimer += Time.deltaTime;
            
            if( _longIdleTimer >= longIdleTime)
            {
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            _longIdleTimer = 0f;
        }
    }

    private void Flip() // Metodo para girar el personaje
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX= localScaleX * -1f; // Si es -1 lo pone a 1 y si es 1 lo pone a -1 
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
