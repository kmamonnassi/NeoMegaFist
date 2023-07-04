using UnityEngine;

[ExecuteInEditMode]
public class ParticleSystemRotateSynchronous : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float offset;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particle == null) return;
        var main = particle.main;
        main.startRotation = transform.localEulerAngles.z + offset;
    }
}