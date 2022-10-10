using UnityEngine;
using System.Linq;

namespace Pathfinding
{
    /// <summary>
    /// Moves the target in example scenes.
    /// This is a simple script which has the sole purpose
    /// of moving the target point of agents in the example
    /// scenes for the A* Pathfinding Project.
    ///
    /// It is not meant to be pretty, but it does the job.
    /// </summary>
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
    public class TargetMover : MonoBehaviour
    {
        /// <summary>Mask for the raycast placement</summary>
        public LayerMask mask;

        public Transform target;
        IAstarAI[] ais;
        Camera cam;

        private RaycastHit2D _soldierHit;

        public void Start()
        {
            //Cache the Main Camera
            cam = Camera.main;
            // Slightly inefficient way of finding all AIs, but this is just an example script, so it doesn't matter much.
            // FindObjectsOfType does not support interfaces unfortunately.
            ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
            useGUILayout = false;
        }


        /// <summary>Update is called once per frame</summary>
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _soldierHit = Physics2D.Raycast(cam.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, mask);
                if (_soldierHit.collider != null)//seçilen yerde soldier olmamasý için kontrol yapýyor
                {
                    return;
                }
                UpdateTargetPosition();
            }
        }

        public void UpdateTargetPosition()
        {
            Vector3 newPosition = Vector3.zero;
            bool positionFound = false;

            newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            positionFound = true;

            if (positionFound && newPosition != target.position)
            {
                target.position = newPosition;
                for (int i = 0; i < ais.Length; i++)
                {
                    if (ais[i] != null) ais[i].SearchPath();
                }
            }
        }
    }
}
