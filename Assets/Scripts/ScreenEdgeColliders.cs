using System;
using UnityEngine;

namespace Scripts
{
    public class ScreenEdgeColliders : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _bottomEdge;
        [SerializeField] private BoxCollider2D _topEdge;
        [SerializeField] private BoxCollider2D _leftEdge;
        [SerializeField] private BoxCollider2D _rightEdge;
        
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            
            UpdateEdges();
        }
        
        void UpdateEdges()
        {
            float height = 2f * _camera.orthographicSize;
            float width = height * _camera.aspect;

            Vector3 camPos = _camera.transform.position;
            
            _topEdge.transform.position = new Vector3(camPos.x, camPos.y + height / 2f + _topEdge.transform.localScale.y / 2f, 0);
            
            _bottomEdge.transform.position = new Vector3(camPos.x, camPos.y - height / 2f - _bottomEdge.transform.localScale.y / 2f, 0);
            _leftEdge.transform.position = new Vector3(camPos.x - width / 2f - _leftEdge.transform.localScale.x / 2f, camPos.y, 0);
            _rightEdge.transform.position = new Vector3(camPos.x + width / 2f + _rightEdge.transform.localScale.x / 2f, camPos.y, 0);

            _topEdge.transform.localScale = new Vector2(width- width*0.2f, _topEdge.transform.localScale.y);
        }
    }
}