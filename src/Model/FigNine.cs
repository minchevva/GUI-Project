using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
	[Serializable]
	public class FigNine:ShapeM
	{
		#region Constructor

		public FigNine(RectangleF rect) : base(rect)
		{
		}

		public FigNine(RectangleShapeM rectangle) : base(rectangle)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);
			base.RotateShapeM(grfx);


			grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(StrokeColor, BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

			float c = (Rectangle.X + Rectangle.Width / 4) - (Location.X + (Width / 2));
			float y = (float)((Location.Y - (-Height / 2)) + Math.Sqrt((Math.Pow(Height / 2, 2)) * (1 - Math.Pow(c, 2) / Math.Pow(Width / 2, 2))));
			
			PointF p1 = new PointF(Rectangle.X, Rectangle.Y + Rectangle.Height / 2);
			PointF p2 = new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height / 2);

			PointF p3 = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y);
			PointF p4 = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height);


			grfx.DrawLine(new Pen(StrokeColor, BorderWidth), p1, p2);
			grfx.DrawLine(new Pen(StrokeColor, BorderWidth), p3, p4);

			grfx.ResetTransform();

		}
	}
}
