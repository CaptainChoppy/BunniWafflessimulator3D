using UnityEngine;

public class Face : MonoBehaviour
{
    public Player Player;

    void Update()
    {
        transform.LookAt(Player.transform);
    }
}
