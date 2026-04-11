using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    List<FishData> fishTypes;

    [SerializeField]
    private float spawnInterval = 2f;

    private float timer;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        FishData data = ChooseFishByFrequency();
        if (data == null) return;

        GameObject fishObj = Instantiate(data.prefab, transform.position, Quaternion.identity);

        FishCrontroller controller = fishObj.GetComponent<FishCrontroller>();
        if (controller != null)
            controller.SetTarget(target);
    }

    private FishData ChooseFishByFrequency()
    {
        float total = 0f;
        foreach (var fish in fishTypes)
            total += fish.frequency;

        float random = Random.value * total;

        foreach (var fish in fishTypes)
        {
            if (random < fish.frequency)
                return fish;

            random -= fish.frequency;
        }

        return null;
    }
}

