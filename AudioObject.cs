using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField]
    public AudioPreset[] AudioPresets;

    private void Start()
    {
        AudioManager.Initiate();

        AudioManager.PlaySound(AudioPresets[0]);
    }
}
