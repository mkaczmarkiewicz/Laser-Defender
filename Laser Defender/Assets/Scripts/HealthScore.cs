using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthScore : MonoBehaviour
{
    [SerializeField] Player player;
    TextMeshProUGUI healthScore;
    void Start()
    {
        healthScore = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        healthScore.SetText(player.GetHealth());
    }
}
