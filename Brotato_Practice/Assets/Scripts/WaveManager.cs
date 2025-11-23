using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;

    //making a static instance of wavemanager allows other scripts to call wavemanager
    public static WaveManager Instance;


    bool waveRunning = true;
    int currentWave = 0;
    int currentWaveTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartNewWave();
        timeText.text = "30";
        waveText.text = "Wave: 1";
    }

    private void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartNewWave();
        }
    }

    public bool WaveRunning() => waveRunning;

    private void StartNewWave()
    {
        StopAllCoroutines();

        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30;
        waveText.text = "Wave" + currentWave;

        //below allows the game wait for secs before we execute something
        StartCoroutine(WaveTimer());
    }

    //below subtracts 1 from wavetime every second 
    IEnumerator WaveTimer()
    {
        while (waveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;

            timeText.text = currentWaveTime.ToString();

            if (currentWaveTime <= 0)
                WaveComplete();
        }
        yield return null;
    }

    private void WaveComplete()
    {
        StopAllCoroutines();
        EnemyManager.Instance.DestoryAllEnemies();
        waveRunning = false;
        currentWaveTime = 30;
        waveText.text = currentWaveTime.ToString();
        timeText.color = Color.red;
    }
}
