using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MapBehaviour : MonoBehaviour
{
    [Header("Map Values")]
    [SerializeField] PillarsManager pillarsManager;
    [SerializeField] GameObject pillar;

    [SerializeField] Transform pillarsParent;

    [SerializeField] RectTransform map;

    [Header("Map Pointers")]
    [SerializeField] GameObject playerToken;
    [SerializeField] GameObject marketToken;
    [SerializeField] GameObject FinalToken;

    //================================

    List<GameObject> listOfPillars = new List<GameObject>();
    int finalNum = 0;
    int marketNum = 0;

    float initialPosition = 0f;
    float distance = 0f;

    const int INITIAL_PILLAR = 1; // Para indicar el pilar seguro

    //================================

    private void OnEnable()
    {
        PillarsBehaviour.UIplayerToken += SetPlayerTokenPosition;
        BossPillarBehaviour.UIplayerToken += SetPlayerTokenPosition;
    }

    private void OnDisable()
    {
        PillarsBehaviour.UIplayerToken -= SetPlayerTokenPosition;        
        BossPillarBehaviour.UIplayerToken -= SetPlayerTokenPosition;
    }

    private void Start()
    {
        int[] variables = pillarsManager.MapCreation();

        marketNum = variables[0];
        finalNum = variables[1] + INITIAL_PILLAR;

        distance = map.rect.width / (finalNum - INITIAL_PILLAR);

        initialPosition = (map.rect.width / 2) * -1; // Para sacar la posicion inicial, teniendo en cuenta que va a estar en el centro de la pantalla

        for (int i = 0; i < finalNum; i++)
        {
            var go = Instantiate(pillar, new Vector3(initialPosition, 0, 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(pillarsParent, false);
            go.transform.name = "Pillar-" + i;

            initialPosition += distance;

            listOfPillars.Add(go);
        }

        SetTokens();
    }

    //================================

    void SetTokens()
    {
        playerToken.transform.position = listOfPillars[0].transform.position;

        marketToken.transform.position = listOfPillars[marketNum].transform.position;

        FinalToken.transform.position = listOfPillars[listOfPillars.Count - 1].transform.position;
    }

    public void SetPlayerTokenPosition()
    {
        StartCoroutine(MoveTheToken());
    }

    //================================

    IEnumerator MoveTheToken()
    {
        float time = 0f;

        int actualPosition = pillarsManager.numerationPillars;

        while (time <= 1)
        {
            playerToken.transform.position = Vector3.Lerp(playerToken.transform.position, listOfPillars[actualPosition].transform.position, time);

            time += Time.deltaTime;

            yield return null;
        }
    }
}
