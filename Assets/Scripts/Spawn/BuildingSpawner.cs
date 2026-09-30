using System.Collections;
using UnityEngine;

[System.Serializable]
public class Building
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField][Range(0, 1000)]
    private float chance; //이 숫자를 조절해서 생성 확률 조절
    private float rate; //실제 생성시 적용될 확률

    public GameObject Prefab { get => prefab; }
    public float Chance { get => chance; }//읽기 전용 프로퍼티
    public float Rate {  get => rate; set { rate = value; } }
}



public class BuildingSpawner : MonoBehaviour
{
    [SerializeField]
    private Building[] buildings;
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int maxSpawnCount;
    private int currentSpawnCount;
    public int MaxSpawnCount { get => maxSpawnCount; }

    private float totalChance; //chance의 총합
    private GameObject lastBlock; //가장 최근 블럭의 정보를 통해 새로운 블럭 생성 가능성 확인

    #region 생성되는 블럭 관련 코드
    [SerializeField][Range(0, 1)]
    private float spawnDelay;
    [SerializeField][Range(0, 10)]
    private float horizontalSpeeds;
    [SerializeField][Range(0, 5)]
    private int addedHorRandomRange;
    [SerializeField][Range(0, 10)]
    private float verticalSpeeds;
    #endregion

    [SerializeField]
    private GameObject tutorialObject; //튜토리얼 그림 사라지기 전까지 블럭 스폰 금지

    private void Awake()
    {
        CalculateChance();

        lastBlock = null;
        currentSpawnCount = 0;
    }
    private IEnumerator Start()
    {   //튜토리얼 이미지 사라지기 전까지 스폰x
        yield return new WaitUntil(()=>!tutorialObject.activeSelf);

        StartCoroutine(nameof(SpawnCoroutine));

        yield return null;
    }
    private IEnumerator SpawnCoroutine()
    {
        while(currentSpawnCount < MaxSpawnCount)
        {
            yield return new WaitUntil(() => lastBlock == null || lastBlock.GetComponent<PlayableMove>()?.IsMoving == false);

            yield return new WaitForSeconds(spawnDelay);

            lastBlock = SpawnBuilding();

            currentSpawnCount++;
        }

        //최대 스폰 수 도달 시 처리
    }

    public void DelaySpawn(float delay)
    {
        //외부에서 호출
        StartCoroutine(nameof(DelayCoroutine), delay);
    }

    private IEnumerator DelayCoroutine(float delay)
    {
        StopCoroutine(nameof(SpawnCoroutine));

        yield return new WaitForSeconds(delay);

        StartCoroutine(nameof(SpawnCoroutine));
    }

    private void CalculateChance()
    {
        totalChance = 0;

        foreach (Building b in buildings)
        {
            totalChance += b.Chance;
            
            b.Rate = totalChance;
        }
    }

    private GameObject SpawnBuilding()
    {
        Building building = buildings[GetRandomIndex()];
        GameObject block = Instantiate(building.Prefab, GetRandomSpawnPoint(), Quaternion.identity);
        
        int addedHorSpeed = Random.Range(0, addedHorRandomRange + 1);

        block.GetComponent<PlayableMove>().HorizontalSpeed = horizontalSpeeds + addedHorSpeed;
        block.GetComponent<PlayableMove>().VerticalSpeed = verticalSpeeds;

        return block;
    }

    private int GetRandomIndex()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float random = Random.Range(0, totalChance); // float [min, max]

        int result = 0;

        for(int i = 0; i < buildings.Length; i++)
        {
            if(random < buildings[i].Rate)
            {
                result = i;

                break;
            }
        }

        return result;
    }

    private Vector2 GetRandomSpawnPoint()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int index = Random.Range(0, spawnPoints.Length); //int [min, max)

        return spawnPoints[index].position;
    }

}
