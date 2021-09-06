using UnityEngine;

public class SetRoom : MonoBehaviour
{
    public Transform parentRoom;

    GameObject room;

    private void OnEnable()
    {
        room = LoaderManager.Get().GetARoom();

        var go = Instantiate(room, parentRoom);
        go.transform.name = room.name;
    }
}
