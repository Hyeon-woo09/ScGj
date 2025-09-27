using UnityEngine;

public class ColliderGroup : MonoBehaviour
{
    [Header("그룹에 속한 Collider들")]
    public Collider[] colliders;

    // 그룹 안 모든 콜라이더가 카메라에 보이는지 확인
    public bool IsAllVisible(Camera cam)
    {
        foreach (var col in colliders)
        {
            if (!IsColliderVisible(cam, col))
                return false; // 하나라도 안 보이면 false
        }
        return true; // 전부 보이면 true
    }

    private bool IsColliderVisible(Camera cam, Collider col)
    {
        if (col == null) return false;

        Bounds bounds = col.bounds;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}
