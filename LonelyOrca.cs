using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2Cpp;
using System.Reflection;

namespace LonelyOrca
{
	public class LonelyOrcaMain : MelonMod
	{
        public static string orcaPath = Application.dataPath + "/../Mods/lonely-orca.unity3d";
        public static AssetBundle bundle;

        public override void OnInitializeMelon()
		{
            //bundle = AssetBundle.LoadFromFile(orcaPath);
            LoadEmbeddedAssetBundle();
            ClassInjector.RegisterTypeInIl2Cpp<OrcaTravel>();
        }

         public static void LoadEmbeddedAssetBundle()
         {
            MemoryStream memoryStream;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LonelyOrca.Resources.lonely-orca");
            memoryStream = new MemoryStream((int)stream.Length);
            stream.CopyTo(memoryStream);

            bundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
                        
         }

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
            // uConsole.RegisterCommand("orca-trigger", new uConsole.DebugCommand(OrcaTrigger));
            if (sceneName.Contains("WhalingStationRegion_SANDBOX"))
            {             

                OrcaTravel orcaTravel = GameObject.FindObjectOfType<OrcaTravel>();
                if (orcaTravel == null)
                {
                    MelonLogger.Msg("Spawning orca");
                    GameObject orcaprefab = LonelyOrcaMain.bundle.LoadAsset<GameObject>("Orca_Root");
                    GameObject orca = GameObject.Instantiate(orcaprefab);
                    orca.AddComponent<OrcaTravel>();
                }
            }
        }

        /*
       public override void OnUpdate()
       {
          if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad0))
           {
               OrcaTrigger();
           }
        }
        */

        private static void OrcaTrigger()
        {
            OrcaTravel orcaTravel = GameObject.FindObjectOfType<OrcaTravel>();
            if (orcaTravel == null)
            {
                MelonLogger.Msg("No orca found.");
                return;
            }

            orcaTravel.SkipDelay();
        }

    }
}