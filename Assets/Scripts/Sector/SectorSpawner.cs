using UnityEngine;

public class SectorSpawner : MonoBehaviour
{
    [SerializeField] GameObject sectorPrefab;
    [SerializeField] GameObject basePrefab;
    [SerializeField] SectorRegister sectorRegister;

    [SerializeField] int sizeX = 11;
    [SerializeField] int sizeZ = 11;

    private float sectorSize = 10;

    void Start()
    {
        Vector3 offset = new Vector3((sizeX - 1) * sectorSize / 2f, 0f, (sizeZ - 1) * sectorSize / 2f);
        GameObject sector;

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Vector3 position = new Vector3(x * sectorSize, 0, z * sectorSize) - offset;

                if (position.x == 0 && position.z == 0)
                {
                    Instantiate(basePrefab, position, Quaternion.identity, transform);
                    continue;
                }

                sector = Instantiate(sectorPrefab, position, Quaternion.identity, transform);
                sectorRegister.Register(sector.GetComponent<SectorController>());
            }
        }
    }
}
