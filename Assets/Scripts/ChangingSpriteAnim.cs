using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingSpriteAnim : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float timeBetweenSprites = 0.5f;

    private SpriteRenderer _spriteRenderer;
    

    private float _timePassed = 0f;
    private float _nextSpriteTime = 0f;
    private int _currentSpriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _nextSpriteTime = timeBetweenSprites;
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed >= _nextSpriteTime)
        {
            _timePassed = 0f;
            _nextSpriteTime = _timePassed + timeBetweenSprites;
            _currentSpriteIndex++;
            if (_currentSpriteIndex >= sprites.Length)
            {
                _currentSpriteIndex = 0;
            }
            _spriteRenderer.sprite = sprites[_currentSpriteIndex];
        }
    }
}
