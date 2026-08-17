using UnityEngine;

public class MaterialOffsetAnimation : MonoBehaviour
{
    public Material Material;

    public Vector2 OffsetVelocity;

    private void Update()
    {
        Material.mainTextureOffset += OffsetVelocity * Time.deltaTime;
    }
}
