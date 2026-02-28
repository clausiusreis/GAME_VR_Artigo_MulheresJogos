using UnityEngine;

public class MagneticRelease : MonoBehaviour
{
    [Header("Referências")]
    public Transform centerEyeAnchor;  // CenterEyeAnchor do rig
    public GameObject obj2;            // Alvo
    public float moveSpeed = 5f;
    public float snapDistance = 0.1f;

    private bool isMoving = false;
    private GameObject currentObj1;

    void Update()
    {
        if (isMoving && currentObj1 != null)
        {
            currentObj1.transform.position = Vector3.MoveTowards(
                currentObj1.transform.position,
                obj2.transform.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(currentObj1.transform.position, obj2.transform.position) < snapDistance)
            {
                isMoving = false;
                currentObj1.transform.position = obj2.transform.position;
            }
        }
    }

    public void OnReleased(GameObject releasedObj)
    {
        currentObj1 = releasedObj;

        if (IsBetweenEyeAndTarget(releasedObj))
        {
            isMoving = true;
        }
    }

    private bool IsBetweenEyeAndTarget(GameObject obj1)
    {
        Vector3 eyePos = centerEyeAnchor.position;
        Vector3 eyeForward = centerEyeAnchor.forward;
        Vector3 obj2Center = obj2.GetComponent<Collider>().bounds.center;

        Vector3 dirToTarget = (obj2Center - eyePos).normalized;

        float angle = Vector3.Angle(eyeForward, dirToTarget);
        if (angle > 10f) return false;

        float distEyeToTarget = Vector3.Distance(eyePos, obj2Center);
        float distEyeToObj = Vector3.Distance(eyePos, obj1.transform.position);
        if (distEyeToObj > distEyeToTarget) return false;

        Ray ray = new Ray(eyePos, dirToTarget);
        return obj2.GetComponent<Collider>().bounds.IntersectRay(ray);
    }
}
