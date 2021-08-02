using System;
using System.Collections;
using System.Linq;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using PWS.TexturePainter;
using System.Collections.Generic;

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

        }
        public void WashAll()
        {
            var washables = UnityEngine.Object.FindObjectsOfType(Il2CppType.Of<WashTarget>());
            Log($"=== WASH START ({washables.Length} washables) ===");
            // var _i = 0;
            foreach (var component in washables)
            {
                WashTarget washTarget;
                if ((washTarget = component.TryCast<WashTarget>()) == null) continue;
                if (washTarget.gameObject == null) continue;
                // Log($"{washTarget.gameObject.name} = {washTarget.GetCleanProgress()}% ({washTarget.CurrentDirtiness})");
                washTarget.CurrentDirtiness = 0;
                // _i++;
                // Log($"Washing {washTarget.gameObject.name} ( {_i} / {_l} )");
            }
            Log($"=== WASH END ({washables.Length} washables) ===");
        }
        UnityEngine.Object[] GetWashables()
        {
            return UnityEngine.Object.FindObjectsOfType(Il2CppType.Of<WashTarget>());
        }
        public void PrintColliderNames()
        {
            var colliders = UnityEngine.Object.FindObjectsOfType(Il2CppType.Of<Collider>());
            var colliderNames = new SortedSet<string>();
            foreach (var component in colliders)
            {
                Collider collider;
                if ((collider = component.TryCast<Collider>()) == null) continue;
                if (collider.gameObject == null) continue;
                if (collider.name.IndexOf("col", StringComparison.OrdinalIgnoreCase) < 0) continue;
                colliderNames.Add(collider.name);
            }
            Log("=== COLLIDERS START ===");
            Log(string.Join("\n", colliderNames));
            Log("=== COLLIDERS END ===");
        }
        public List<UnityEngine.Object> GetOuterColliders()
        {
            var _meshColliders = UnityEngine.Object.FindObjectsOfType(Il2CppType.Of<MeshCollider>());
            var outsideColliderNames = new List<string>() { "OuterRingCollision", "BoundsCol", "StoryBookCottageCollision", "HouseCol", "BoundaryCollision", "BoundaryCollision_02", "BoundaryCollision_03" };
            var meshColliders = new List<UnityEngine.Object>();
            foreach (var component in _meshColliders)
            {
                MeshCollider meshCollider;
                if ((meshCollider = component.TryCast<MeshCollider>()) == null) continue;
                if (meshCollider.gameObject == null) continue;
                Log($"Found collider {meshCollider.name}!");
                if (!outsideColliderNames.Contains(meshCollider.name)) continue;
                Log($"Found collider {meshCollider.name}, disabling!");
                meshCollider.gameObject.active = false;
                meshColliders.Add(component);
            }
            return meshColliders;
        }
        public void onSurfaceCompletelyCleaned(WashTarget washTarget) {
            Log($"onSurfaceCompletelyCleaned: {washTarget.gameObject.name}");
        }
    }
}
