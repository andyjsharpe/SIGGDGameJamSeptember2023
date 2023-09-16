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
    private List<Vector3> _intersections = new List<Vector3>();

    // Start is called before the first frame update
    private void Start()
    {
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
    }

    private void MakeBuilding(Vector3 pos)
    {
        Instantiate(buildings[Random.Range(0, buildings.Length)], pos, quaternion.identity);
    }
    
    private void MakeCar(Vector3 pos)
    {
        if (Random.value > 0.9f)
        {
            Instantiate(car[Random.Range(0, car.Length)], pos, quaternion.identity);
        }
    }

    public Vector3 SecondNearestIntersection(Vector3 currentPos)
    {
        float bestDist = -1;
        var bestPos = currentPos;
        var secondBestPos = currentPos;
        foreach (var vec in _intersections)
        {
            var dist = Vector3.Distance(currentPos, vec);
            if (bestDist < 0 || bestDist > dist)
            {
                bestDist = dist;
                secondBestPos = bestPos;
                bestPos = vec;
            }
        }

        return secondBestPos;
    }
}
