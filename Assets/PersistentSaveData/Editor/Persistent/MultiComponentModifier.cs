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
using PersistentSaveData.Core;
using UnityEngine;
using System.Linq;
using UnityEditor.SceneManagement;

namespace PersistentSaveDataEditor.Persistent
{
    
    public class MultiComponentModifier : ScriptableWizard
    {

        public PersistentReference original = null;
        public PersistentReference[] copies = null;

        PersistentReference lastCopied = null;
        PersistentReferenceEditor m_Editor = null;

        [MenuItem("Window/Persistent Save Data/MultiComponent Modifier")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<MultiComponentModifier>("Multi Component", "Close", "Apply");
        }

        void OnWizardCreate()
        {
        }

        void OnWizardUpdate()
        {
            if (!original)
            {
                helpString = "Please add a PersistentReference component into the available slot marked 'Original'";
            }
            else
            {
                helpString = "Use the 'Apply' button to apply the 'Original' changes to the 'Copies'.";
            }
        }

        // When the user presses the "Apply" button OnWizardOtherButton is called.
        void OnWizardOtherButton()
        {
            if(original)
            {
                if (Selection.gameObjects.Where(x => x.GetComponent<PersistentReference>()).Count() > 0)
                {
                    Selection.activeGameObject = original.gameObject;
                }
                for (int i = 0; i < copies.Length; i++)
                {
                    PersistentReference pr = copies[i];
                    PersistentReferenceEditor.SetComponentReferenceAndFilename(pr);
                    pr.referenceIndex = original.referenceIndex;
                    pr.playModeSync = original.playModeSync;

                    pr.persistData.members = new Member[original.persistData.members.Length];
                    for (int j = 0; j < original.persistData.members.Length; j++)
                    {
                        Member member = original.persistData.members[j];
                        pr.persistData.members[j] = new Member(member.type, member.name, member.index);
                        pr.persistData.members[j].value = member.value;
                    }
                    pr.savePersistence = original.savePersistence;
                    pr.loadPersistence = original.loadPersistence;
                    EditorUtility.SetDirty(pr);
                }
            }
        }

        void OnEnable()
        {
            EditorSceneManager.activeSceneChangedInEditMode -= EditorSceneManager_activeSceneChangedInEditMode;
            EditorSceneManager.activeSceneChangedInEditMode += EditorSceneManager_activeSceneChangedInEditMode;
        }

        void OnDestroy()
        {
            DestroyEditor();
            EditorSceneManager.activeSceneChangedInEditMode -= EditorSceneManager_activeSceneChangedInEditMode;
        }

        void EditorSceneManager_activeSceneChangedInEditMode(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            DestroyEditor();
            original = null;
            copies = null;
        }

        protected override bool DrawWizardGUI()
        {
            bool changed = base.DrawWizardGUI();
            
            if(changed)
            {
                if (original != lastCopied && original)
                {
                    DestroyEditor();
                    lastCopied = original;
                    m_Editor = Editor.CreateEditor(original) as PersistentReferenceEditor;
                }
                else
                {
                    if(!original)
                    {
                        DestroyEditor();
                    }
                }
            }

            if(m_Editor)
            {
                EditorGUILayout.HelpBox("Edit the values below to change the copies to the modified values.", MessageType.Info);
                m_Editor.OnInspectorGUI();
            }

            return changed;
        }

        void DestroyEditor()
        {
            if (m_Editor != null)
            {
                DestroyImmediate(m_Editor);
                m_Editor = null;
            }
        }
    }
}
