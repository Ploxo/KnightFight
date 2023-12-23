using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 3f;

    public bool counting = false;

    private CounterManager cm;

    public TextMeshProUGUI countdownText;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        cm = GameObject.Find("GameManager").GetComponent<CounterManager>();
    }

    public void ResetTimer()
    {
        gameObject.SetActive(true);
        currentTime = startingTime;
    }
    // Update is called once per frame
    public void Update()
    {
        if(counting == true)
        {
            currentTime -= Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 1)
            {
                currentTime = 0;
                cm.countdownIsDone = true;
                counting = false;
                gameObject.SetActive(false);
            }

        }
    }
}
