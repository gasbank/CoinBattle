using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private SkillData skillData;
    [SerializeField] private Image image;
    [SerializeField] private Camera worldCam;
    [SerializeField] private Camera interfaceCam;
    [SerializeField] private Monster testMonster;
    [SerializeField] private TextMeshProUGUI helpText;

    private void Start()
    {
        image.sprite = skillData.Sprite;
    }

    [UsedImplicitly]
    public void OnClick()
    {
        if (testMonster == null)
        {
            return;
        }
        
        var imageCorners = new Vector3[4];
        image.rectTransform.GetWorldCorners(imageCorners);
        var imageCenter = (imageCorners[0] + imageCorners[2]) / 2;

        var screenPoint = RectTransformUtility.WorldToScreenPoint(interfaceCam, imageCenter);

        var worldPoint = worldCam.ScreenToWorldPoint(screenPoint);
        //worldPoint.z = 0;
        var skillMesh = Instantiate(skillData.Mesh, worldPoint, Quaternion.identity);
        skillMesh.Target = testMonster;

        helpText.text = skillData.HelpText;
    }
}
