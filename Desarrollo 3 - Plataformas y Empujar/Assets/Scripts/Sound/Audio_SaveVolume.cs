using UnityEngine;

public class Audio_SaveVolume : MB_SingletonDontDestroy<Audio_SaveVolume>
{
    [Header("All the Volumes")]
    public float masterValue;
    public float musicValue;
    public float sfxValue;
}
