public class GameManager : MB_SingletonDestroy<GameManager>
{
    private bool firstEncounter = false;

    public void Start()
    {
        firstEncounter = false;
    }

    public void FirstEncounter()
    {
        if (!firstEncounter)
        {
            firstEncounter = true;

            AkSoundEngine.PostEvent("partida_musica02_enemigos", gameObject);
        }
    }
}