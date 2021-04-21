using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSpawner : MonoBehaviour
{
    public Sprite[] spritesSoltos;
    public Sprite[] telas;

    public GameObject prefab;
    public GameObject telaPrefab;
    public float spawnWait = 2f;
    public float lerpTime = 10f;

    private bool isLerping = false;

    void Start()
    {
        print(spritesSoltos.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("SpawnsImages");
            isLerping = true;
        }
        if (isLerping)
        {
            spawnWait = Mathf.Lerp(spawnWait, 0.05f, lerpTime * Time.deltaTime);
        }
    }

    public void SpawnRandom()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
        Instantiate(prefab, screenPosition, Quaternion.identity);
    }

    IEnumerator SpawnsImages() {
        for (int i = 0; i < telas.Length; i++)
        {
            SpriteRenderer spriteRenderer = telaPrefab.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = telas[i];
            spriteRenderer.sortingOrder = i * 1;

            Instantiate(telaPrefab, Vector3.zero, Quaternion.identity);

            yield return new WaitForSeconds(spawnWait);
        }

        for (int i = 0;  i < spritesSoltos.Length; i++)
        {
            SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spritesSoltos[i];
            spriteRenderer.sortingOrder = i + 6;


            prefab.GetComponent<SpriteRenderer>().sprite = spritesSoltos[i];
            SpawnRandom();

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
