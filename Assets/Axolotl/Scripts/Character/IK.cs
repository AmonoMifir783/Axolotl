using UnityEngine;

public class IK : MonoBehaviour
{
    public Transform[] FootTarget;
    public LayerMask layerToHit; 

    public void LateUpdate()
    {

        for(int i = 0; i < FootTarget.Length; i++)
        {
            var foot = FootTarget[i];
            var ray = new Ray(foot.transform.position + Vector3.down * 0.5f, Vector3.up);
            RaycastHit hit;

           // if(Physics.SphereCast(ray, 1f, out hitInfo, 0.5f))
            Vector3 direction = Vector3.down; // Direction of the ray

            if (Physics.Raycast(ray, out hit, 1.4f, layerToHit))
            //if(Physics.SphereCast(ray, 1f, out hit, 0.5f, layerToHit))
            {
                foot.position = hit.point + Vector3.up * 0.22f;
                Debug.Log("Hit: " + hit.transform.name);
                Debug.DrawRay(foot.transform.position, Vector3.down * 10f, Color.red, layerToHit);
            }
        }
    }
}
