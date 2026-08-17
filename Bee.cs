using UnityEngine;

public class Bee
{
    private GameObject BeeGameObject;
    private BeeObject BeeObject;

    private Vector3 Velocity;
    public Vector3 TargetPosition;

    public Vector3 Position
    {
        get
        {
            return BeeGameObject.transform.position;
        }
        private set
        {
            Vector3 newposition = value;

            if(newposition.x > BeeManager.SimulationBounds.x)
            {
                newposition.x = BeeManager.SimulationBounds.x;
            }
            else if (newposition.x < -BeeManager.SimulationBounds.x)
            {
                newposition.x = -BeeManager.SimulationBounds.x;
            }

            if (newposition.y > BeeManager.SimulationBounds.y)
            {
                newposition.y = BeeManager.SimulationBounds.y;
            }
            else if (newposition.y < -BeeManager.SimulationBounds.y)
            {
                newposition.y = -BeeManager.SimulationBounds.y;
            }

            if (newposition.z > BeeManager.SimulationBounds.z)
            {
                newposition.z = BeeManager.SimulationBounds.z;
            }
            else if (newposition.z < -BeeManager.SimulationBounds.z)
            {
                newposition.z = -BeeManager.SimulationBounds.z;
            }

            BeeGameObject.transform.position = newposition;
        }
    }

    public Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(BeeManager.BoundsLowerBound.x, BeeManager.BoundsUpperBound.x), Random.Range(BeeManager.BoundsLowerBound.y, BeeManager.BoundsUpperBound.y), Random.Range(BeeManager.BoundsLowerBound.z, BeeManager.BoundsUpperBound.z));
    }

    public BeeManager BeeManager => BeeManager.Instance;

    public bool Destroyed = false;

    public Bee()
    {
        BeeGameObject = GameObject.Instantiate(BeeManager.BeePrefab);
        Position = GetRandomPosition();
        TargetPosition = GetRandomPosition();

        BeeObject = BeeGameObject.GetComponent<BeeObject>();
        BeeObject.Bee = this;

        BeeManager.AddBeeToSimulation(this);
    }

    public void Step()
    {
        if(Destroyed == true)
        {
            return;
        }

        CalculateVelocity();

        BeeGameObject.transform.Translate(Velocity * Time.deltaTime, Space.World);

        BeeGameObject.transform.LookAt(Position + Velocity);

        if(Vector3.Distance(Position, TargetPosition) < 7.5f)
        {
            TargetPosition = GetRandomPosition();
        }
    }

    public void ApplyForce(Vector3 Force)
    {
        Velocity += Force * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity, BeeManager.BeeTerminalSpeed);
    }

    public void DestroyBee()
    {
        Destroyed = true;
        BeeManager.RemoveBeeFromSimulation(this);
        GameObject.Destroy(BeeGameObject);
    }

    private void CalculateVelocity()
    {
        Vector3 force = (TargetPosition - Position).normalized * BeeManager.BeeMovementForceMultiplier;

        ApplyForce(force);
    }
}