using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class YearTimer : MonoBehaviour
{
    private TMP_Text _yearText;

    private void Start()
    {
        _yearText = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        _yearText.text = SimulatorManager.Instance.CurrentYear.ToString();
    }
}
