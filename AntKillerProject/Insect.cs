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

namespace AntKillerProject
{
	public class Insect
	{
		private Entity m_Entity;
		private int m_Type;
		private float m_Speed;
		private int m_Score;
		private string m_SpriteName;

		public Insect(int type, float speed, int score, string spriteName)
		{
			m_Type = type;
			m_Speed = speed;
			m_Score = score;
			m_SpriteName = spriteName;

			int x = WaveServices.Random.Next (0, 768);
			string spritePath = m_SpriteName + ".wpkk";
			string spriteMap = m_SpriteName + ".xml";

			m_Entity = new Entity ()
				.AddComponent (
				new Transform2D () {
					Origin = Vector2.Center,
					X = x,
					Y = 0,
				})
				.AddComponent (new Sprite (spritePath))
				.AddComponent (Animation2D.Create<TexturePackerGenericXml> (spriteMap)
					.Add ("Run", new SpriteSheetAnimationSequence () {
						First = 2,
						Length = 2,
						FramesPerSecond = 10
					})
					.Add ("Die", new SpriteSheetAnimationSequence () {
						First = 1,
						Length = 1,
						FramesPerSecond = 5
					})
				)
				.AddComponent (new AnimationUI ())
				.AddComponent (new TouchGestures () {
				EnabledGestures = SupportedGesture.Translation
			})
				.AddComponent (new RectangleCollider())
				.AddComponent (new InsectBehavior ())
				.AddComponent (new AnimatedSpriteRenderer (DefaultLayers.Debug));

			var move = new SingleAnimation(0f, WaveServices.Platform.ScreenHeight, 1f/m_Speed, EasingFunctions.Cubic );
			AnimationUI animation = m_Entity.FindComponent<AnimationUI>();
			animation.BeginAnimation(Transform2D.YProperty, move);

			var anim2D = m_Entity.FindComponent<Animation2D>();
			anim2D.Play(true);

			var insectBehavior = m_Entity.FindComponent<InsectBehavior> ();
			insectBehavior.SetState (InsectBehavior.AnimState.Run);
		}

		public Entity GetEntity()
		{
			return m_Entity;
		}

		public int GetInsectType()
		{
			return m_Type;
		}

		public int GetScore()
		{
			return m_Score;
		}
	}
}

