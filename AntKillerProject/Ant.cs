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
	public class Ant : Insect
	{
		public Ant () : base(Constant.AntType, Constant.AntSpeed, Constant.AntScore, Constant.AntSpriteName)
		{
			this.GetEntity ().FindComponent<Transform2D> ().Scale = new Vector2 (0.5f, 0.5f);
		}
	}
}

