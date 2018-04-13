# Unity Cardboard VR
## Getting Started
To get started making your own VR game with Google Cardboard, you’re going to need the following:

* Unity Personal Edition, version 5.6 or newer
* A smartphone, either an iPhone 5 (or later) or an Android phone running Kit Kat (4.4) or later on which to test.
* A Cardboard unit.

## Getting Started with Google VR SDK
The first thing you need to do is download the SDK. Head on over to the [Google VR Downloads page](https://developers.google.com/vr/develop/unity/download) and download the SDK.

Next, import it into your project. From Unity’s menu, select Assets\Import Package\Custom Package... and then select the GoogleVRForUnity.unitypackage from within the repo you just downloaded.

Make sure everything is selected and then click the Import button.

## Hack it
To get your game working as a VR experience, you need to perform a few quick ‘n dirty hacks.

From the GoogleVR\Prefabs folder in the Project Browser, add the following prefabs to your scene:

Prefab name | Location in scene | Description
------------ | ------------- | -------------
GvrControllerMain | Anywhere in scene | Main controller prefab, responsible for managing controller state. Includes the GvrControllerInput component, which is the main entry point to the controller API.
GvrControllerPointer | Sibling to the Main Camera | Daydream controller prefab. Provides controller, laser, and reticle visualizations and serves as attachment point for tooltips and custom visualizations.
GvrEventSystem | Anywhere in scene | Drop-in replacement for Unity's Event System prefab. Includes the GvrPointerInputModule component instead of Unity's StandaloneInputModule. Lets the Daydream controller use the Unity event system.
GvrEditorEmulator | Anywhere in scene | Editor play mode camera controller. Lets you simulate user's head movement with your mouse or trackpad.
GvrInstantPreviewMain | Anywhere in scene | Editor play mode Instant Preview controller. Lets you stream a stereo preview to your phone and use a physical Daydream controller in the editor.


## Running your project on an iOS Device

* Select File\Build Settings... — Select iOS as your default platform.
* Click Player Settings and switch to the inspector
* In Other Settings, change your Bundle Identifier to be something appropriate for your team. (Like com.<your_team>.UnityVRHackathon).
* Change your Target Device to iPhone Only.
* In XR Settings, select Virtual Reality Supported checkbox and add Cardboard as your project SDK.

Attach your iPhone to your computer, select Build and Run and give your export folder a name.

Unity will export your project, and then it should automatically open up in Xcode. If it doesn’t, start up Xcode and manually, open the generated project, run it, and try it out on your phone.

## Running your project on an Android Device
* Select File\Build Settings... — Select Android as your default platform.
* Click Player Settings and switch to the inspector
* In Other Settings, change your Bundle Identifier to be something appropriate for your team. (Like com.<your_team>.UnityVRHackathon)
* In Other Settings, change the Minimum API Level to Android 4.4 Kit Kat
* In XR Settings, select Virtual Reality Supported checkbox and add Cardboard as your project SDK.

Attach your Android to your computer, select Build and Run and give your apk a name. It'll be automatically installed in your phone