using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Segment : MonoBehaviour
{
    private readonly List<Fish> _fish = new List<Fish>();
    private List<Segment> _neighbors = new List<Segment>();

    public void AddFish(Fish fish)
    {
        _fish.Add(fish);
    }
    
    public void RemoveFish(Fish fish)
    {
        _fish.Remove(fish);
    }

    public void SetSize(Vector3 size)
    {
        transform.localScale = size;
    }

    private void OnDrawGizmos()
    {
        if (_fish.Count > 0)
            Gizmos.color = new Color(0, 1, 0, 0.2f);
        else
            Gizmos.color = new Color(1, 0, 0, 0.01f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    public Fish GetClosestFish(Fish searchingFish)
    {
        var newColl = _fish.Where(fish => fish != searchingFish);
        return newColl.Count() == 0 ? null : newColl.OrderBy(fish => (searchingFish.transform.position - fish.transform.position).sqrMagnitude).First();
    }
}