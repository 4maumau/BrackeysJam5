using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseParticles : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private string particleTag;

    [SerializeField] private Vector2 xVelocityRange;
    [SerializeField] private Vector2 yVelocityRange;

    [SerializeField] private Vector2Int amountRange;
    private bool emitedAlready = false;


    ObjectPooler objPooler;
    private void Start()
    {
        objPooler = ObjectPooler.Instance;
    }

    public void EmitParticles()
    {
        if (!emitedAlready)
        {
            emitedAlready = true;
            int particleAmount = Random.Range(amountRange.x, amountRange.y);

            for (int i = 0; i < particleAmount; i++)
            {
                //Feather instantiatedFeather = Instantiate(particlePrefab, transform.position, Quaternion.identity).GetComponent<Feather>();
                Feather instantiatedFeather = objPooler.SpawnFromPool(particleTag, transform.position, Quaternion.identity).GetComponent<Feather>();

                instantiatedFeather.Initialize(Random.insideUnitCircle * Random.Range(xVelocityRange.x, xVelocityRange.y),
                    Random.Range(yVelocityRange.x, yVelocityRange.y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
