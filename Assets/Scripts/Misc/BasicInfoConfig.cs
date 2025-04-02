using UnityEngine;

namespace WinterUniverse
{
    public abstract class BasicInfoConfig : ScriptableObject
    {
        [SerializeField] protected string _id;
        [SerializeField] protected string _displayName;
        [SerializeField, TextArea] protected string _description;
        [SerializeField] protected Color _color = Color.white;
        [SerializeField] protected Sprite _icon;

        public string ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public Color Color => _color;
        public Sprite Icon => _icon;
    }
}