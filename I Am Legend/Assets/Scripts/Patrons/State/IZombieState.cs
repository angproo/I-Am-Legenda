using UnityEngine;

public interface IZombieState 
{
    void EnterState(ZombieBase zombie);
    void UpdateState(ZombieBase zombie);
}
