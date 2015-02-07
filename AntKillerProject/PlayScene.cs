#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.Gestures;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Resources;
using WaveEngine.Framework.Services;
using WaveEngine.Components.Animation;
using WaveEngine.Framework.Animation;
using WaveEngine.Framework.Physics2D;

#endregion

namespace AntKillerProject
{

	public class PlayScene : Scene
	{
		protected override void CreateScene ()
		{
			#region Simple test
			//Create a 3D camera
			var camera3D = new FreeCamera ("Camera3D", new Vector3 (0, 2, 4), Vector3.Zero) { BackgroundColor = Color.CornflowerBlue };
			EntityManager.Add (camera3D);

			// Create a 2D camera
			var camera2D = new FixedCamera2D ("Camera2D") { ClearFlags = ClearFlags.DepthAndStencil }; // Transparent background need this clearFlags.
			EntityManager.Add (camera2D);


			// Generator
			int time = WaveServices.Random.Next(1, 5);
			WaveServices.TimerFactory.CreateTimer("AntGenerator", TimeSpan.FromSeconds((float)time), () => 
				{
					Ant ant = new Ant();
					EntityManager.Add(ant.GetEntity());
				}, true);

			int timeCockroach = WaveServices.Random.Next(4, 6);
			WaveServices.TimerFactory.CreateTimer("CockroachGenerator", TimeSpan.FromSeconds((float)timeCockroach), () => 
				{
					Cockroach cockroach = new Cockroach();
					EntityManager.Add(cockroach.GetEntity());
				}, true);

			#endregion
		}

		protected override void Start ()
		{
			base.Start ();

			// This method is called after the CreateScene and Initialize methods and before the first Update.
		}

	}
}

