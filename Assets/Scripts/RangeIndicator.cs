using UnityEngine;

namespace UndeadAssault
{
    [RequireComponent(typeof(LineRenderer))]
    public class RangeIndicator : MonoBehaviour
    {
        public float radius = 2f;
        public float lineWidth = 0.1f;
        public int segments = 128;

        private LineRenderer _lineRenderer;

        void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            DrawSegments();
        }

        public void DrawSegments()
        {
            _lineRenderer.positionCount = segments;
            _lineRenderer.widthMultiplier = lineWidth;
            _lineRenderer.loop = true;
            float theta = 360f / segments;
            float angle = 0;
            for (int i = 0; i < segments; i++)
            {
                float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
                float z = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
                Vector3 pos = new Vector3(x, 0, z);
                _lineRenderer.SetPosition(i, pos);
                angle += theta;
            }
        }

        public void OnValidate()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();
            DrawSegments();
        }
    }
}
