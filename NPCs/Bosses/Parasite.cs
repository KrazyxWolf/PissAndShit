using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace PissAndShit.NPCs.Bosses
{
	internal class ParasiteHead : ModNPC
	{
		public static int health = 0;
		public bool flies = true;
		public float speed = 60f;
		public float turnSpeed = 30f;
		public int minLength = 20;
		public int maxLength = 20;
		bool TailSpawned = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Festering Parasite");
		}


		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.EaterofWorldsHead);
			npc.aiStyle = 6;
			npc.damage = 100;
			npc.defense = 35;
			npc.lifeMax = 150000;
			npc.width = 50;
			npc.height = 50;
			npc.lavaImmune = true;
			npc.value = Item.buyPrice(0, 1, 75, 0);
			npc.boss = true;
            	music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/POOP_WORM");
          		musicPriority = MusicPriority.BossHigh;
		}
		public override bool PreAI()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{

				if (npc.ai[0] == 0)
				{

					npc.realLife = npc.whoAmI;

					int latestNPC = npc.whoAmI;


					for (int i = 0; i < maxLength; ++i)
					{

						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Sanswormbody"), npc.whoAmI, 0, latestNPC);
						Main.npc[(int)latestNPC].realLife = npc.whoAmI;
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
					}

					latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Sanswormtail"), npc.whoAmI, 0, latestNPC);
					Main.npc[(int)latestNPC].realLife = npc.whoAmI;
					Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
					TailSpawned = true;

					npc.ai[0] = 1;
					npc.netUpdate = true;
				}
			}
			return true;
		}

		internal class ParasiteBody : ModNPC
		{
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("Festering Parasite");
			}
			public override void SetDefaults()
			{
				npc.CloneDefaults(NPCID.EaterofWorldsBody);
				npc.aiStyle = 6;
				npc.damage = 60;
				npc.defense = 10;
				npc.width = 50;
				npc.height = 50;
				npc.lavaImmune = true;
				npc.boss = true;

			}
				public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
			{

				return false;       
			}

		}
	}

	internal class ParasiteTail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Festering Parasite");
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.EaterofWorldsTail);
			npc.aiStyle = 6;
			npc.damage = 100;
			npc.defense = 35;
			npc.width = 50;
			npc.height = 50;
			npc.lavaImmune = true;
			npc.boss = true;
		}

			public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{

			return false;       
		}
	}
}
