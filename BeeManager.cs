using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public static BeeManager Instance;

    public float BeeTerminalSpeed = 15.0f;
    public float BeeMovementForceMultiplier = 1.0f;

    public int NumberOfBees = 10;

    public GameObject BeePrefab;

    public List<Bee> Bees = new List<Bee>();

    public Vector3 SimulationBounds = Vector3.one * 10.0f;
    public Vector3 SimulationOffset = Vector3.zero;

    public Vector3 BoundsUpperBound => (SimulationBounds / 2) + SimulationOffset;
    public Vector3 BoundsLowerBound => (-SimulationBounds / 2) + SimulationOffset;

    public bool FreezeSimulation = false;

    public static int BeesCollected;

    private void Start()
    {
        Instance = this;

        BeesCollected = 0;

        for (int i = 0; i < NumberOfBees; i++)
        {
            AddBeeToSimulation(new Bee());
        }
    }

    private void Update()
    {
        if(FreezeSimulation == true)
        {
            return;
        }

        foreach(Bee bee in Bees)
        {
            bee.Step();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(SimulationOffset, SimulationBounds);
    }

    public void AddBeeToSimulation(Bee bee)
    {
        Bees.Add(bee);
    }

    public void RemoveBeeFromSimulation(Bee bee)
    {
        Bees.Remove(bee);
    }
}
