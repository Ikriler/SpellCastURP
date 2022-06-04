using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPossible : MonoBehaviour
{
    public void StableY(float y)
    {
        Vector3 stablePoint = transform.position;
        stablePoint.y = y;
        transform.position = stablePoint;
    }
    public IEnumerator ChangeColor(Color color)
    {
        for (int i = 0; i < 20; i++)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().sharedMaterial.color, color, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
