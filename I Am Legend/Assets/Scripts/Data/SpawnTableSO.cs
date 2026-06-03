using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RangoMiedoConfig
{
    public int minMiedo;
    public int maxMiedo;
    public List<ZombieData> listaZombies;
}

[CreateAssetMenu(fileName = "NuevaTablaSpawn", menuName = "Zombies/Tabla Spawn")]
public class SpawnTableSO : ScriptableObject
{
    public List<RangoMiedoConfig> configuraciones;

    public ZombieData ObtenerZombiePorMiedo(int nivelMiedo)
    {
        foreach (var config in configuraciones)
        {
            if (nivelMiedo >= config.minMiedo && nivelMiedo <= config.maxMiedo)
            {
                if (config.listaZombies.Count > 0)
                {
                    int randomIndex = Random.Range(0, config.listaZombies.Count);
                    return config.listaZombies[randomIndex];
                }
            }
        }
        return null;
    }
}
