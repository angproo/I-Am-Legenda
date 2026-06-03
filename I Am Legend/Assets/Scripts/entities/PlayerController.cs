using UnityEngine;

public class PlayerController : BaseEntity
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.tag != "Player") gameObject.tag = "Player"; 
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Aplicación del Patrón Command
        ICommand move = new MoveCommand(rb, moveInput.normalized, moveSpeed);
        move.Execute();
    }

    public override void Die()
    {
        Debug.Log("Jugador Muerto");
        Time.timeScale = 0; 
    }
}