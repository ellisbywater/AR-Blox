using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject[] blocks;
    private ARRaycastManager _raycastManager;

    [SerializeField] LayerMask blockLayer;

    private int selectedBlock;
    // Start is called before the first frame update
    void Start()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }
    
    public void OnBuildButtonPressed()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f,.5f));
        if (Physics.Raycast(rayToCast, out hitInfo, 200f, blockLayer))
        {
            Vector3 buildablePosition = hitInfo.normal + hitInfo.transform.position;
            Quaternion buildabRotation = hitInfo.transform.rotation;
            Build(buildablePosition, buildabRotation);
        }
        else
        {
            _raycastManager.Raycast(rayToCast, hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                Vector3 buildablePositon = new Vector3(Mathf.Round(hits[0].pose.position.x / 1) * 1, hits[0].pose.position.y, Mathf.Round(hits[0].pose.position.z / 1) * 1);
                Quaternion buildableRotation = hits[0].pose.rotation;
                Build(buildablePositon, buildableRotation);
            }
        }
    }

    public void SelectBlock(int blockID)
    {
        selectedBlock = blockID;
    }

    public void OnDestroyButtonPressed()
    {
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f,.5f));
        if (Physics.Raycast(rayToCast, out hitInfo, 200f, blockLayer))
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }

    void Build(Vector3 position, Quaternion rotation)
    {
        Instantiate(blocks[selectedBlock], position, rotation);
    }
}
