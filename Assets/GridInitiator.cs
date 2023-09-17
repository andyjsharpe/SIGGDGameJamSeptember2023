using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class GridInitiator : MonoBehaviour
{
    [SerializeField] private Sprite[] roadSprites;
    [SerializeField] private Sprite[] intersectionSprites;
    [SerializeField] private Sprite[] concreteSprites;
    [SerializeField] private Sprite[] waterSprites;

    [SerializeField] private GameObject[] buildings;
    [SerializeField] private GameObject[] car;
    [SerializeField] private GameObject person;
    private List<Vector3> _intersections = new List<Vector3>();
    private List<GameObject> _buildings = new List<GameObject>();

    private Transform _playerTrans;

    // Start is called before the first frame update
    private void Start()
    {
        _playerTrans = FindObjectOfType<AlienControl>().transform;
        var tilemap = GetComponentInChildren<Tilemap>();
        tilemap.ResizeBounds();
        var bounds = tilemap.localBounds;
        for(var x = bounds.min.x; x < bounds.max.x; x++){
            for(var y= bounds.min.y; y < bounds.max.y; y++){
                for(var z= bounds.min.z; z < bounds.max.z; z++)
                {
                    var pos = new Vector3Int((int)x, (int)y, (int)z);
                    var tile = tilemap.GetTile(pos);
                    if (tile == null)
                    {
                        continue;
                    }
                    var reference = new TileData();
                    tile.GetTileData(pos, tilemap, ref reference);
                    var sprite = reference.sprite;
                    var worldPos = tilemap.CellToWorld(pos);
                    if (ArrayUtility.Contains(concreteSprites, sprite))
                    {
                        MakeBuilding(worldPos);
                    } else if (ArrayUtility.Contains(intersectionSprites, sprite))
                    {
                        _intersections.Add(worldPos);
                    } else if (ArrayUtility.Contains(roadSprites, sprite))
                    {
                        MakeCar(worldPos);
                    }
                }
            }
        }

        foreach (var intersection in _intersections)
        {
            MakePerson(intersection);
        }
    }

    private void MakeBuilding(Vector3 pos)
    {
        var g = Instantiate(buildings[Random.Range(0, buildings.Length)], pos + Vector3.up * 0.375f, quaternion.identity, transform);
        _buildings.Add(g);
    }
    
    private void MakeCar(Vector3 pos)
    {
        if (Random.value > 0.9f)
        {
            Instantiate(car[Random.Range(0, car.Length)], pos+ Vector3.up * 0.375f, quaternion.identity);
        }
    }
    
    private void MakePerson(Vector3 pos)
    {
        if (Random.value > 0.1f)
        {
            Instantiate(person, pos+ Vector3.up * 0.375f, quaternion.identity);
        }
    }

    public Vector3 SecondNearestIntersection(Vector3 currentPos)
    {
        float bestDist = -1;
        var bestPos = currentPos - Vector3.up * 0.375f;
        var secondBestPos = bestPos;
        foreach (var vec in _intersections)
        {
            var dist = Vector3.SqrMagnitude(currentPos - vec);
            if (bestDist < 0 || bestDist > dist)
            {
                bestDist = dist;
                secondBestPos = bestPos;
                bestPos = vec;
            }
        }

        return secondBestPos + Vector3.up * 0.375f;
    }
}
