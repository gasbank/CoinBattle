using UnityEngine;

[DisallowMultipleComponent]
public class Globals : MonoBehaviour
{
    public static Globals I { get; private set; }
    
    [SerializeField] private Color normalColor;
    [SerializeField] private Color enemyColor;
    [SerializeField] private Camera worldCam;
    [SerializeField] private Camera interfaceCam;

    public Color NormalColor => normalColor;
    public Color EnemyColor => enemyColor;
    public Camera WorldCam => worldCam;
    public Camera InterfaceCam => interfaceCam;
    
    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
