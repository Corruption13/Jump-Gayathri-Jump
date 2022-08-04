using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowGV : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Follow Parameters")]
    public float speed = .1f;
    public Vector3 cameraYOffset;

    //private Camera thisCam;

    public Transform player;

    /*
    [Header("Zoom Settings")]
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 2f;
    public float sensetivity = 1f;

    private float total_points = 3;
    */
    private Vector3 lerpVector;
    private Vector3 target_pos;

    void Start()
    {
        //thisCam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 targetVector = player.transform.position + cameraYOffset;
        lerpVector = Vector3.Lerp(transform.position, targetVector, speed * Time.deltaTime);
        transform.position = new Vector3(lerpVector.x, lerpVector.y, transform.position.z);
        
    }
}
