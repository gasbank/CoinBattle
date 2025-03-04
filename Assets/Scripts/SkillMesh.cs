using UnityEngine;

public class SkillMesh : MonoBehaviour
{
    [SerializeField] private float approachSmoothTime = 0.2f;
    public Monster Target { get; set; }

    private Vector3 vel;

    private void Update()
    {
        if (Target)
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, Target.transform.position, ref vel, approachSmoothTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);

        if (other.TryGetComponent<Monster>(out var monster))
        {
            _ = monster.ApplyDamageAsync(100);
            Destroy(gameObject);
        }
    }
}
