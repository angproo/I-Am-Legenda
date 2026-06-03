using UnityEngine;

public class MoveCommand : ICommand 
{
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float speed;

   
    public MoveCommand(Rigidbody2D rb, Vector2 moveDir, float speed) 
    {
        this.rb = rb;       
        this.moveDir = moveDir;
        this.speed = speed;
    }

    public void Execute() 
    {
        if (rb != null) 
        {
            rb.linearVelocity = moveDir * speed;
        }
    }
}
