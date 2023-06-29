using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTab : MonoBehaviour
{
    [SerializeField]
    private OptionKinds optionKind;
    public OptionKinds optionKindProp => optionKind;
}
