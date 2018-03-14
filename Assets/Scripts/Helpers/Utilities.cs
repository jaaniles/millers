using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {
	public static Transform GetClosest(Collider[] colliders, Vector3 pos)
	{
		Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider potentialTarget in colliders)
        {
            GameObject potentialGameObject = potentialTarget.gameObject;

            if (!potentialGameObject.activeSelf)
                continue;
            

            Vector3 directionToTarget = potentialTarget.transform.position - pos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTarget = potentialGameObject.transform;
            }
        }

        return closestTarget;
	}
}
