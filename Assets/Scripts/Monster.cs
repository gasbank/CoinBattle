using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform meshTransform;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private int hp = 1000;
    [SerializeField] private TextMeshPro monsterNameText;
    
    private Vector2 targetEuler;
    private Vector2 targetEulerVel;
    private CancellationTokenSource attackCts;

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

    private void OnDestroy()
    {
        attackCts?.Dispose();
    }

    public async Task ApplyDamageAsync(int amount)
    {
        monsterNameText.color = Globals.I.EnemyColor;
        
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (attackCts is { IsCancellationRequested: false })
        {
            attackCts.Cancel();
        }

        attackCts = new CancellationTokenSource();

        _ = AttackPlayerAsync(attackCts.Token);
    }

    private async Task<bool> AttackPlayerAsync(CancellationToken ct)
    {
        var vel = Vector3.zero;
        var attackTargetPos = Globals.I.WorldCam.transform.position + Globals.I.WorldCam.transform.forward * 4;
        while (Application.isPlaying && Vector3.Distance(meshTransform.position, attackTargetPos) > Vector3.kEpsilon)
        {
            meshTransform.position = Vector3.SmoothDamp(meshTransform.position, attackTargetPos, ref vel, 0.2f);
            ct.ThrowIfCancellationRequested();
            await Task.Yield();
        }
        
        vel = Vector3.zero;
        while (Application.isPlaying && Vector3.Distance(meshTransform.localPosition, Vector3.zero) > Vector3.kEpsilon)
        {
            meshTransform.localPosition = Vector3.SmoothDamp(meshTransform.localPosition, Vector3.zero, ref vel, 0.2f);
            ct.ThrowIfCancellationRequested();
            await Task.Yield();
        }

        meshTransform.localPosition = Vector3.zero;
        
        return true;
    }
}
