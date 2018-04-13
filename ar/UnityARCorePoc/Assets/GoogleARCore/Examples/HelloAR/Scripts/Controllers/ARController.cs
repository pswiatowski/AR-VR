using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARController : MonoBehaviour {

    // The first-person camera being used to render the passthrough camera image (i.e. AR background).
    public Camera FirstPersonCamera;

    // A prefab for tracking and visualizing detected planes.
    public GameObject TrackedPlanePrefab;

    // Ball prefab
    public GameObject ballPrefab;

    // Cloth component of the basketball hoop
    public Cloth cloth;

    // A gameobject parenting UI for displaying the "searching for planes" snackbar.
    public GameObject SearchingForPlaneUI;

    // A list to hold new planes ARCore began tracking in the current frame. This object is used across
    // the application to avoid per-frame allocations.
    private List<TrackedPlane> m_NewPlanes = new List<TrackedPlane>();

    // A list to hold all planes ARCore is tracking in the current frame. This object is used across
    // the application to avoid per-frame allocations.
    private List<TrackedPlane> m_AllPlanes = new List<TrackedPlane>();

    // True if the app is in the process of quitting due to an ARCore connection error, otherwise false.
    private bool m_IsQuitting = false;

    void Start() {
        _QuitOnConnectionErrors();
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        _QuitOnConnectionErrors();

        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking) {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            if (!m_IsQuitting && Session.Status.IsValid()) {
                SearchingForPlaneUI.SetActive(true);
            }

            return;
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
        Session.GetTrackables<TrackedPlane>(m_NewPlanes, TrackableQueryFilter.New);
        for (int i = 0; i < m_NewPlanes.Count; i++) {
            // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
            // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
            // coordinates.
            GameObject planeObject = Instantiate(TrackedPlanePrefab, Vector3.zero, Quaternion.identity,
                transform);
            planeObject.GetComponent<GoogleARCore.HelloAR.TrackedPlaneVisualizer>().Initialize(m_NewPlanes[i]);
        }

        // Disable the snackbar UI when no planes are valid.
        Session.GetTrackables<TrackedPlane>(m_AllPlanes);
        bool showSearchingUI = true;
        for (int i = 0; i < m_AllPlanes.Count; i++) {
            if (m_AllPlanes[i].TrackingState == TrackingState.Tracking) {
                showSearchingUI = false;
                break;
            }
        }

        SearchingForPlaneUI.SetActive(showSearchingUI);


        if (SwipeController.Instance.IsSwiping(SwipeDirection.Up)) {

            GameObject instance = Instantiate(ballPrefab);
            instance.transform.position = FirstPersonCamera.transform.TransformPoint(0, 0, 0.5f);
            Rigidbody ball = instance.GetComponent<Rigidbody>();

            // Set the cloth collider to enable collisions with the ball
            SphereCollider collider = instance.GetComponent<SphereCollider>();
            cloth.sphereColliders = new ClothSphereColliderPair[] { new ClothSphereColliderPair(collider) };

            // Add torque to the ball 
            float randomX = Random.Range(10f, 100f);
            //float randomY = Random.Range(10f, 100f);
            //float randomZ = Random.Range(10f, 100f);
            ball.AddTorque(randomX, 0, 0);

            // Add force to the ball 
            float x = (SwipeController.Instance.lastTouch.x - SwipeController.Instance.firstTouch.x) / Screen.height * 34f;
            float y = (SwipeController.Instance.lastTouch.y - SwipeController.Instance.firstTouch.y) / Screen.height * 34f;
            ball.AddForce(FirstPersonCamera.transform.TransformDirection(x, y, 15) * 0.3f, ForceMode.Impulse);

            Destroy(instance, 6);
        }
    }

    // Quit the application if there was a connection error for the ARCore session.
    private void _QuitOnConnectionErrors() {
        if (m_IsQuitting) {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted) {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        } else if (Session.Status.IsError()) {
            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    // Actually quit the application.
    private void _DoQuit() {
        Application.Quit();
    }


    // Show an Android toast message.
    private void _ShowAndroidToastMessage(string message) {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null) {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
