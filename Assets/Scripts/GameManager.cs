using BackEnd;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera MainCamera = null;

    private float _Time = 0.0f;
    private float _TimeRemaining = 3.0f;

    private static GameManager _Instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameManager>();
            }

            return _Instance;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Rect rect = MainCamera.rect;

        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }

        MainCamera.rect = rect;

        Backend.Initialize();

#if UNITY_ANDROID
        Debug.Log("구글 해시 : " + Backend.Utils.GetGoogleHash());
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Time += Time.deltaTime;
        if (_Time > _TimeRemaining)
        {
            _Time = 0.0f;
        }
    }

}
