using UnityEngine;

public class BeeObject : MonoBehaviour
{
    public Bee Bee;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            BeeManager.BeesCollected++;
            Bee.DestroyBee();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Bee.TargetPosition, 0.5f);
    }
}
