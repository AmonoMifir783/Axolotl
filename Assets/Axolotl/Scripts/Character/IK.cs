using UnityEngine;

public class IK : MonoBehaviour
{
    public Transform[] FootTarget;

    public void LateUpdate()
    {
        for(int i = 0; i < FootTarget.Length; i++)
        {
            var foot = FootTarget[i];
            var ray = new Ray(foot.transform.position + Vector3.up * 0.5f, Vector3.down);
            var hitInfo = new RaycastHit();
            if(Physics.SphereCast(ray, 0.05f, out hitInfo, 0.50f))
                foot.position = hitInfo.point + Vector3.up * 0.05f;
        }
    }
}
