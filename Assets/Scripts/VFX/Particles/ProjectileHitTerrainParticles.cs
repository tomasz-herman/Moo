using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitTerrainParticles : BurstParticles
{
    public Color SparkColor
    { 
        set
        {
            var colorOverLifetime = GetComponent<ParticleSystem>().colorOverLifetime;
            var oldGradient = colorOverLifetime.color.gradient;

            //For some reason you cannot edit Gradient's colorKeys[index] directly and you need to do this instead...
            //This won't work:
            //oldGradient.colorKeys[1].color = value;
            //colorOverLifetime.color = new Gradient() { alphaKeys = oldGradient.alphaKeys, colorKeys = oldGradient.colorKeys };

            var newColors = oldGradient.colorKeys;
            newColors[1].color = value;

            colorOverLifetime.color = new Gradient() { alphaKeys = oldGradient.alphaKeys, colorKeys = newColors };
        }
    }
}
