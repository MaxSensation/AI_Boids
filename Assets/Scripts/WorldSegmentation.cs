using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldGeneration))]
public class WorldSegmentation : MonoBehaviour
{
    [SerializeField] private WorldGeneration worldGeneration;
    [Range(0.001f,1f)] [SerializeField] private float segmentDivider;
    [SerializeField] private GameObject segmentPrefab;

    public static List<Segment> segments = new List<Segment>();
    
    private void Start()
    {
        GenerateSegments();
    }

    private void GenerateSegments()
    {
        var size = segmentDivider * worldGeneration.GetSize();
        for (var level = 0; level < worldGeneration.GetSize() / size; level++)
        {
            for (var col = 0; col < worldGeneration.GetSize() / size; col++)
            {
                for (var row = 0; row < worldGeneration.GetSize() / size; row++)
                {
                    var segment = Instantiate(segmentPrefab, new Vector3(
                            -worldGeneration.GetSize() / 2 + size / 2 + size * row,
                            -worldGeneration.GetSize() / 2 + size / 2 + size * level,
                            -worldGeneration.GetSize() / 2 + size / 2 + size * col),
                        Quaternion.identity).GetComponent<Segment>();
                    segment.SetSize(Vector3.one * size);
                    segments.Add(segment);
                }
            }
        }
    }
}
