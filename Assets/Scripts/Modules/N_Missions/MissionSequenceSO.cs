using System.Collections.Generic;
using UnityEngine;

namespace PP3.Missions
{
    // Optional ScriptableObject to author pipelines in the editor.
    [CreateAssetMenu(menuName = "PP3/Missions/Mission Sequence", fileName = "MissionSequence")]
    public class MissionSequenceSO : ScriptableObject
    {
         public List<MissionSO> Missions = new();
    }
}