using System.Linq;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform backWall;
    [SerializeField] private Transform frontWall;
    [SerializeField] private Transform roof;
    [SerializeField] private Transform floor;

    private BoxCollider _leftWallCollider;
    private BoxCollider _rightWallCollider;
    private BoxCollider _backWallCollider;
    private BoxCollider _frontWallCollider;
    private BoxCollider _roofCollider;
    private BoxCollider _floorCollider;
    private void Start()
    {
        _leftWallCollider = leftWall.GetComponent<BoxCollider>();
        _rightWallCollider = rightWall.GetComponent<BoxCollider>();
        _backWallCollider = backWall.GetComponent<BoxCollider>();
        _frontWallCollider = frontWall.GetComponent<BoxCollider>();
        _roofCollider = roof.GetComponent<BoxCollider>();
        _floorCollider = floor.GetComponent<BoxCollider>();
        UpdateSize();
    }

    private void UpdateSize()
    {
        leftWall.transform.position = new Vector3(0, 0, -size/2);
        _leftWallCollider.size = new Vector3(size, size, 1);
        rightWall.transform.position = new Vector3(0, 0, size/2);
        _rightWallCollider.size = new Vector3(size, size, 1);
        backWall.transform.position = new Vector3(-size/2, 0, 0);
        _backWallCollider.size = new Vector3(1, size, size);
        frontWall.transform.position = new Vector3(size/2, 0, 0);
        _frontWallCollider.size = new Vector3(1, size, size);
        roof.transform.position = new Vector3(0, size/2, 0);
        _roofCollider.size = new Vector3(size, 1, size);
        floor.transform.position = new Vector3(0, -size/2, 0);
        _floorCollider.size = new Vector3(size, 1, size);
    }

    public float GetSize()
    {
        return size;
    }
}
