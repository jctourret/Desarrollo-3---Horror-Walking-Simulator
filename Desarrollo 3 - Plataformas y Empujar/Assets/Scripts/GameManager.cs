public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private bool firstEncounter = false;

    public void FirstEncounter()
    {
        if (!firstEncounter)
        {
            firstEncounter = true;

            AkSoundEngine.PostEvent("partida_musica02_enemigos", gameObject);
        }
    }
}