#region Author
/*
     
     Jones St. Lewis Cropper (caLLow)
     
     Another caLLowCreation
     
     Visit us on Google+ and other social media outlets @caLLowCreation
     
     Thanks for using our product.
     
     Send questions/comments/concerns/requests to 
      e-mail: caLLowCreation@gmail.com
      subject: Persistent Save Data
     
*/
#endregion

using UnityEditor;
using PersistentSaveData.Persistent;
using PersistentSaveData.Core.Formatters;
using PersistentSaveData.Formatters;
using System.Collections.Generic;
using System;
using UnityEngine;
using PersistentSaveData.DefaultNames;
using TryItCompatibilityEditor;
using PersistentSaveData.Core;

namespace PersistentSaveDataEditor.Persistent
{
    /// <summary>
    /// Opens example scene picker
    /// </summary>
    [InitializeOnLoad]
    internal sealed class OpenScenesEditorWindow
    {
        static OpenScenesEditorWindow()
        {
            //Debug.Log("OpenScenesEditorWindow UpdateShowPrompt");
            ExampleScenesWindow.newsletterSignupUrl = "http://eepurl.com/bPDq3P";
            ExampleScenesWindow.isFullVersion = !PersistentSettings.Instance.GetTryIt();
            ExampleScenesWindow.packageName = "Persistent Save Data";
            ExampleScenesWindow.buttonTexts = new SceneButton[]
            {
                new SceneButton("Open Example Scene", "Example Scene", "Assets/PersistentSaveData/Scenes/Main Example Scene.unity",
                    "This scene, Example Scene, is a quick show of how the main components are used.  Press play, enter text into the Input field.  Use the Save Text button to save the text to file then use the Load Text button to load the text from file.  Use the movement/arrow keys to move the circle.  The circle will load it's position when you press play again."),
                new SceneButton("Open Points Example", "Points Scene", "Assets/PersistentSaveData/Scenes/Points Example Scene.unity",
                    "This scene, Points Example Scene, shows an example of saving and loading points.  Use the movement/arrow keys to collect the green objects to earn points.  Stop and start play mode to see point persistent."),
                new SceneButton("Open REST Points Example", "REST Points Scene", "Assets/PersistentSaveData/Scenes/Points Example Scene REST.unity",
                    "Cloud save and load is here.  This scene, Points Example Scene, shows an example of saving and loading points.  Use the movement/arrow keys to collect the green objects to earn points.  Stop and start play mode to see point persistent."),
            };

            ExampleScenesWindow.fullVersionUrl = "http://u3d.as/JuQ";
        }

        [MenuItem("Window/Persistent Save Data/Example Scenes")]
        internal static void Init()
        {
            ExampleScenesWindow.Init(height: 280, width: 280);
        }
    }
}
