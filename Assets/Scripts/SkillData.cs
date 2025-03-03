using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private SkillMesh mesh;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string title;
    [SerializeField] private string description;

    public SkillMesh Mesh => mesh;
    public Sprite Sprite => sprite;

    public string HelpText => $"<b>{title}</b>\n\n{description}";
}
