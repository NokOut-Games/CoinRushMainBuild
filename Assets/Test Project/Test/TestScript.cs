using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public List<Transform> cubes;
    private void Update()
    {
        transform.position = FindCenterOfTransforms(cubes);    
    }

    public Vector3 FindCenterOfTransforms(List<Transform> transforms)
    {
        Bounds bound = new Bounds(transforms[0].position, Vector3.zero);
        for (int i = 1; i < transforms.Count; i++)
        {
            bound.Encapsulate(transforms[i].position);
        }
        return bound.center;
    }
}

