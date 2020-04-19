using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerPanel : MonoBehaviour
{

    public Vector2 Size;
    [Range(2, 10)]
    public int Difficulty;

    private Rect bounds;
    private Vector2 targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetObjective(float radius) {

        bounds = new Rect(transform.position, Size);

        // Get a random point within a rectangle.
        targetPoint = new Vector2(Random.Range(bounds.xMax, bounds.xMin), Random.Range(bounds.xMin, bounds.xMax));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
