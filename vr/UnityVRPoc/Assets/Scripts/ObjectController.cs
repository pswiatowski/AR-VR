using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ObjectController : MonoBehaviour {

    public GameObject messageCanvas;
	public GameObject humanRenderMesh;
    public Text header;
    public Text body;

    public string title;
    public string description;
	public bool rotate;
	public Renderer ren;
	public Material[] mat;
	public ColorPicker picker;
	public Color Color;
	public Transform camTr;
	public Transform shoe;
	public Vector3 firstPosition;
	public Quaternion shoeRotation;
	public Light Lightcomponent;
	public bool moved;

	public Vector3 camPos;


	// Use this for initialization
	void Start () {
        // Set canvas object to deactivate
        messageCanvas.SetActive(false);

		picker.onValueChanged.AddListener(color =>
			{
						Color = color;
						Debug.Log (color);
			});
		
		shoeRotation = transform.rotation;
		firstPosition = camTr.position;
		camPos = camTr.position;
	}
	
	// Update is called once per frame
	void Update () {
        // Update canvas rotation only if canvas is active
        if (messageCanvas.activeSelf) {
            // Canvas rotation
            var canvasRotation = messageCanvas.transform.rotation;
            canvasRotation.y = Camera.main.transform.rotation.y;
            messageCanvas.transform.rotation = canvasRotation;
        }
		if (rotate) {
			if (transform.parent.rotation.x != 0) {
				transform.Rotate (new Vector3 (0, 0, Time.deltaTime * 30));
			} else {
				transform.Rotate (new Vector3 (0, Time.deltaTime * 30, 0));
			}
		} else {
			transform.Rotate (new Vector3 (0, 0, 0));
		}


	}

    public void ShowMessage() {
        // Set header and body text
        header.text = title;
        body.text = description;

        // Canvas position
        var canvasPosition = messageCanvas.transform.position;
        canvasPosition.x = transform.position.x;
        messageCanvas.transform.position = canvasPosition;

        // Set canvas object to active
        messageCanvas.SetActive(true);

		ren = GetComponent<Renderer> ();
		mat = ren.materials;
		for (int i = 0; i < mat.Length; i++) {
			if (mat [i].name.Contains("UpperOut")){
				mat [i].SetColor("_Color",Color);
			} 
		}
		Debug.Log (mat [0].name);

		camPos = new Vector3 (shoe.position.x, shoe.position.y+(float)0.4, shoe.position.z-(float)0.4);


		rotate = true;
		camTr.position = camPos;
		moved = true;

		Lightcomponent.enabled = true;
		Lightcomponent.transform.position = new Vector3 (shoe.position.x, shoe.position.y + (float)0.4, shoe.position.z);


    } 

    public void HideMessage() {
        // Set canvas object to deactivate
		messageCanvas.SetActive(false);
		transform.rotation = shoeRotation;
		rotate = false;
    }
}
