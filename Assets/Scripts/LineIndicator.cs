using UnityEngine;

namespace UndeadAssault
{
    public class LineIndicator : MonoBehaviour
    {
        public float range = 2f;
        public float width = 0.1f;

        public LineRenderer line1Renderer;
        public LineRenderer line2Renderer;

        public void DrawLine()
        {
            line1Renderer.positionCount = 2;
            line1Renderer.widthMultiplier = 0.1f;
            line1Renderer.SetPosition(0, new Vector3(width, 0, 0));
            line1Renderer.SetPosition(1, new Vector3(width, 0, range));

            line2Renderer.positionCount = 2;
            line2Renderer.widthMultiplier = 0.1f;
            line2Renderer.SetPosition(0, new Vector3(-width, 0, 0));
            line2Renderer.SetPosition(1, new Vector3(-width, 0, range));
        }

        public void OnValidate()
        {
            DrawLine();
        }
    }
}
