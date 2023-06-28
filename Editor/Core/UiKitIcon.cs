using UnityEditor;
using UnityEngine;

namespace Editor.Core
{
    public class UiKitIcon
    {
        private const int WIDTH = 16;
    
        private static Texture texture;
    
        [InitializeOnLoadMethod]
        private static void OnLoad() {
            texture = Resources.Load("gs2-logo") as Texture;
            EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
        }
    
        private static void OnGUI( int instanceID, Rect selectionRect )
        {
            var gameObject = EditorUtility.InstanceIDToObject( instanceID ) as GameObject;
            if ( gameObject == null )
            {
                return;
            }

            bool includeUiKit = false;
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (component.GetType().Module.Name == "Gs2.Unity.UIKit.dll") {
                    includeUiKit = true;
                    break;
                }
            }

            if (!includeUiKit) {
                return;
            }
            
            if ( texture == null )
            {
                return;
            }
        
            var pos = selectionRect;
            pos.x = pos.xMax - WIDTH;
            pos.width = WIDTH;
        
            GUI.DrawTexture( pos, texture, ScaleMode.ScaleToFit, true );
        }
    }
}
