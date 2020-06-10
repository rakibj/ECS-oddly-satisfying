using UnityEngine;
using UnityEditor;

namespace Rakib
{
	public class ExtendedHotkeys : ScriptableObject
	{
		//Deselect all by pressing Shift + D
		[MenuItem("Rakib/Select GameConfig %_t")]
		static void SelectGameConfig()
		{
			Selection.activeObject=AssetDatabase.LoadMainAssetAtPath("Assets/_Rakib/Setups/GameConfig.asset");
		}

	}
}