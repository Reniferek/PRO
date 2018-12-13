using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SlingShot : MonoBehaviour
    {
        public float springConstant = 1f;

        [Header("Debug Settings")]
        public bool debugDrawMouseRay = true;

        // Runtime state
        private Vector3 positionOnMouseDown;
        private bool isMouseDown;

        private Vector3 mousePositionInLocal;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isMouseDown)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;

                Vector3 mousePoint = CalcControlPoint();

                transform.position = mousePoint - mousePositionInLocal;

                if (Input.GetMouseButtonDown(1))
                {
                    // Record slingshot start
                    mousePoint = CalcControlPoint();
                }
            }
        }

        private void OnMouseDown()
        {
            positionOnMouseDown = transform.position;
            isMouseDown = true;


            //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //RaycastHit hitInfo;
            //float maxDistance = Mathf.Infinity;
            //bool wasHit = GetComponent<Collider>().Raycast(mouseRay, out hitInfo, maxDistance);

            //Debug.Assert(wasHit, "Mouse raycast did not hit the collider from OnMouseDown event. Engine inconsistency. Please, debug.");

            Vector3 mousePoint = CalcControlPoint();

            mousePositionInLocal = mousePoint - transform.position;
            mousePositionInLocal.z = 0f;
        }

        private void OnMouseUp()
        {
            // Apply velocity based on displacement
            if (Input.GetMouseButton(1))
            {
                Vector3 toOrigin = positionOnMouseDown - transform.position;
                GetComponent<Rigidbody>().velocity = toOrigin * springConstant;
            }
            isMouseDown = false;
        }

        private Vector3 CalcControlPoint()
        {

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane plane = new Plane(-Camera.main.transform.forward, positionOnMouseDown);
            Plane plane = new Plane(-Vector3.forward, positionOnMouseDown);

            float enter;
            bool wasHit = plane.Raycast(mouseRay, out enter);

            Vector3 mousePoint = mouseRay.GetPoint(enter);

            Debug.Assert(wasHit, "Mouse raycast did not hit the collider from OnMouseDown event. Engine inconsistency. Please, debug.");

            return mousePoint;
        }

        private void OnDrawGizmos()
        {
            if (enabled && gameObject.activeInHierarchy)
            {
                Vector3 controlPoint = CalcControlPoint();

                if (debugDrawMouseRay)
                {
                    Gizmos.color = Color.white;
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(Camera.main.transform.position, 0.5f);
                    Gizmos.DrawLine(Camera.main.transform.position, controlPoint);
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(controlPoint, 0.5f);
                }

                if (isMouseDown)
                {
                    Gizmos.color = Input.GetMouseButton(1) ? Color.red : Color.blue;
                    Gizmos.DrawLine(controlPoint, positionOnMouseDown);
                    Gizmos.DrawWireSphere(positionOnMouseDown, 0.5f);
                }
            }
        }
    }
}
