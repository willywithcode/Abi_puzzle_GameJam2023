using UnityEngine;

namespace _Game._Script._SOS
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private GameObject level;
        [SerializeField] private int levelindex;
        [SerializeField] private int turnovers;
        public GameObject Level
        {
            get { return level; }
            set { level = value; }
        }
        public int Levelindex
        {
            get { return levelindex; }
            set { levelindex = value; }
        }
        public int Turnovers
        {
            get { return turnovers; }
            set { turnovers = value; }
        }
    }
}