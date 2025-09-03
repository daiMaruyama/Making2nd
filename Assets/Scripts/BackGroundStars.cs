using UnityEngine;

public class BackgroundStars : MonoBehaviour
{
    public ParticleSystem starParticle;

    void Start()
    {
        if (starParticle != null)
        {
            starParticle.Play();
        }
    }
}
