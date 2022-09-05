using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InputSystem
{
    public class InputActionRegistry : MonoBehaviour
    {
        public static InputActionRegistry Instance { get; private set; }

        [Header("Keyboard Sprites")] 
        [SerializeField] private Sprite _keyboardKey;
        [SerializeField] private Sprite _leftMouseButton;
        [SerializeField] private Sprite _middleMouseButton;
        [SerializeField] private Sprite _rightMouseButton;

        [Header("Button Sprites")]
        [SerializeField] private Sprite _gamepadButtonSouth;
        [SerializeField] private Sprite _gamepadButtonEast;
        [SerializeField] private Sprite _gamepadButtonWest;
        [SerializeField] private Sprite _gamepadButtonNorth;
        [SerializeField] private Sprite _gamepadLeftBumper;
        [SerializeField] private Sprite _gamepadRightBumper;
        [SerializeField] private Sprite _gamepadLeftTrigger;
        [SerializeField] private Sprite _gamepadRightTrigger;
        [SerializeField] private Sprite _gamepadLeftStickClick;
        [SerializeField] private Sprite _gamepadRightStickClick;

        private readonly IDictionary<string, Sprite> _inputBindImages = new Dictionary<string, Sprite>();

        private readonly IDictionary<KeyBind, string> _inputRegistry = new Dictionary<KeyBind, string>
        {
            {KeyBind.A, "<Keyboard>/a"},
            {KeyBind.B, "<Keyboard>/b"},
            {KeyBind.C, "<Keyboard>/c"},
            {KeyBind.D, "<Keyboard>/d"},
            {KeyBind.E, "<Keyboard>/e"},
            {KeyBind.F, "<Keyboard>/f"},
            {KeyBind.G, "<Keyboard>/g"},
            {KeyBind.H, "<Keyboard>/h"},
            {KeyBind.I, "<Keyboard>/i"},
            {KeyBind.J, "<Keyboard>/j"},
            {KeyBind.K, "<Keyboard>/k"},
            {KeyBind.L, "<Keyboard>/l"},
            {KeyBind.M, "<Keyboard>/m"},
            {KeyBind.N, "<Keyboard>/n"},
            {KeyBind.O, "<Keyboard>/o"},
            {KeyBind.P, "<Keyboard>/p"},
            {KeyBind.Q, "<Keyboard>/q"},
            {KeyBind.R, "<Keyboard>/r"},
            {KeyBind.S, "<Keyboard>/s"},
            {KeyBind.T, "<Keyboard>/t"},
            {KeyBind.U, "<Keyboard>/u"},
            {KeyBind.V, "<Keyboard>/v"},
            {KeyBind.W, "<Keyboard>/w"},
            {KeyBind.X, "<Keyboard>/x"},
            {KeyBind.Y, "<Keyboard>/y"},
            {KeyBind.Z, "<Keyboard>/z"},
            {KeyBind.N1, "<Keyboard>/1"},
            {KeyBind.N2, "<Keyboard>/2"},
            {KeyBind.N3, "<Keyboard>/3"},
            {KeyBind.N4, "<Keyboard>/4"},
            {KeyBind.N5, "<Keyboard>/5"},
            {KeyBind.N6, "<Keyboard>/6"},
            {KeyBind.N7, "<Keyboard>/7"},
            {KeyBind.N8, "<Keyboard>/8"},
            {KeyBind.N9, "<Keyboard>/9"},
            {KeyBind.N0, "<Keyboard>/0"},
            {KeyBind.Escape, "<Keyboard>/escape"},
            {KeyBind.Delete, "<Keyboard>/delete"},
            {KeyBind.Shift, "<Keyboard>/shift"},
            {KeyBind.LeftShift, "<Keyboard>/leftShift"},
            {KeyBind.RightShift, "<Keyboard>/rightShift"},
            {KeyBind.GamepadButtonSouth, "<Gamepad>/buttonSouth"},
            {KeyBind.GamepadButtonEast, "<Gamepad>/buttonEast"},
            {KeyBind.GamepadButtonWest, "<Gamepad>/buttonWest"},
            {KeyBind.GamepadButtonNorth, "<Gamepad>/buttonNorth"},
            {KeyBind.GamepadLeftBumper, "<Gamepad>/leftShoulder"},
            {KeyBind.GamepadRightBumper, "<Gamepad>/rightShoulder"},
            {KeyBind.GamepadLeftTrigger, "<Gamepad>/leftTrigger"},
            {KeyBind.GamepadRightTrigger, "<Gamepad>/rightTrigger"},
            {KeyBind.GamepadLeftStickClick, "<Gamepad>/leftStickClick"},
            {KeyBind.GamepadRightStickClick, "<Gamepad>/rightStickClick"},
        };

        #region UnityEvents

        private void Awake()
        {
            Instance = this;

            GetInputBindSprites();
        }

        #endregion

        #region Utility

        private void GetInputBindSprites()
        {
            // Buttons
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadButtonSouth), _gamepadButtonSouth);
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadButtonEast), _gamepadButtonEast);
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadButtonWest), _gamepadButtonWest);
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadButtonNorth), _gamepadButtonNorth);

            // Bumpers
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadLeftBumper), _gamepadLeftBumper);
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadRightBumper), _gamepadRightBumper);

            // Triggers
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadLeftTrigger), _gamepadLeftTrigger);
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadRightTrigger), _gamepadRightTrigger);

            // Sticks
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadLeftStickClick), _gamepadLeftStickClick);
            _inputBindImages.TryAdd(KeyCodeToBindingString(KeyBind.GamepadRightStickClick), _gamepadRightStickClick);
        }

        public static Sprite GetKeyboardKeyImage()
        {
            return Instance._keyboardKey;
        }

        public static string KeyCodeToBindingString(KeyBind key)
        {
            return Instance._inputRegistry.TryGetValue(key, out var path) ? path : null;
        }

        public static Sprite KeyCodeToImage(KeyBind key)
        {
            var path = KeyCodeToBindingString(key);
            if (path == null)
                return null;

            return Instance._inputBindImages.TryGetValue(path, out var sprite) ? sprite : null;
        }

        public static KeyBind BindingStringToKeyCode(string binding)
        {
            var pairs = Instance._inputRegistry.ToList();
            foreach (var (key, path) in pairs)
            {
                if (path.Equals(binding))
                    return key;
            }

            return KeyBind.None;
        }

        public List<KeyBind> GetKeyCodes() => Instance._inputRegistry.Keys.ToList();

        public static string EnumToString(KeyBind key)
        {
            var keyName = key.ToString();
            if (keyName.StartsWith('N') && keyName.Length == 2) return $"{keyName[1]}";
            return keyName;
        }
        
        #endregion
    }
}
