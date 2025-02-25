using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get 
        {
            if(_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public SoundAudioClip[] soundAudioClipArray;

    public Transform pfDamagePopup;
    public Transform pfTextPopup;

    [System.Serializable]
    public class BuffIcon
    {
        public BuffBar.BuffType type;
        public Sprite icon;
    }
    public BuffIcon[] buffIconArray;

    [System.Serializable]
    public class RoomIcon 
    {
        public Node.Type type;
        public Sprite icon;
    }
    public RoomIcon[] roomIconArray;
}
