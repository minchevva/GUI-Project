using System;
using System.Collections.Generic;
using System.Drawing;
/*using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace Draw.src.Model
{
	[Serializable]
	public class GroupShapeM : ShapeM
    {
		
		
			#region Constructor

			public GroupShapeM(RectangleF rect) : base(rect)
			{
			}

			public GroupShapeM(RectangleShapeM rectangle) : base(rectangle)
			{
			}

			#endregion

			public List<ShapeM> groupedShapeM = new List<ShapeM>();
		
			public override bool Contains(PointF point)
			{
				if (base.Contains(point))
				{
					foreach (ShapeM item in groupedShapeM)
					{
						if (item.Contains(point)) return true;

					}

					return true;
				}
				else
				{
					return false;
				}
			}


			public override void MoveGroupedShapeM(float dx, float dy)
			{
				base.MoveGroupedShapeM(dx, dy);
				foreach (var shape in groupedShapeM)
				{
					shape.MoveGroupedShapeM(dx * 2, dy * 2);
				}
			}

			public override void ChangeGroupFillColor(Color color)
			{
				base.ChangeGroupFillColor(color);
				foreach (var shape in groupedShapeM)
				{
					shape.FillColor = color;
				}
			}
			public override void ChangeGroupStrokeColor(Color color)
			{
				base.ChangeGroupStrokeColor(color);
				foreach (var shape in groupedShapeM)
				{
					shape.StrokeColor = color;
				}
			}
		public override void ChangeGroupStrokeWidth(float borderWidthShape)
			{
				base.ChangeGroupStrokeWidth(borderWidthShape);
				foreach (var shape in groupedShapeM)
				{
					shape.BorderWidth = borderWidthShape;
				}
			}
			public override void ChangeGroupOpacity(int opacity)
			{
				base.ChangeGroupOpacity(opacity);
				foreach (var shape in groupedShapeM)
				{
					shape.Opacity = opacity;
				}
			}

			public override void ChangeGroupRotate(float angle)
			{
				base.ChangeGroupRotate(angle);
				foreach (var shape in groupedShapeM)
				{
					shape.ShapeAngle = angle;
				}
			}


			public override void GroupResizeWidth(float width)
			{
				base.GroupResizeWidth(width);
				float maxX = float.NegativeInfinity;
				float minX = float.PositiveInfinity;
				foreach (var item in groupedShapeM)
				{
					item.Width = width;
					if (minX > item.Location.X)
					{
						minX = item.Location.X;
					}
					if (maxX < item.Location.X + item.Width)
					{
						maxX = item.Location.X + item.Width;
					}

				}
				this.Rectangle = new RectangleF(minX, this.Rectangle.Y, maxX - minX, this.Rectangle.Height);
			}
			public override void GroupResizeHeight(float height)
			{
				base.GroupResizeHeight(height);
				float maxY = float.NegativeInfinity;
				float minY = float.PositiveInfinity;
				foreach (var item in groupedShapeM)
				{
					item.Height = height;
					if (minY > item.Location.Y)
					{
						minY = item.Location.Y;
					}
					if (maxY < item.Location.Y + item.Height)
					{
						maxY = item.Location.Y + item.Height;
					}

				}
				this.Rectangle = new RectangleF(this.Rectangle.X, minY, this.Rectangle.Width, maxY - minY);

			}

			/// <summary>
			/// Частта, визуализираща конкретния примитив.
			/// </summary>
			public override void DrawSelf(Graphics grfx)
			{
				base.DrawSelf(grfx);

				foreach (ShapeM item in groupedShapeM)
				{
					item.DrawSelf(grfx);
				}


			}
		}
	}

