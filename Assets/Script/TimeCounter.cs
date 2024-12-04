using System;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public float CurrentTime;
    
    [SerializeField]
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        _text.text = "Temps joué: " + TimeSpan.FromSeconds(CurrentTime).ToString(@"hh\:mm\:ss\.fff");
    }
}
