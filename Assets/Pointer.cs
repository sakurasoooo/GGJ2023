using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pointer : MonoBehaviour
{
    private List<Transform> targets;
    private Camera mainCamera;

    [SerializeField] private Aim aimPrefab;
    private void Awake()
    {
        targets = new List<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Left mouse button was clicked.");
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Debug.Log("Right mouse button was clicked.");
            if (targets.Count > 0)
            {
                Aim des = Instantiate<Aim>(aimPrefab);

                des.transform.position = GetMousePos();
                foreach (var target in targets)
                {
                    // des.count++;
                    if (target == null)
                    {
                        targets.Remove(target);
                    }
                    else
                    {
                        target.SendMessage("StartMove", des.transform, SendMessageOptions.DontRequireReceiver);

                    }
                }
            }
        }
    }

    public void AddTarget(Transform transform)
    {
        targets.Add(transform);
    }

    public void RemoveTarget(Transform transform)
    {
        targets.Remove(transform);
    }

    private Vector2 GetMousePos()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        // Debug.Log("Mouse position in world space: " + worldPosition);
        return worldPosition;
    }
}
