using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/View/Label/Gs2CoreStringConcat")]
    public partial class Gs2CoreStringConcat : MonoBehaviour
    {
        private string _value1;
        private string _value2;

        public void Update()
        {
            if (this._value1 != null && this._value2 != null)
            {
                onUpdate?.Invoke(this._value1 + this.delimiter + this._value2);
            }
            else {
                onUpdate?.Invoke("");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreStringConcat
    {
        public void Awake()
        {
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreStringConcat
    {
        public void SetValue1(string value) {
            this._value1 = value;
            Update();
        }
        
        public void SetValue2(string value) {
            this._value2 = value;
            Update();
        }
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreStringConcat
    {
        public string delimiter;
    }
    
    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreStringConcat
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}
