
namespace ItemSystem
{
    using UnityEngine;

    public class GUID : MonoBehaviour
    {
        [SerializeField]
        private string guidString;

        private System.Guid _guid;
        public System.Guid UniqueGuid
        {
            get
            {
                if (_guid == System.Guid.Empty && !System.String.IsNullOrEmpty(guidString))
                {
                    _guid = new System.Guid(guidString);
                }
                return _guid;
            }
        }

        public string Value
        {
            get { return guidString; }
        }

        public void Generate()
        {
            _guid = System.Guid.NewGuid();
            guidString = UniqueGuid.ToString();
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(GUID))]

    class GuidInspector : UnityEditor.Editor
    {
        void OnEnable()
        {
            GUID guid = (GUID)target;

            if (guid.UniqueGuid == System.Guid.Empty)
            {
                guid.Generate();
                UnityEditor.EditorUtility.SetDirty(target);
            }
        }

        public override void OnInspectorGUI()
        {
            GUID guid = (GUID)target;
            UnityEditor.EditorGUILayout.BeginHorizontal("Box");
            UnityEditor.EditorGUILayout.SelectableLabel(guid.UniqueGuid.ToString());
            UnityEditor.EditorGUILayout.EndHorizontal();
        }
    }

    public class GenerateGuids : UnityEditor.ScriptableWizard
    {
        [UnityEditor.MenuItem("JEFFDEV/Helpers/GUID/Generate Guids")]
        static void Generate()
        {
            var objList = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (var obj in objList)
            {
                GUID guidObj = ((GameObject)obj).GetComponent<GUID>();
                if (guidObj != null)
                {
                    if (guidObj.UniqueGuid == System.Guid.Empty)
                    {
                        guidObj.Generate();

                        GUI.changed = true;
                        UnityEditor.EditorUtility.SetDirty(guidObj);
                    }
                }
            }
        }
    }
#endif
}