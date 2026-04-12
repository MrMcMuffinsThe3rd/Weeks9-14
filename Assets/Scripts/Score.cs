using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    int ghostsShot = 2;
    int invadersShot = 3;

    int totalKills = 0;

    public TextMeshProUGUI score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalKills = ghostsShot + invadersShot;

        score.text = totalKills.ToString();
    }
}
