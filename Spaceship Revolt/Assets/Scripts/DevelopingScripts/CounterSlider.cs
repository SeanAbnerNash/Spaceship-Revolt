using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterSlider : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider progressBar;
    void Start()
    {
        progressBar = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.value = GameData.Instance.GetCurrentCounterProgression() / GameData.Instance.GetMaxCounterProgression();
    }
}
