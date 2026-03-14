using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorable : MonoBehaviour
{
    public int scoreValue = 100;
    public float velocityThreshold = 5f;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {

            float impactForce = collision.relativeVelocity.magnitude;

            if (impactForce > velocityThreshold)
            {

                int pointsEarned = Mathf.RoundToInt(scoreValue * impactForce);
                MissionDemolition.ADD_SCORE(pointsEarned);
            }
        }
    }
}
