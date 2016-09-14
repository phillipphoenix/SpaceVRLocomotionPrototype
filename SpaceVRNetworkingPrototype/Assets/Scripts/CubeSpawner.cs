using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;

public class CubeSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject _cubePrefab;
    [SerializeField]
    private int _spawnAmount;
    [SerializeField]
    private float _spawnAreaSize = 5;
    [SerializeField]
    private Transform _cubeParent;

    public int SpawnCount { get; private set; }

    [Command]
    public void CmdSpawnCubes()
    {
        foreach (var i in Enumerable.Range(0, _spawnAmount))
        {
            GameObject obj = Instantiate(_cubePrefab);
            obj.transform.parent = _cubeParent;
            Vector3 normalRandomPos = new Vector3(Random.Range(-.5f, .5f), Random.Range(0, 1f), Random.Range(0, 1f));
            Vector3 scaledRandomPos = normalRandomPos * _spawnAreaSize;
            obj.GetComponent<Rigidbody>().MovePosition(scaledRandomPos);
            NetworkServer.Spawn(obj);
            SpawnCount++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(0, _spawnAreaSize/2, .5f), new Vector3(_spawnAreaSize, _spawnAreaSize, _spawnAreaSize));
    }
}
