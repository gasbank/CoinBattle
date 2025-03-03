using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform meshTransform;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private int hp = 1000;
    
    private Vector2 targetEuler;
    private Vector2 targetEulerVel;

    private IEnumerator Start()
    {
        while (Application.isPlaying)
        {
            targetEuler.x = Random.Range(-30.0f, 5.0f);
            targetEuler.y = Random.Range(-30.0f, 30.0f);
            
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }
    
    private void Update()
    {
        var meshEulerAngles = meshTransform.eulerAngles;
        meshEulerAngles.x = Mathf.SmoothDampAngle(meshEulerAngles.x, targetEuler.x, ref targetEulerVel.x, smoothTime);
        meshEulerAngles.y = Mathf.SmoothDampAngle(meshEulerAngles.y, targetEuler.y, ref targetEulerVel.y, smoothTime);
        meshTransform.eulerAngles = meshEulerAngles;
    }

    public void ApplyDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
