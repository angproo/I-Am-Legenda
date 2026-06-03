using UnityEngine;

public class PlayerController : BaseEntity
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
      
        ICommand move = new MoveCommand(rb, moveInput.normalized, moveSpeed);
        move.Execute();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.GetComponent<ZombieBase>())
        {
            Die();
        }
    }

    public override void Die()
    {
        Time.timeScale = 0; // Pausa el juego
    }
}