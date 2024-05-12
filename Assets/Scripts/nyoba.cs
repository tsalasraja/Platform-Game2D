using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class nyoba : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _lastCamPos;
    private float _textureSizeX;

    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _lastCamPos = _cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void FixedUpdate()
    {
        ParallaxMove();
        RepeatBackground();
    }

    private void ParallaxMove()
    {
        Vector3 deltaMovment = _cameraTransform.position - _lastCamPos;
        transform.position += new Vector3(
                deltaMovment.x * parallaxEffectMultiplier.x,
                deltaMovment.y * parallaxEffectMultiplier.y
            );
        _lastCamPos = _cameraTransform.position;
    }

    private void RepeatBackground()
    {
        if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureSizeX)
        {
            float offsetPosX = (_cameraTransform.position.x - transform.position.x) % _textureSizeX;
            transform.position = new(_cameraTransform.position.x + offsetPosX, transform.position.y);
        }
    }


}