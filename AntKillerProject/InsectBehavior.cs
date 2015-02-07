using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Animation;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Animation;

namespace AntKillerProject
{
	public class InsectBehavior : Behavior
	{
		public enum AnimState { Run, Die, BeforeDesTroy, Destroy };
		private AnimState currentState;

		[RequiredComponent]
		public Animation2D anim2D;
		[RequiredComponent]
		public Transform2D trans2D;
		[RequiredComponent]
		public AnimationUI animUI;

		public InsectBehavior () : base("InsectBehavior")
		{
			this.anim2D = null;
			this.currentState = AnimState.Run;
		}

		public void SetState(AnimState state)
		{
			currentState = state;
		}

		protected override void Update(TimeSpan gamTime)
		{
			// Touch
			var touch = this.Owner.FindComponent<TouchGestures>();
			touch.TouchPressed += (s,o) => 
			{
				this.currentState = AnimState.Die;
			};

			if (this.Owner.FindComponent<Transform2D> ().Y >= 1280) {
				this.currentState = AnimState.Destroy;
			}

			if (currentState == AnimState.Die) {
				// display the die sprite
				anim2D.CurrentAnimation = "Die";
				trans2D.Effect = SpriteEffects.FontSprite;
				anim2D.Play ();
				this.Owner.RemoveComponent<AnimationUI> ();
				// play animation blur
				var dieing = new SingleAnimation (1f, 0f, 0.8f, EasingFunctions.Cubic);
				this.Owner.AddComponent( new AnimationUI());
				animUI = this.Owner.FindComponent<AnimationUI>();
				animUI.BeginAnimation (Transform2D.OpacityProperty, dieing);
				currentState = AnimState.BeforeDesTroy;
			}

			if(currentState == AnimState.BeforeDesTroy && this.trans2D.Opacity == 0	) {
				currentState = AnimState.Destroy;
			}

			if (currentState == AnimState.Destroy) {
				this.EntityManager.Remove (this.Owner);
			}
		}
	}
}

