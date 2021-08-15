using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour, IRoom
{
    public enum Behaviour
    {
        Rotate,
        MoveTo,
    };
    public Behaviour behaviour;

    [Header("Rotate values")]
    public Vector3 finalRotationVec;

    [Header("Move To values")]
    public Vector3 newPosition;

    [Header("Show Gameobjects")]
    public List<GameObject> hiddenObjs;

    float MAX_TIME = 1f;

    // ================================================================== \\

    public void StartBehaviour()
    {
        switch (behaviour)
        {
            case Behaviour.Rotate:

                StartCoroutine(RotateObj());

                break;

            case Behaviour.MoveTo:

                MoveRoom();

                break;
        }

        foreach(GameObject obj in hiddenObjs)
        {
            if(obj != null)
                obj.SetActive(true);
        }
    }

    // ================================================================== \\

    IEnumerator RotateObj()
    {
        float time = 0f;

        Quaternion initialRot = this.transform.rotation;
        Quaternion destiny = Quaternion.Euler(finalRotationVec);

        while (time < MAX_TIME)
        {
            this.transform.rotation = Quaternion.Lerp(initialRot, destiny, time);

            time += Time.deltaTime;

            yield return null;
        }
    }

    void MoveRoom()
    {

        this.transform.position = newPosition;

    }
}
