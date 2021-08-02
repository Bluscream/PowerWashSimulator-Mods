using System;
using System.Collections;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using PWS.TexturePainter;

// using Object = UnityEngine.Object;
[assembly: MelonInfo(typeof(TestMod.Mod), "My Mod Name", "version", "Author Name")]
[assembly: MelonGame("GameStudio", "GameName")]
namespace TestMod
{
    public class Mod : MelonMod
    {
        public void Log(string msg) => Debug.Log(msg);
        public override void OnApplicationStart()
        {
            PWS.WashManager.Instance.add_onSurfaceCompletelyCleaned(new Action<WashTarget>(onSurfaceCompletelyCleaned));
            // ...
            //var washTargetsIl2cpp = UnityEngine.Object.FindObjectsOfType(PWS.TexturePainter.WashTarget.Il2CppType);
            //Il2CppTypeOf<PWS.TexturePainter.WashTarget>.Type
            //PWS.TexturePainter.WashTarget[] washTargetsMono = washTargetsIl2cpp.Cast<PWS.TexturePainter.WashTarget[]>();

            //Debug.Log("Found " + washTargetsMono.Length + " washTargetsMono");
            //for (int i = 0; i < washTargetsMono.Length; i++)
            //{
            //    Debug.Log(i+1 + " > " + washTargetsMono[i].gameObject.name);
            //}
            Log("=== WASH START ===");
            foreach (var component in GetWashables())
            {
                WashTarget washTarget;
                if ((washTarget = component.TryCast<WashTarget>()) == null) continue;
                Log($"{washTarget.gameObject.name} = {washTarget.GetCleanProgress()}% ({washTarget.CurrentDirtiness})");
                washTarget.CurrentDirtiness = 0;
                Log($"{washTarget.gameObject.name} = {washTarget.GetCleanProgress()}% ({washTarget.CurrentDirtiness})");
            }
            Log("=== WASH END ===");

        }
        UnityEngine.Object[] GetWashables()
        {
            return UnityEngine.Object.FindObjectsOfType(Il2CppType.Of<WashTarget>());
        }
        public void onSurfaceCompletelyCleaned(WashTarget washTarget) {
            Log($"onSurfaceCompletelyCleaned: {washTarget.gameObject.name}");
        }
    }
}
