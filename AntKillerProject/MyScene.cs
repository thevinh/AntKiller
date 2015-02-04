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

	public class MyScene : Scene
	{
		protected override void CreateScene ()
		{
			//Insert your scene definition here.

			#region Simple test
			//Create a 3D camera
			var camera3D = new FreeCamera ("Camera3D", new Vector3 (0, 2, 4), Vector3.Zero) { BackgroundColor = Color.CornflowerBlue };
			EntityManager.Add (camera3D);

			// Create a 2D camera
			var camera2D = new FixedCamera2D ("Camera2D") { ClearFlags = ClearFlags.DepthAndStencil }; // Transparent background need this clearFlags.
			EntityManager.Add (camera2D);

			// Draw a simple sprite
			Entity ant = new Entity ()
				.AddComponent (new Transform2D (){
					Origin = Vector2.Center,
					X = 100,
					Y = 0,
				})
				.AddComponent (new Sprite ("antSprite.wpk"))
				.AddComponent(Animation2D.Create<TexturePackerGenericXml>("antSprite.xml")
					.Add("run", new SpriteSheetAnimationSequence()
						{
							First = 1,
							Length = 2,
							FramesPerSecond = 11
						}))
				.AddComponent (new AnimationUI())
				.AddComponent (new RectangleCollider())
				.AddComponent (new TouchGestures(){ EnabledGestures = SupportedGesture.Translation } )
				.AddComponent (new InsectBehavior())
				.AddComponent (new AnimatedSpriteRenderer());

			EntityManager.Add (ant);

			var move = new SingleAnimation(0f, WaveServices.Platform.ScreenHeight, 5.0f, EasingFunctions.Cubic );

			AnimationUI animation = ant.FindComponent<AnimationUI>();
			animation.BeginAnimation(Transform2D.YProperty, move);
			var anim2D = ant.FindComponent<Animation2D>();
			anim2D.Play(true);

			#endregion
		}

		protected override void Start ()
		{
			base.Start ();

			// This method is called after the CreateScene and Initialize methods and before the first Update.
		}

	}
}

