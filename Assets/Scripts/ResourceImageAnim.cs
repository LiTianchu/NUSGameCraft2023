using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceImageAnim : MonoBehaviour
{
    [SerializeField]
    private float minScale = 0.8f;
    [SerializeField]
    private float maxScale = 1.2f;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private World relatingWorld;

    private Image _img;
    private float _sign = 1f;
    private Vector3 _originalScale;
    public bool IsAnimating { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _img = GetComponent<Image>();
        _originalScale = _img.transform.localScale;
        IsAnimating = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsAnimating = relatingWorld.GetNumOfAdv() > 0;
        if(IsAnimating)
        {
            if (_img.transform.localScale.x > maxScale)
            {
                _sign = -1f;
            }

            if (_img.transform.localScale.x < minScale)
            {
                _sign = 1f;
            }

            _img.transform.localScale += new Vector3(speed * Time.deltaTime * _sign, speed * Time.deltaTime * _sign, 0f);

        }
        else
        {
            _img.transform.localScale = _originalScale;
        }

    }
}
