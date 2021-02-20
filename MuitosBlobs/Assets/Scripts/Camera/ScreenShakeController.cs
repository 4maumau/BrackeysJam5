using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController instance;

    public float trauma;

    private float shakeTimeCountdown, shakePower, shakeFadeTime, shakeRotation;

    [SerializeField] private float rotationMultiplier = 15f;

    void Start()
    {
        instance = this;
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartShake(.5f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            trauma += .5f;
        }

        if (trauma > 0)
            trauma -= 2f * Time.deltaTime;
        else if (trauma <= 0)
            trauma = 0;


        shakePower = Mathf.Pow(trauma, 2f);
        shakeRotation = shakePower * rotationMultiplier;

    }
    
    private void LateUpdate()
    {
        if (trauma > 0)
        {
            shakeTimeCountdown -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float time, float power)
    {
        shakeTimeCountdown = time;
        shakePower = power;

        shakeFadeTime = power / time;

        shakeRotation = power * rotationMultiplier; 
    }

    public void AddTrauma(float _trauma)
    {
        if (trauma < .35f)
            trauma += _trauma;
    }
}
