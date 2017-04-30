using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomInputType
{
    MonoInput,
    BinaryInput,
    MultiInput,
    SeriesInput,
}

public class CustomInput : Singleton<CustomInput>
{
    #region Subclasses
    [System.Serializable]
    private class InputComponent
    {
        [SerializeField]
        protected string m_inputName;

        public string name { get { return m_inputName; } }

        public bool IsInput(string name) =>
            name == m_inputName;
    }

    [System.Serializable]
    private class InputInterpreter
    {
        [SerializeField]
        private string m_input;
        private int m_mouseButton;
        private KeyCode m_keyCode = KeyCode.None;
        private bool m_isMouseInput = false;
        private bool m_interpreted = false;

        private bool isMouseInput
        {
            get
            {
                if (!m_interpreted)
                {
                    InterpreteKey();
                }
                return m_isMouseInput;
            }
        }

        private void InterpreteKey()
        {
            if (m_input.ToUpper().StartsWith("MOUSE"))
            {
                m_isMouseInput = true;
                m_mouseButton = StringToMouseKey(m_input);
                m_keyCode = (KeyCode)((int)KeyCode.Mouse0 + m_mouseButton);
            }
            else
                m_keyCode = StringToKey(m_input);

            m_interpreted = true;
        }

        private KeyCode StringToKey(string key)
        {
            var upperCased = key.ToUpper();
            var splitKey = key.Substring(1);
            var revisedKey = upperCased[0].ToString() + splitKey;
            return (KeyCode)System.Enum.Parse(typeof(KeyCode), revisedKey);
        }

        private int StringToMouseKey(string key)
        {
            if (key.Length == 6)
            {
                return int.Parse(key.Substring(5));
            }
            else
            {
                Debug.LogError("Invalid Mouse Input. Follow mouse# format");
                return -1;
            }
        }

        private KeyCode keyCode
        {
            get
            {
                if (!m_interpreted)
                {
                    m_keyCode = StringToKey(m_input);
                }
                return m_keyCode;
            }
        }

        public int TapValue() =>
            isMouseInput ? (Input.GetMouseButtonDown(m_mouseButton) ? 1 : 0) : (Input.GetKeyDown(keyCode) ? 1 : 0);


        public int HoldValue() =>
            isMouseInput ? (Input.GetMouseButton(m_mouseButton) ? 1 : 0) : (Input.GetKey(keyCode) ? 1 : 0);

        public int ReleaseValue() =>
            isMouseInput ? (Input.GetMouseButtonUp(m_mouseButton) ? 1 : 0) : (Input.GetKeyUp(keyCode) ? 1 : 0);

        public bool isTapped() =>
            isMouseInput ? Input.GetMouseButtonDown(m_mouseButton) : Input.GetKeyDown(keyCode);

        public bool isHeld() =>
            isMouseInput ? Input.GetMouseButton(m_mouseButton) : Input.GetKey(keyCode);

        public bool isRelease() =>
            isMouseInput ? Input.GetMouseButtonUp(m_mouseButton) : Input.GetKeyUp(keyCode);
    }

    [System.Serializable]
    private class MonoInput : InputComponent
    {
        [SerializeField]
        private InputInterpreter m_key;

        public int TapValue() =>
            m_key.TapValue();

        public int HoldValue() =>
            m_key.HoldValue();

        public int ReleaseValue() =>
            m_key.ReleaseValue();

        public bool isTapped() =>
            m_key.isTapped();


        public bool isHeld() =>
            m_key.isHeld();

        public bool isReleased() =>
            m_key.isRelease();
    }

    [System.Serializable]
    private class BinaryInput : InputComponent
    {
        [SerializeField]
        private InputInterpreter m_positive;
        [SerializeField]
        private InputInterpreter m_negative;

        public int TapValue()
        {
            int value = 0;

            value += m_positive.TapValue();
            value -= m_negative.TapValue();

            return value;
        }

        public int HoldValue()
        {
            int value = 0;
            value += m_positive.HoldValue();
            value -= m_negative.HoldValue();
            return value;
        }

        public int ReleaseValue()
        {
            int value = 0;

            value += m_positive.ReleaseValue();
            value -= m_negative.ReleaseValue();

            return value;
        }

        public bool isTapped() =>
           m_positive.isTapped() || m_negative.isTapped();


        public bool isHeld() =>
            m_positive.isHeld() || m_negative.isHeld();

        public bool isReleased() =>
            (m_positive.isRelease() || m_negative.isRelease());

        public bool isFree() =>
            (m_positive.isRelease() && m_negative.isRelease());
    }

    [System.Serializable]
    private class MultiInput : InputComponent
    {
        [SerializeField]
        private InputInterpreter m_executionKey;
        [SerializeField]
        private InputInterpreter[] m_HoldKeys;

        public bool isExecuted()
        {
            for (int i = 0; i < m_HoldKeys.Length; i++)
            {
                if (m_HoldKeys[i].HoldValue() == 0)
                    return false;
            }

            return m_executionKey.TapValue() == 1;
        }
    }
    #endregion

    #region OnHold
    //[System.Serializable]
    //private class SeriesInput : InputComponent
    //{
    //    [SerializeField]
    //    private KeyInterpreter[] m_inputSeries;
    //    private int checkIndex = 0;

    //    public bool isQualified()
    //    {
    //        if(m_inputDecoder == null)
    //        {
    //            m_inputDecoder = new bool[m_inputSeries.Length];
    //        }


    //    }
    //}
    #endregion

    private static int[] m_keyValues;
    private static bool[] m_keys;

    [SerializeField]
    [Tooltip("Amount of time the next input is valid for combos")]
    private float m_validTime;

