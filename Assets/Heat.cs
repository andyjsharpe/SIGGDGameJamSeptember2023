using UnityEngine;
using UnityEngine.UI;

public class Heat : MonoBehaviour
{
    private float _heat = 0;
    [SerializeField] private Slider[] heatSliders;

    public void AddHeat(float heatDelta)
    {
        _heat += heatDelta;
        heatSliders[0].value = Mathf.Max(0, Mathf.Min(1, _heat));
        heatSliders[1].value = Mathf.Max(0, Mathf.Min(2, _heat) - 1);
        heatSliders[2].value = Mathf.Max(0, Mathf.Min(3, _heat) - 2);
        heatSliders[3].value = Mathf.Max(0, Mathf.Min(4, _heat) - 3);
        heatSliders[4].value = Mathf.Max(0, Mathf.Min(5, _heat) - 4);
    }
}
