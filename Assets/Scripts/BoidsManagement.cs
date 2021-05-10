using UnityEngine;

public class BoidsManagement : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private int totalFish;

    private void Start()
    {
        SpawnFish();
    }

    private void SpawnFish()
    {
        for (var i = 0; i < totalFish; i++)
        {
            Instantiate(fishPrefab, transform.position, Random.rotation);
        }
    }
}
