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
                break;
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
}
