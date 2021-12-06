using UnityEngine;
using System.Collections;

public class SoundManager : MB_SingletonDestroy<SoundManager>
{
    private bool firstEncounter = false;

    public enum MusicState
    {
        Common,
        Market,
        Boss
    }

    public MusicState musicState = MusicState.Common;

    public void Start()
    {
        firstEncounter = false;

        musicState = MusicState.Common;
    }

    public void FirstEncounter()
    {
        if (!firstEncounter)
        {
            firstEncounter = true;

            AkSoundEngine.PostEvent("partida_musica02_enemigos", gameObject);
        }
    }

    public void ChangeMainMusic(MusicState state)
    {
        switch (state)
        {
            case MusicState.Common:

                if(musicState != state)
                {
                    if(musicState == MusicState.Market)
                        AkSoundEngine.PostEvent("partida_musica03_mercado_OUT", gameObject);
                    musicState = MusicState.Common;
                }

                break;
            case MusicState.Market:

                if (musicState != state)
                {
                    AkSoundEngine.PostEvent("partida_musica03_mercado_IN", gameObject);
                    AkSoundEngine.PostEvent("mono_frase1_welcome", gameObject);
                    musicState = MusicState.Market;
                }

                break;
            case MusicState.Boss:

                if (musicState != state)
                {
                    AkSoundEngine.PostEvent("partida_musica04_pre_boss", gameObject);
                    musicState = MusicState.Boss;
                }

                break;
        }
    }    
}