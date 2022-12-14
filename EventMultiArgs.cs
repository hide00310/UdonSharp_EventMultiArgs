
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class EventMultiArgs : UdonSharpBehaviour
{
    public UnityEngine.UI.Text text1;
    public UnityEngine.UI.Text text2;

    [UdonSynced, FieldChangeCallback(nameof(EventFlag1))]
    private bool _eventFlag1;
    [UdonSynced, FieldChangeCallback(nameof(EventFlag2))]
    private bool _eventFlag2;
    [UdonSynced, FieldChangeCallback(nameof(Arg1))]
    private int _arg1;
    [UdonSynced, FieldChangeCallback(nameof(Arg2))]
    private int _arg2;
    [UdonSynced, FieldChangeCallback(nameof(Arg3))]
    private int _arg3;

    public bool EventFlag1
    {
        set => _eventFlag1 = value;
        get => _eventFlag1;
    }
    public bool EventFlag2
    {
        set => _eventFlag2 = value;
        get => _eventFlag2;
    }
    public int Arg1
    {
        set => _arg1 = value;
        get => _arg1;
    }
    public int Arg2
    {
        set => _arg2 = value;
        get => _arg2;
    }
    public int Arg3
    {
        set => _arg3 = value;
        get => _arg3;
    }
    private bool _prevEventFlag1;
    private bool _prevEventFlag2;

    private int _cnt;
    private int _cnt2;

    public void OnClick1()
    {
        if (!Networking.LocalPlayer.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
        _cnt++;
        SendMyEvent1(_cnt, _cnt+1);
    }
    public void OnClick2()
    {
        if (!Networking.LocalPlayer.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
        _cnt2++;
        SendMyEvent2(_cnt2);
    }

    public void SendMyEvent1(int arg1, int arg2)
    {
        Arg1 = arg1;
        Arg2 = arg2;
        EventFlag1 = !EventFlag1;
        RequestSerialization();
    }
    public void SendMyEvent2(int arg3)
    {
        Arg3 = arg3;
        EventFlag2 = !EventFlag2;
        RequestSerialization();
    }
    public override void OnPreSerialization()
    {
        UpdateText();
    }
    public override void OnDeserialization()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        if (EventFlag1 != _prevEventFlag1)
        {
            text1.text = $"Arg1:{Arg1}, Arg2:{Arg2}";
            _prevEventFlag1 = EventFlag1;
        }
        if (EventFlag2 != _prevEventFlag2)
        {
            text2.text = $"Arg3:{Arg3}";
            _prevEventFlag2 = EventFlag2;
        }
    }
}
