using UnityEngine;

public class ParallaxController : MonoBehaviour
{

    Transform cam; //main camera
    Vector3 camStartPos;
    float distance; //distance between the camera start position and its current position

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;
    
    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i= 0; i < backCount; i++) //find the farthest background
        {
            if((backgrounds[i].transform.position.z-cam.position.z) > farthestBack)
            {
               farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) //set the speed of backgrounds
        {
            backSpeed[i]=1-(backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }


    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for(int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;

// public class ParralaxController : MonoBehaviour
// {
//     private Transform _cameraTransform;
//     private Vector3 _lastCamPos;
//     private float _textureSizeX;

//     [SerializeField] private Vector2 parallaxEffectMultiplier;

//     private void Awake()
//     {
//         _cameraTransform = Camera.main.transform;
//         _lastCamPos = _cameraTransform.position;

//         Sprite sprite = GetComponent<SpriteRenderer>().sprite;
//         Texture2D texture = sprite.texture;
//         _textureSizeX = texture.width / sprite.pixelsPerUnit;
//     }

//     private void FixedUpdate()
//     {
//         ParallaxMove();
//         RepeatBackground();
//     }

//     private void ParallaxMove()
//     {
//         Vector3 deltaMovment = _cameraTransform.position - _lastCamPos;
//         transform.position += new Vector3(
//                 deltaMovment.x * parallaxEffectMultiplier.x,
//                 deltaMovment.y * parallaxEffectMultiplier.y
//             );
//         _lastCamPos = _cameraTransform.position;
//     }

//     private void RepeatBackground()
//     {
//         if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureSizeX)
//         {
//             float offsetPosX = (_cameraTransform.position.x - transform.position.x) % _textureSizeX;
//             transform.position = new(_cameraTransform.position.x + offsetPosX, transform.position.y);
//         }
//     }


// }