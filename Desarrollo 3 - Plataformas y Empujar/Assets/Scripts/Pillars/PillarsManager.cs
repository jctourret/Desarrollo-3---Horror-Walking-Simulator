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

    SpawnDirection lastSpawnDirection;

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
        NumbersOfPillars();

        SetPositionOfPillar();

        CreatePillar();
    }

    void BakeMesh()
    {
        navMesh.BuildNavMesh();
        Debug.Log("Baking Mesh");
    }

    //===========================================

    float ReturnPillarScale()
    {
        if (typeOfPillar == TypeOfPillars.InitialRoom)
        {
            typeOfPillar = TypeOfPillars.CommonRoom;
            return initialPillar.GetComponent<Pillar>().GetPillarScale();
        }
        else if (typeOfPillar == TypeOfPillars.FinalRoom)
        {
            return finalPillar.GetComponent<Pillar>().GetPillarScale();
        }
        else
        {
            return pillar.GetComponent<Pillar>().GetPillarScale();
        }
    }

    void SetPositionOfPillar()
    {
        Vector3 newPosition = Vector3.zero;
        float scale = ReturnPillarScale();

        lastSpawnDirection = SelectDirection();

        if (lastSpawnDirection == SpawnDirection.Up)
            newPosition = new Vector3(scale + distBetweenPillars, 0, scale + distBetweenPillars);
        else if (lastSpawnDirection == SpawnDirection.Down)
            newPosition = new Vector3(-scale - distBetweenPillars, 0, -scale - distBetweenPillars);
        else if (lastSpawnDirection == SpawnDirection.Left)
            newPosition = new Vector3(-scale - distBetweenPillars, 0, scale + distBetweenPillars);
        else if (lastSpawnDirection == SpawnDirection.Right)
            newPosition = new Vector3(scale + distBetweenPillars, 0, -scale - distBetweenPillars);

        this.transform.position = this.transform.position + newPosition;
    }

    void CreatePillar()
    {
        GameObject pref = null;

        if (typeOfPillar == TypeOfPillars.FinalRoom)
        {
            pref = finalPillar;
        }
        else if(typeOfPillar == TypeOfPillars.MarketRoom)
        {
            pref = marketPillar;
        }
        else if(typeOfPillar == TypeOfPillars.CommonRoom)
        {
            pref = pillar;
        }

        var go = Instantiate(pref, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(Vector3.up * -45), parent);
        go.transform.name = pref.name + "-" + (numerationPillars + 1).ToString();
        go.GetComponentInChildren<CallCameraTrigger>().cam = cam;
    }


    SpawnDirection SelectDirection()
    {
        int rand = UnityEngine.Random.Range(0, 4);

        SpawnDirection direction = (SpawnDirection)rand;

        // Estos if's son utilizados para evitar que se vuelva a elegir la direccion opuesta en la que se estuvo anteriormente
        // para evitar "volver hacia atras" con los pilares que se van generando y evitando que se quede estancado repitiendo muchas
        // veces eligiendo los mismos y nunca "avanzando".

        if (direction == SpawnDirection.Up && lastSpawnDirection == SpawnDirection.Down)
        {
            direction = SpawnDirection.Down;
        }
        else if (direction == SpawnDirection.Down && lastSpawnDirection == SpawnDirection.Up)
        {
            direction = SpawnDirection.Up;
        }
        else if (direction == SpawnDirection.Left && lastSpawnDirection == SpawnDirection.Right)
        {
            direction = SpawnDirection.Right;
        }
        else if (direction == SpawnDirection.Right && lastSpawnDirection == SpawnDirection.Left)
        {
            direction = SpawnDirection.Left;
        }

        return direction;
    }
}
