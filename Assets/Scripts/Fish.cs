using System;
using System.Linq;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float targetMinDistance;
    [SerializeField] private float maxTurnAroundSpeed;
    [SerializeField] private float obstacleTurnSpeed;
    [SerializeField] private float targetTurnSpeed;
    [SerializeField] private float alignmentTurnSpeed;
    [SerializeField] private float cohesionTurnSpeed;
    [SerializeField] private float separationTurnSpeed;
    [SerializeField] private float minimumDistance;
    [SerializeField] private float holdDistance;
    
    private GameObject _target;
    private Segment _currentSegment;
    private Fish _closestFish;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Target");
    }

    private void Update()
    {
        CheckSegment();
        GetClosestFish();
        CalculateDirection();
        MoveFish();
    }

    private void MoveFish()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void CalculateDirection()
    {
        if ((_target.transform.position - transform.position).sqrMagnitude > targetMinDistance)
        {
            var targetRotation = Quaternion.LookRotation(_target.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, targetTurnSpeed * Time.deltaTime);
        }
        else
        {
            var targetRotation = Quaternion.LookRotation(transform.position - _target.transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxTurnAroundSpeed * Time.deltaTime);
        }

        if (_closestFish == null) return;
        var alignmentRotation = _closestFish.transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, alignmentRotation, alignmentTurnSpeed * Time.deltaTime);
        if ((_closestFish.transform.position - transform.position).sqrMagnitude > holdDistance)
        {
            var cohesionRotation = Quaternion.LookRotation(_closestFish.transform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, cohesionRotation, cohesionTurnSpeed * Time.deltaTime);
        }
        if ((_closestFish.transform.position - transform.position).sqrMagnitude < minimumDistance)
        {
            var separationRotation = Quaternion.LookRotation(transform.position - _closestFish.transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, separationRotation, separationTurnSpeed * Time.deltaTime);
        }
    }

    private void GetClosestFish()
    {
        _closestFish = _currentSegment.GetClosestFish(this);
    }

    private void CheckSegment()
    {
        var newSegment = WorldSegmentation.segments.OrderBy(segment => (segment.transform.position - transform.position).sqrMagnitude).First();
        if (newSegment == _currentSegment) return;
        if (_currentSegment != null) _currentSegment.RemoveFish(this);
        newSegment.AddFish(this);
        _currentSegment = newSegment;
    }

    private void OnDrawGizmos()
    {
        if (_closestFish == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, _closestFish.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            var obstacleRotation = Quaternion.LookRotation(transform.position - other.transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, obstacleRotation, obstacleTurnSpeed * Time.deltaTime);   
        }

        if (other.CompareTag("Wall"))
        {
            var homeRotation = Quaternion.LookRotation(Vector3.zero - other.transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, homeRotation, maxTurnAroundSpeed);
        }
    }
}
