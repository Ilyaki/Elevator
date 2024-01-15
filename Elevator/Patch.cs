using HarmonyLib;
using StardewValley;
using StardewValley.Buildings;
using System;
using System.Linq;
using System.Reflection;

namespace Elevator
{
	abstract class Patch
	{

		//Remember Prefix/Postfix should be public and static! Do not use lambdas

		protected class PatchDescriptor
		{
			public Type targetType;
			public string targetMethodName;
			public Type[] targetMethodArguments;

			/// <param name="targetType">Don't use typeof() or it won't work on other platforms</param>
			/// <param name="targetMethodName">Null if constructor is desired</param>
			/// <param name="targetMethodArguments">Null if no method abiguity</param>
			public PatchDescriptor(Type targetType, string targetMethodName, Type[] targetMethodArguments = null)
			{
				this.targetType = targetType;
				this.targetMethodName = targetMethodName;
				this.targetMethodArguments = targetMethodArguments;
			}
		}
		public static void PatchAll(string id)
		{
			Harmony harmonyInstance = new Harmony(id);
			harmonyInstance.Patch(original: AccessTools.Method(typeof(Building), nameof(Building.doAction)), prefix: new HarmonyMethod(typeof(BuildingPatcher_patchAction), nameof(BuildingPatcher_patchAction.Prefix)));
			harmonyInstance.Patch(original: AccessTools.Method(typeof(Building), nameof(Building.isActionableTile)), postfix: new HarmonyMethod(typeof(BuildingPatcher_patchIsActionableTile), nameof(BuildingPatcher_patchIsActionableTile.Postfix)));
			harmonyInstance.Patch(original: AccessTools.Method(typeof(Building), nameof(Building.resetTexture)), prefix: new HarmonyMethod(typeof(BuildingPatcher_patchResetTexture), nameof(BuildingPatcher_patchResetTexture.Prefix)));
			harmonyInstance.Patch(original: AccessTools.Method(typeof(Multiplayer), nameof(Multiplayer.addPlayer)), prefix: new HarmonyMethod(typeof(PlayerCreateListener), nameof(PlayerCreateListener.Prefix)));
		}
	}
}
