using UnityEngine;

public class gizmo : MonoBehaviour
{
    public Vector3 scale;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(Color.cyan, Color.clear, 0.5f);//set gizmo color
        Gizmos.DrawCube(Vector3.up * scale.y / 2f, scale);//draw the gizmo
        Gizmos.color = Color.cyan;//change color
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * 0.4f);//draw a line
    }
}