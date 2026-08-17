public static class Options
{
    private static float volume = 1.0f;
    public static float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = value;
            AudioManager.UpdateVolumes();
        }
    }

    public static float MouseSensitivity = 150.0f;
}
