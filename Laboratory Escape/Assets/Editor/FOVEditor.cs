using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        AIFOV fov = (AIFOV)target; // AIFOV를 가져옴

        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f); // 원을 그리자!

        //Handles.color = Color.white;

        Handles.color = new Color(1, 1, 1, 0.2f);

        Handles.DrawWireDisc(fov.transform.position
                             , Vector3.up
                             , fov.viewRange);

        Handles.DrawSolidArc(fov.transform.position
                             , Vector3.up
                             , fromAnglePos
                             , fov.viewAngle
                             , fov.viewRange);

        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f)
                      , fov.viewAngle.ToString());
    }
}
