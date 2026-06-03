using UnityEngine;

public class ZombieBase : BaseEntity
{
   public float speed = 2f;
    public Transform PlayerTransform { get; private set; }
    private IZombieState currentState;
    private ZombieData data;

   
    public int InstanceID { get; private set; }
    private SpawnManager manager;

   
    public void Initialize(ZombieData zombieData, int id, SpawnManager spawnManager, Transform playerTransform) 
    {
       
        this.data = zombieData;
        this.entityName = data.zombieName;
        this.speed = data.speed;
        
       
        this.InstanceID = id;
        this.manager = spawnManager;
        this.PlayerTransform = playerTransform;
    
      
        SetState(new ChaseState());
    }

    public void SetState(IZombieState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    private void Update()
    {
      
        if (currentState != null) currentState.UpdateState(this);
    }
    
   
    public void ApplyThreatColor(float threatValue) 
    {
        float t = threatValue / 100f;
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null) 
        {
            sr.color = Color.Lerp(Color.white, Color.red, t);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); 
            Die(); 
        }
    }

    
    public override void Die()
    {
       
        AutoWeapon weapon = FindFirstObjectByType<AutoWeapon>();
        if (weapon != null) weapon.RegisterKill();
    
       
        if (manager != null)
        {
            manager.RemoveById(InstanceID);
        }
    
       
        Destroy(gameObject);
    }
}