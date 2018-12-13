using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    // Constants
    static string IgnoreRaycastLayer = "Ignore Raycast";

    // Settings 
    public GameObject decalPrefab;
    public GameObject sparksPrefab;

    // Private settings
    private int buttonIdx = 0;

    // Runtime state
    private LineRenderer lineRenderer;
    private Transform sparksInstance;
    private AudioSource audioSource;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(mouseRay, out hitInfo);

        if (Input.GetMouseButton(buttonIdx) && isHit && decalPrefab)
        {
            ApplyDecal(decalPrefab, hitInfo.collider.transform, hitInfo.point, hitInfo.normal);
        }

        if (lineRenderer)
        {
            if (Input.GetMouseButton(buttonIdx))
            {
                //Vector3 farPoint = isHit ? hitInfo.point : mouseRay.GetPoint(100f);
                Vector3 farPoint = isHit ? mouseRay.GetPoint(hitInfo.distance - 0.1f) : mouseRay.GetPoint(100f);
                lineRenderer.SetPositions(new Vector3[] {
                    Camera.main.transform.position + Camera.main.transform.right - 0.2f * Camera.main.transform.up,
                    farPoint
                });
                lineRenderer.positionCount = 2;

                // Update texture offset to create a feeling of a laser
                GetComponent<Renderer>().sharedMaterial.mainTextureOffset -= new Vector2(1f * Time.deltaTime, 0f);

                if (sparksPrefab)
                {
                    if (!sparksInstance)
                    {
                        // Create particle system
                        sparksInstance = GameObject.Instantiate(sparksPrefab, farPoint, Quaternion.identity).transform;
                    }
                    else
                    {
                        sparksInstance.position = farPoint;
                    }
                }

                if (audioSource && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (sparksInstance)
                {
                    Destroy(sparksInstance.gameObject);
                }

                lineRenderer.positionCount = 0;

                if (audioSource && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
        else
        {
            Debug.Log("Missing LineRenderer in DecalApplicator on object " + this.name);
        }

    }

    public static void ApplyDecal(GameObject decalPrefab, Transform parent, Vector3 position, Vector3 normal)
    {
        // Apply decal at point of hit
        position += normal * 0.01f;

        Quaternion rotation = Quaternion.LookRotation(-normal);
        //Quaternion rotation = Quaternion.FromToRotation(Vector3.back, hitInfo.normal);

        // Preserve scale
        Transform newDecal = GameObject.Instantiate(decalPrefab, position, rotation).transform;
        newDecal.parent = parent;

        newDecal.gameObject.layer = LayerMask.NameToLayer(IgnoreRaycastLayer);

        Rigidbody rigidbody = parent.GetComponentInParent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.AddForceAtPosition(-normal * 1f, position);
        }
    }


}
