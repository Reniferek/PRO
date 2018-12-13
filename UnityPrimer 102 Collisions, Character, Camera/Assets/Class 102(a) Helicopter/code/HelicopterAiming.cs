using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAiming : MonoBehaviour {

    public UnityEngine.UI.Text label;

    private Tank[] allTanks = new Tank[0];
    private HashSet<Tank> lockedTanks = new HashSet<Tank>();

	// Use this for initialization
	void Start () {
        allTanks = GameObject.FindObjectsOfType<Tank>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        allTanks = GameObject.FindObjectsOfType<Tank>();


        float smallestAngle = float.MaxValue;
        float horizontalAngle = float.MaxValue;
        lockedTanks.Clear();
		foreach(Tank tank in allTanks)
        {
            Vector3 heliToTank = tank.transform.position - this.transform.position;
            float targetDotForward = Vector3.Dot(heliToTank.normalized, -this.transform.right);
            float angleFromForward = Mathf.Abs(Mathf.Acos(targetDotForward)) * Mathf.Rad2Deg;
            if (angleFromForward < 60f)
            {
                lockedTanks.Add(tank);
                smallestAngle = angleFromForward;

                // Calc horizontal angle

                // project both our vectors onto horizontal plane
                heliToTank = heliToTank - Vector3.Dot(heliToTank, Vector3.up) * Vector3.up;
                Vector3 dirHorizontal = -this.transform.right;
                dirHorizontal.y = 0f;

                float dotHorizontal = Vector3.Dot(heliToTank.normalized, dirHorizontal.normalized);
                horizontalAngle = Mathf.Abs(Mathf.Acos(dotHorizontal)) * Mathf.Rad2Deg;
            }
        }

        if (label)
        {
            if (lockedTanks.Count > 0)
            {
                label.text = "Tank locked at angle " + smallestAngle + "(Horizontal angle " + horizontalAngle + ")";
            }
            else
            {
                label.text = "Searching for targets";
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Tank t in allTanks)
        {
            if (!lockedTanks.Contains(t))
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(this.transform.position, t.transform.position);
            }
        }

        foreach (Tank t in lockedTanks)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.transform.position, 2f);
            Gizmos.DrawLine(this.transform.position, t.transform.position);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, this.transform.position - 3f * this.transform.right);
    }
}
