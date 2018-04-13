# Unity ARCore
## Getting Started

* Android SDK version 7.0 (API Level 24) or higher.
* Unity 2017.3.0f2 or higher, with the Android Build Support component.
* A [supported Android device](https://developers.google.com/ar/discover/#supported_devices).

## Getting Started with ARCore SDK
The first thing you need to do is download the SDK. You can download the ARCore SDK for Unity from [here](https://github.com/google-ar/arcore-unity-sdk/releases/download/v1.1.0/arcore-unity-sdk-v1.1.0.unitypackage).

Next, import it into your project. From Unity’s menu, select Assets\Import Package\Custom Package... and then select the ARCore-Unity-SDK.unitypackage.

Make sure everything is selected and then click the Import button.

## Configure the build settings
Open the Build Settings window by clicking File > Build settings.

Then, change the following settings:
1. Change the target platform to Android and click Switch Platform.
2. Click Player Settings to open the Android Player Settings. Then change the following settings:
* Other Settings > Multithreaded Rendering: Off
* Other Settings > Package Name: a unique app ID such as com.<your_team>.UnityARCoreHackathon
* Other Settings > Minimum API Level: Android 7.0 or higher
* Other Settings > Target API Level: Android 7.0 or higher
* XR Settings > ARCore Supported: On
3. The Scenes in Build lists the scenes from your project that will be included in your build


## Build and run
Make sure your device is connected to your machine and then click Build and Run. Unity builds your project into an Android APK, installs it on the device, and launches it.