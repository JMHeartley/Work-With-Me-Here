using UnityEngine;
using UnityEngine.EventSystems;

namespace WebGLKeyboard
{
    /// <summary>
    /// Trigger the focus event in input fields
    /// </summary>
    public class DetectInputFocus : MonoBehaviour, IPointerClickHandler, IDeselectHandler
    {
        private KeyboardController _controller;
        private UnityEngine.UI.InputField _nativeInput;

#if USE_TMPRO
        private TMPro.TMP_InputField _tmproInput;
#endif

        //        [DllImport("__Internal")]
        //        private static extern bool IsMobile();

        //        public static bool PlayedOnMobile 
        //        { 
        //            get 
        //            {
        //#if !UNITY_EDITOR && UNITY_WEBGL
        //             return IsMobile();
        //#endif
        //            return false;                
        //            }
        //        }

        public void Initialize(KeyboardController _controller)
        {
            this._controller = _controller;
            _nativeInput = gameObject.GetComponent<UnityEngine.UI.InputField>();
#if USE_TMPRO
            _tmproInput = gameObject.GetComponent<TMPro.TMP_InputField>();
#endif
        }
        /// <summary>
        /// Calls the controller passing the selected input field to enable the keyboard
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerClick(PointerEventData data)
        {
            //if (PlayedOnMobile)
            //{
            if (_nativeInput != null)
            {
                _controller.FocusInput(_nativeInput);
            }
#if USE_TMPRO
            if (_tmproInput != null)
            {
                _controller.FocusInput(_tmproInput);
            }
#endif
            //}
        }
        /// <summary>
        /// Clears the input action if the player deselected the field ingame
        /// </summary>
        /// <param name="data"></param>
        public void OnDeselect(BaseEventData data)
        {
            //if (PlayedOnMobile)
            //{
            _controller.ForceClose();
            //}
        }
    }
}