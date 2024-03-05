using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D myBox2D;
    private RectTransform myRectTransform;
    
    void Start()
    {
        myBox2D = GetComponent<BoxCollider2D>();
        myRectTransform = GetComponent<RectTransform>();

        myBox2D.size = new Vector2(myRectTransform.rect.width, myRectTransform.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
