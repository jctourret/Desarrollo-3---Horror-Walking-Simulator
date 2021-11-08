using System;
using UnityEngine.AI;
using UnityEngine;

public class PillarsManager : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] float distBetweenPillars = 2f;

    [Header("Initial Pillar")]
    [SerializeField] GameObject initialPillar;

    [Header("Pillars Spawner")]
    [SerializeField] GameObject pillar;

    [Header("Market and Treasure pillars")]
    [SerializeField] GameObject marketPillar;
    [SerializeField] [Range(-5, 0)] int minRandNumber = -1; // Para el azar de la aparicion de la habitacion
    [SerializeField] [Range(0, 5)] int maxRandNumber = 3;
    [SerializeField] int pillarsBeforeMerket; // ---> uno de los valores a pedir para el mapa

    [Header("Final Pillars Spawner")]
    [SerializeField] GameObject finalPillar;
    [SerializeField] int pillarsBeforeFinal = 10; // ---> uno de los valores a pedir para el mapa

    [Header("Actual Pillar")]
    public int numerationPillars = 0;

    //=====================================

    NavMeshSurface navMesh;
    Camera cam;

    enum SpawnDirection
    {
        Up,     // X axis +
        Down,   // X axis -
        Left,   // Z axis +
        Right   // Z axis -
    };

    SpawnDirection spawnDirection;

    enum TypeOfPillars
    {
        InitialRoom,
        CommonRoom,
        MarketRoom,
        FinalRoom
    };

    TypeOfPillars typeOfPillar = TypeOfPillars.InitialRoom;

    //=====================================

    private void Awake()
    {
        numerationPillars = 0;
        navMesh = GetComponent<NavMeshSurface>();

        this.transform.position = initialPillar.transform.position;

        // Seleccion del numero del pillar del mercado:
        int mid = pillarsBeforeFinal / 2;
        pillarsBeforeMerket = mid + UnityEngine.Random.Range(minRandNumber, maxRandNumber);
    }

    private void OnEnable()
    {
        PillarsBehaviour.CreatePillar += CallOtherPillar;
        PillarsBehaviour.OnPillarUp += BakeMesh;
        BossPillarBehaviour.OnPillarUp += BakeMesh;

        StartLever.ActivateObject += CallOtherPillar;
        CameraBehaviour.OnSendCamera += GetCamera;
    }

    private void OnDisable()
    {
        PillarsBehaviour.CreatePillar -= CallOtherPillar;
        PillarsBehaviour.OnPillarUp -= BakeMesh;
        BossPillarBehaviour.OnPillarUp -= BakeMesh;

        StartLever.ActivateObject -= CallOtherPillar;
        CameraBehaviour.OnSendCamera -= GetCamera;
    }

    //=====================================

    public int[] MapCreation() // Para la creacion del mapa UI
    {
        int[] map = { pillarsBeforeMerket, pillarsBeforeFinal };
        return map;
    }

    //=====================================

    void GetCamera(Camera newCamera)
    {
        cam = newCamera;
    }

    void NumbersOfPillars()
    {
        numerationPillars++;

        if(numerationPillars == 1)
        {
            typeOfPillar = TypeOfPillars.InitialRoom;
        }
        else if (numerationPillars == pillarsBeforeMerket)
        {
            typeOfPillar = TypeOfPillars.MarketRoom;
        }
        else if (numerationPillars >= pillarsBeforeFinal)
        {
            typeOfPillar = TypeOfPillars.FinalRoom;
        }
        else
        {
            typeOfPillar = TypeOfPillars.CommonRoom;
        }
    }

    void CallOtherPillar()
    {
        Vector3 newPosition;
        float scale = 0f;

        // ---

        NumbersOfPillars();

        // ---

        spawnDirection = SelectDirection();

        switch (typeOfPillar)
        {
            case TypeOfPillars.InitialRoom:
                scale = initialPillar.GetComponent<Pillar>().GetPillarScale();
                typeOfPillar = TypeOfPillars.CommonRoom;
                break;

            case TypeOfPillars.FinalRoom:
                scale = finalPillar.GetComponent<Pillar>().GetPillarScale();
                break;

            default:
                scale = pillar.GetComponent<Pillar>().GetPillarScale();
                break;
        }

        // ---

        switch (spawnDirection)
        {
            case SpawnDirection.Up:
                newPosition = new Vector3(scale + distBetweenPillars, 0, scale + distBetweenPillars);
                this.transform.position = this.transform.position + newPosition;                
                break;

            case SpawnDirection.Down:
                newPosition = new Vector3(-scale - distBetweenPillars, 0, -scale - distBetweenPillars);
                this.transform.position = this.transform.position + newPosition;
                break;

            case SpawnDirection.Left:
                newPosition = new Vector3(-scale - distBetweenPillars, 0, scale + distBetweenPillars);
                this.transform.position = this.transform.position + newPosition;
                break;

            case SpawnDirection.Right:
                newPosition = new Vector3(scale + distBetweenPillars, 0, -scale - distBetweenPillars);
                this.transform.position = this.transform.position + newPosition;
                break;
        }

        // ---

        switch (typeOfPillar)
        {
            case TypeOfPillars.FinalRoom:
                var go = Instantiate(finalPillar, new Vector3(this.transform.position.x, pillar.transform.position.y, this.transform.position.z), Quaternion.Euler(Vector3.up * -45), parent);
                go.transform.name = finalPillar.name;
                go.GetComponentInChildren<CallCameraTrigger>().cam = cam;
                break;

            case TypeOfPillars.MarketRoom:
                var go2 = Instantiate(marketPillar, new Vector3(this.transform.position.x, pillar.transform.position.y, this.transform.position.z), Quaternion.Euler(Vector3.up * -45), parent);
                go2.transform.name = marketPillar.name + "-" + (numerationPillars + 1).ToString();
                go2.GetComponentInChildren<CallCameraTrigger>().cam = cam;
                break;

            case TypeOfPillars.CommonRoom:
                var go3 = Instantiate(pillar, new Vector3(this.transform.position.x, pillar.transform.position.y, this.transform.position.z), Quaternion.Euler(Vector3.up * -45), parent);
                go3.transform.name = pillar.name + "-" + (numerationPillars + 1).ToString();
                go3.GetComponentInChildren<CallCameraTrigger>().cam = cam;
                break;
        }
    }

    void BakeMesh()
    {
        navMesh.BuildNavMesh();
        Debug.Log("Baking Mesh");
    }

    //===========================================

    SpawnDirection SelectDirection()
    {
        int rand = UnityEngine.Random.Range(0, 4);

        SpawnDirection direction = (SpawnDirection)rand;

        switch (direction)
        {
            case SpawnDirection.Up:
                if (spawnDirection == SpawnDirection.Down)
                {
                    direction = SpawnDirection.Down;
                }
                break;

            case SpawnDirection.Down:
                if (spawnDirection == SpawnDirection.Up)
                {
                    direction = SpawnDirection.Up;
                }
                break;

            case SpawnDirection.Left:
                if (spawnDirection == SpawnDirection.Right)
                {
                    direction = SpawnDirection.Right;
                }
                break;

            case SpawnDirection.Right:
                if (spawnDirection == SpawnDirection.Left)
                {
                    direction = SpawnDirection.Left;
                }
                break;
        }

        return direction;
    }
}
