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
	public Quaternion shoeRotation;
	public Light Lightcomponent;

	public Vector3 camPos;


	// Use this for initialization
	void Start () {
        // Set canvas object to deactivate
        messageCanvas.SetActive(false);
		// We initiate the color picker
		picker.onValueChanged.AddListener(color =>
			{
						Color = color;
						Debug.Log (color);
			});
		// We get the default position of the shoe
		shoeRotation = transform.rotation;
		// We initiate the variable camPos with the default position of the camera
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
		// Set the rotation of the shoe (there's one shoe that is moved -90º so we need to rotate it in other axis)
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

    public void ShoeClicked() {
        // Set header and body text
        header.text = title;
        body.text = description;

        // Canvas position
        var canvasPosition = messageCanvas.transform.position;
        canvasPosition.x = transform.position.x;
        messageCanvas.transform.position = canvasPosition;

        // Set canvas object to active
        messageCanvas.SetActive(true);

		// Set the color of the UpperOut layer to the color selected in the picker
		ren = GetComponent<Renderer> ();
		mat = ren.materials;
		for (int i = 0; i < mat.Length; i++) {
			if (mat [i].name.Contains("UpperOut")){
				mat [i].SetColor("_Color",Color);
			} 
		}

		// Move the camera close to the selected shoe
		camPos = new Vector3 (shoe.position.x, shoe.position.y+(float)0.4, shoe.position.z-(float)0.4);
		camTr.position = camPos;

		// Set rotate to true
		rotate = true;

		// Enable a light to illuminate the selected shoe
		Lightcomponent.enabled = true;
		Lightcomponent.transform.position = new Vector3 (shoe.position.x, shoe.position.y + (float)0.4, shoe.position.z);
    } 

    public void ShoeUnfocused() {
        // Set canvas object to deactivate
		messageCanvas.SetActive(false);
		// Set the rotation to the default value
		transform.rotation = shoeRotation;
		// Set rotate to false so the shoe stops rotating
		rotate = false;
    }
}
