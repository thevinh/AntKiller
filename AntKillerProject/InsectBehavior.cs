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

namespace AntKillerProject
{
	public class InsectBehavior : Behavior
	{
		private enum AnimState{	Run, Die };
		private AnimState currentState, lastState;

		public InsectBehavior () : base("InsectBehavior")
		{

		}

		protected override void Update(TimeSpan gamTime)
		{
			var touches = WaveServices.Input.TouchPanelState;
			RectangleCollider collider = this.Owner.FindComponent<RectangleCollider> ();
			// Touch
			var touch = this.Owner.FindComponent<TouchGestures>();
			touch.TouchPressed += (s,o) => 
			{
				Console.WriteLine("Click Ant");
				this.EntityManager.Remove (this.Owner);
			};

			if (this.Owner.FindComponent<Transform2D> ().Y >= 1280) {
				this.EntityManager.Remove (this.Owner);
			}
		}
	}
}

