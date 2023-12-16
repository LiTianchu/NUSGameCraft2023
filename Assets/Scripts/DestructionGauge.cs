using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DestructionGauge : MonoBehaviour
{
    [SerializeField]
    private ShakeAnim shakeAnim;
    [SerializeField]
    private float shakeThreshold = 0.8f;

    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //update destructon gauge
        _slider.value = SimulatorManager.Instance.DestructionGauge;

        //let gameobject shake when destruction gauge is close to 100%
        if (SimulatorManager.Instance.DestructionGauge >= shakeThreshold)
        {
            
            shakeAnim.enabled = true;
            
            float severityCoeff = SimulatorManager.Instance.DestructionGauge <=99 ? (1 / (100 - SimulatorManager.Instance.DestructionGauge)) : 1;
            AudioManager.Instance.SetBGMPlayRate(1f + severityCoeff);
            shakeAnim.AdditionalShakeAmount = severityCoeff;
        }
        else
        {
            AudioManager.Instance.SetBGMPlayRate(1f);
            shakeAnim.enabled = false;
        }
    }
}