    [Header("Inputs")]
    [SerializeField]
    private MonoInput[] m_monoInputs;
    [SerializeField]
    private BinaryInput[] m_binaryInputs;
    [SerializeField]
    private MultiInput[] m_multiInputs;
    //[SerializeField]
    //private SeriesInput[] m_seriesInputs;

    private void Sort<T>(ref T[] array) where T : InputComponent
    {
        var list = new List<T>(array);
        list.Sort((x, y) => (x.name.CompareTo(y.name)));
        array = list.ToArray();
    }

    private T GetInput<T>(CustomInputType type, string inputName) where T : InputComponent
    {
        string restrictionCheck = "";
        int checkIndex = 0;
        InputComponent[] inputList = null;

        switch (type)
        {
            case CustomInputType.MonoInput:
                inputList = m_monoInputs;
                break;

            case CustomInputType.BinaryInput:
                inputList = m_binaryInputs;
                break;

            case CustomInputType.MultiInput:
                inputList = m_multiInputs;
                break;

            case CustomInputType.SeriesInput:
                Debug.LogWarning($"{type} not Implemented");
                break;
        }

        //Terminates Early if there is no input List
        if (inputList == null)
        {
            Debug.LogError($"{type} has no Value");
            return null;
        }

        //Terminates Early if the input name precedes the name of the first input in sorted order
        if (inputName.CompareTo(inputList[0].name) == -1)
        {
            Debug.LogWarning($"{inputName} not Found");
            return null;
        }

        for (int i = 0; i < inputList.Length; i++)
        {
            var currentInput = inputList[i];

            if (inputName[checkIndex] != currentInput.name[checkIndex])
            {
                if (inputName.ToUpper() == currentInput.name.ToUpper())
                {
                    Debug.LogWarning($"Do you mean to call {currentInput.name}");
                }

                //Terminates Early if the input name follows the current name of the input in sorted order
                if (checkIndex > 0 && !currentInput.name.StartsWith(restrictionCheck))
                {
                    Debug.LogWarning($"{inputName} not Found in sorted");
                    return null;
                }
            }
            else
            {
                restrictionCheck += inputName[checkIndex];
                checkIndex++;
                if (currentInput.IsInput(inputName))
                {
                    return (T)currentInput;
                }
            }
        }
        Debug.LogWarning($"{inputName} not Found");
        return null;
    }

    #region Getters
    public bool isTapped(CustomInputType type, string inputName)
    {
        switch (type)
        {
            case CustomInputType.MonoInput:
                return GetInput<MonoInput>(CustomInputType.MonoInput, inputName)?.isTapped() ?? false;

            case CustomInputType.BinaryInput:
                return GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.isTapped() ?? false;

            default:
                Debug.LogWarning($"isTapped is not Applicable to {type}");
                return false;
        }
    }


    public bool isHeld(CustomInputType type, string inputName)
    {
        switch (type)
        {
            case CustomInputType.MonoInput:
                return GetInput<MonoInput>(CustomInputType.MonoInput, inputName)?.isHeld() ?? false;

            case CustomInputType.BinaryInput:
                return GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.isHeld() ?? false;

            default:
                Debug.LogWarning($"isHeld is not Applicable to {type}");
                return false;
        }
    }

    public bool isReleased(CustomInputType type, string inputName)
    {
        switch (type)
        {
            case CustomInputType.MonoInput:
                return GetInput<MonoInput>(CustomInputType.MonoInput, inputName)?.isReleased() ?? false;

            case CustomInputType.BinaryInput:
                return GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.isReleased() ?? false;

            default:
                Debug.LogWarning($"isHeld is not Applicable to {type}");
                return false;
        }
    }

    public bool isFree(string inputName) =>
         GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.isFree() ?? false;

    public bool isExecuted(string inputName) =>
        GetInput<MultiInput>(CustomInputType.MultiInput, inputName)?.isExecuted() ?? false;

    public int GetTapValue(CustomInputType type, string inputName)
    {
        switch (type)
        {
            case CustomInputType.MonoInput:
                return GetInput<MonoInput>(CustomInputType.MonoInput, inputName)?.TapValue() ?? 0;

            case CustomInputType.BinaryInput:
                return GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.TapValue() ?? 0;

            default:
                Debug.LogWarning($"{type} not Implemented");
                return 0;
        }
    }

    public int GetHoldValue(CustomInputType type, string inputName)
    {
        switch (type)
        {
            case CustomInputType.MonoInput:
                return GetInput<MonoInput>(CustomInputType.MonoInput, inputName)?.HoldValue() ?? 0;

            case CustomInputType.BinaryInput:
                return GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.HoldValue() ?? 0;

            default:
                Debug.LogWarning($"{type} not Implemented");
                return 0;
        }
    }

    public int GetReleasedValue(CustomInputType type, string inputName)
    {
        switch (type)
        {
            case CustomInputType.MonoInput:
                return GetInput<MonoInput>(CustomInputType.MonoInput, inputName)?.ReleaseValue() ?? 0;

            case CustomInputType.BinaryInput:
                return GetInput<BinaryInput>(CustomInputType.BinaryInput, inputName)?.ReleaseValue() ?? 0;

            default:
                Debug.LogWarning($"{type} not Implemented");
                return 0;
        }
    }

    #endregion

    void Awake()
    {
        m_keyValues = (int[])System.Enum.GetValues(typeof(KeyCode));
        m_keys = new bool[m_keyValues.Length];
        Sort(ref m_monoInputs);
        Sort(ref m_binaryInputs);
        Sort(ref m_multiInputs);
    }

    void Update()
    {
        for (int i = 0; i < m_keyValues.Length; i++)
        {
            m_keys[i] = Input.GetKey((KeyCode)m_keyValues[i]);
        }
    }
}

