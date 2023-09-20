using TMPro;
using UnityEngine;

public class EndText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI abductees;
    [SerializeField] private TextMeshProUGUI heat;
    
    // Start is called before the first frame update
    private void Start()
    {
        timer.text = "Time Survived: " + PlayerPrefs.GetFloat("Time").ToString("F2");
        abductees.text = "People abducted: " + PlayerPrefs.GetInt("Abductees");
        heat.text = "Notoriety Reached: " + PlayerPrefs.GetFloat("Heat").ToString("F2");
    }
}
