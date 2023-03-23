using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
  
		[Serializable]
		public class CircleShapeM : ShapeM
		{
			#region Constructor

			public CircleShapeM(RectangleF rect) : base(rect)
			{
			}

			public CircleShapeM(EllipseShapeM circle) : base(circle)
			{
			}

			#endregion


			public override bool Contains(PointF point)
			{
				if (base.Contains(point))
				{
					float a = Width / 2;
					float b = Height / 2;
					float x1 = Location.X + a;
					float y1 = Location.Y + b;
					bool isItOn = (Math.Pow((point.X - x1) / a, 2) + Math.Pow((point.Y - y1) / b, 2) - 1) <= 0;
					return isItOn;
				}

				else
				{
					return false;
				}


			}

			/// <summary>
			/// Частта, визуализираща конкретния примитив.
			/// </summary>
			public override void DrawSelf(Graphics grfx)
			{
				base.DrawSelf(grfx);

				//grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
				grfx.FillEllipse(new SolidBrush(Color.FromArgb(Opacity, FillColor)), new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Width));
				//grfx.DrawRectangle(Pens.Black, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
				grfx.DrawEllipse(new Pen(StrokeColor,BorderWidth), new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Width));

			}
		}
	}
