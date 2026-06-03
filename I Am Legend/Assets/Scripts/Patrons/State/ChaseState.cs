using UnityEngine;

public class ChaseState : IZombieState 
{
    public void EnterState(ZombieBase zombie) 
    {
       
    }

    public void UpdateState(ZombieBase zombie) 
    {
        if (zombie.PlayerTransform != null) 
        {
            Vector3 direction = (zombie.PlayerTransform.position - zombie.transform.position).normalized;
            zombie.transform.position += direction * zombie.speed * Time.deltaTime;
        }
    }
}
