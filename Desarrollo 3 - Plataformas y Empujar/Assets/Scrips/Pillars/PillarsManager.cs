using UnityEngine.AI;
using UnityEngine;

public class PillarsManager : MonoBehaviour
{ 
    [Header("Pillars Spawner")]
    public Transform parent;
    public GameObject pillar;
    public float scalePillars = 20f;
    public float distBetweenPillars = 2f;

    public int numerationPillars = 0;

    NavMeshSurface navMesh;

    enum SpawnDirection
    {
        Up,     // X axis +
        Down,   // X axis -
        Left,   // Z axis +
        Right   // Z axis -
    };

    SpawnDirection spawnDirection;

    //=====================================

    private void Awake()
    {
        numerationPillars = 0;
        navMesh = GetComponent<NavMeshSurface>();
    }

    private void OnEnable()
    {
        PillarsBehaviour.IsCollapsing += CallOtherPillar;
        PillarsBehaviour.CreatePillar += NumbersOfPillars;
        PillarsBehaviour.OnPillarUp += BakeMesh;
    }

    private void OnDisable()
    {
        PillarsBehaviour.IsCollapsing -= CallOtherPillar;        
        PillarsBehaviour.CreatePillar -= NumbersOfPillars;
        PillarsBehaviour.OnPillarUp -= BakeMesh;
    }

    //=====================================

    void NumbersOfPillars()
    {
        numerationPillars++;
    }

    void CallOtherPillar()
    {
        Vector3 newPosition;

        spawnDirection = SelectDirection();        

        switch (spawnDirection)
        {
            case SpawnDirection.Up:

                newPosition = new Vector3(scalePillars + distBetweenPillars , 0, 0);

                this.transform.position = this.transform.position + newPosition;
                
                break;
            case SpawnDirection.Down:

                newPosition = new Vector3( -scalePillars - distBetweenPillars, 0, 0);

                this.transform.position = this.transform.position + newPosition;

                break;
            case SpawnDirection.Left:

                newPosition = new Vector3(0, 0, scalePillars + distBetweenPillars);

                this.transform.position = this.transform.position + newPosition;

                break;
            case SpawnDirection.Right:

                newPosition = new Vector3(0, 0, -scalePillars - distBetweenPillars);

                this.transform.position = this.transform.position + newPosition;

                break;
        }


        GameObject go = Instantiate(pillar, new Vector3(this.transform.position.x, pillar.transform.position.y, this.transform.position.z), Quaternion.Euler(Vector3.up), parent);

        go.transform.name = pillar.name + "-" + (numerationPillars + 1).ToString();
    }

    void BakeMesh()
    {
        navMesh.BuildNavMesh();
        Debug.Log("Baking Mesh");
    }

    //===========================================

    SpawnDirection SelectDirection()
    {
        int rand = Random.Range(0, 4);

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
