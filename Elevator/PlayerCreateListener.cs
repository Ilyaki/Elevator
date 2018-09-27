﻿using StardewValley;
using StardewValley.Network;
using System;

namespace Elevator
{
	class PlayerCreateListener : Patch
	{
		protected override PatchDescriptor GetPatchDescriptor() => new PatchDescriptor(typeof(Multiplayer), "addPlayer");

		public static bool Prefix(NetFarmerRoot f)
		{
			if (f.Value.Name.Length == 0)
			{
				//A new player has joined. If there is less then 10 spots availible, mark up until 10
				int emptyPlaces = 0;
				foreach (Farmer player in Game1.getAllFarmhands())
					if (player.Name.Length == 0)
						emptyPlaces++;

				Console.WriteLine($"Generating {10 - emptyPlaces} new cabins");

				if (emptyPlaces < 10)
					for (int i = 0; i <= 10 - emptyPlaces; i++)//Make up to 10
						CabinHelper.AddNewCabin(Game1.random.Next(1, 4));
			}

			return true;
		}
	}
}
