using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System.Linq;

public class GridInitiator : MonoBehaviour
{
    /*
    [SerializeField] private Sprite[] roadSprites;

    [SerializeField] private GameObject[] car;

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
                    if (roadSprites.Contains(sprite))
                    {
                        MakeCar(worldPos);
                    }
                }
            }
        }

    }

    private void MakeCar(Vector3 pos)
    {
        if (Random.value > 0.9f)
        {
            Instantiate(car[Random.Range(0, car.Length)], pos+ Vector3.up * 0.375f, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
    */
}
