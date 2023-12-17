
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class EventMultiArgs : UdonSharpBehaviour
{
    public Text Text1;
    public Text Text2;

    [UdonSynced]
    public bool EventFlag1;
    [UdonSynced]
    public bool EventFlag2;
    [UdonSynced]
    public int Arg1;
    [UdonSynced]
    public int Arg2;
    [UdonSynced]
    public int Arg3;

    bool EventFlag1Sub;
    bool EventFlag2Sub;
    int Arg1Sub;
    int Arg2Sub;
    int Arg3Sub;

    bool PrevEventFlag1;
    bool PrevEventFlag2;

    int Cnt;
    int Cnt2;

    public void OnClick1()
    {
        if (!Networking.LocalPlayer.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
        Cnt++;
        SendMyEvent1(Cnt, Cnt+1);
    }
    public void OnClick2()
    {
        if (!Networking.LocalPlayer.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
        Cnt2++;
        SendMyEvent2(Cnt2);
    }

    public void SendMyEvent1(int arg1, int arg2)
    {
        Arg1Sub = arg1;
        Arg2Sub = arg2;
        EventFlag1Sub = !EventFlag1Sub;
        RequestSerialization();
    }
    public void SendMyEvent2(int arg3)
    {
        Arg3Sub = arg3;
        EventFlag2Sub = !EventFlag2Sub;
        RequestSerialization();
    }
    public override void OnPreSerialization()
    {
        EventFlag1 = EventFlag1Sub;
        EventFlag2 = EventFlag2Sub;
        Arg1 = Arg1Sub;
        Arg2 = Arg2Sub;
        Arg3 = Arg3Sub;
        UpdateText();
    }
    public override void OnDeserialization()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        if (EventFlag1 != PrevEventFlag1)
        {
            Text1.text = $"Arg1:{Arg1}, Arg2:{Arg2}";
            PrevEventFlag1 = EventFlag1;
        }
        if (EventFlag2 != PrevEventFlag2)
        {
            Text2.text = $"Arg3:{Arg3}";
            PrevEventFlag2 = EventFlag2;
        }
    }
}
