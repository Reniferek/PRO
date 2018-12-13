using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GravityGun : MonoBehaviour {

    // Settings
    public float hoverSmoothing = 0.99f;
    public float scrollWheelMultiplier = 1f;

    // Private settings
    private int buttonIdx = 1; // you want right button for right-handed people, as we're also using scroll wheel with the index finger

    // Runtime data
    private bool isHovering;
    private Transform hoveredObject;
    private float hoverDistanceFromCamera;

    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update ()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(mouseRay, out hitInfo);

        UpdateHovering(mouseRay, isHit, hitInfo);
    }

    private void UpdateHovering(Ray mouseRay, bool isHit, RaycastHit hitInfo)
    {
        if (Input.GetMouseButtonDown(buttonIdx) && isHit)
        {
            // Check if it is a rigidbody
            Rigidbody rigidbody = hitInfo.collider.GetComponentInParent<Rigidbody>();

            // Start hovering
            if (rigidbody)
            {
                hoveredObject = rigidbody.transform;
                hoverDistanceFromCamera = Vector3.Distance(hoveredObject.position, Camera.main.transform.position);

                isHovering = true;
            }
        }

        if (isHovering)
        {
            // Gravitate front/back
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            hoverDistanceFromCamera += scrollDelta * scrollWheelMultiplier;

            // Update hovering
            Vector3 positionTarget = mouseRay.GetPoint(hoverDistanceFromCamera);

            float interpolationParam = Math_SmoothingToInterpolationParam(hoverSmoothing, Time.deltaTime);
            positionTarget = Vector3.Lerp(hoveredObject.position, positionTarget, interpolationParam);

            // Zero body velocity
            Rigidbody rigidbody = hoveredObject.GetComponentInParent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.velocity = (positionTarget - hoveredObject.position) / Time.deltaTime;
            }
            else
            {
                hoveredObject.position = positionTarget;
            }
        }

        if (Input.GetMouseButtonUp(buttonIdx) && isHovering)
        {
            // End hovering

            isHovering = false;
        }

    }

    public static float Math_SmoothingToInterpolationParam(float smoothing, float dt)
    {
        return 1f - Mathf.Pow(1f - smoothing, dt);
    }
}
