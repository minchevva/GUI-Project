using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor

		public DialogProcessor()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Избран елемент.
		/// </summary>
		private List<ShapeM> selection = new List<ShapeM>();
		public List<ShapeM> Selection
		{
			get { return selection; }
			set { selection = value; }
		}

		private bool isDragging;
		public bool IsDragging
		{
			get { return isDragging; }
			set { isDragging = value; }
		}


		private PointF lastLocation;
		public PointF LastLocation
		{
			get { return lastLocation; }
			set { lastLocation = value; }
		}

		
		private List<ShapeM> copiedShapeMList = new List<ShapeM>();
		public List<ShapeM> CopiedShapeMList
		{
			get { return copiedShapeMList; }
			set { copiedShapeMList = value; }
		}
		#endregion
		public Color currentStrokeColor = Color.Red;
		public Color currentFillColor = Color.BlueViolet;
		public double currentOpac = 1;
		public float currentWidth = 1;
		public Color currentSelectedColor = Color.Red;

		public void ChangeGroupFillColor(Color color)
		{
			foreach (GroupShapeM shapeM in Selection)
			{
				shapeM.ChangeGroupFillColor(color);
			}
		}

		public void ChangeGroupStrokeColor(Color color)
		{
			foreach (GroupShapeM shapeM in Selection)
			{
				shapeM.ChangeGroupStrokeColor(color);
			}
		}

		public void ChangeGroupStrokeWidth(float num)
		{
			foreach (GroupShapeM shapeM in Selection)
			{
				shapeM.ChangeGroupStrokeWidth(num);
			}
		}

		public void ChangeGroupOpacity(int opac)
		{
			foreach (GroupShapeM shapeM in Selection)
			{
				shapeM.ChangeGroupOpacity(opac);
			}
		}
		public void AddRandomFigNine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			FigNine nine = new FigNine(new Rectangle(x, y, 100, 100));


		 nine.FillColor = Color.White;
			nine.StrokeColor = Color.Black;
			nine.BorderWidth = 2;
			nine.ShapeAngle = 45;

			ShapeMList.Add(nine);
		}
		public void AddDumbRectangleM()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			RectangleShapeM rect = new RectangleShapeM(new Rectangle(x, y, 100, 200));
			rect.Opacity = (int)currentOpac * 255;
			rect.FillColor = currentFillColor;
			rect.StrokeColor = currentStrokeColor;
			rect.BorderWidth = currentWidth;
			ShapeMList.Add(rect);
		}

		public void AddDumbEllipseM()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShapeM ellipse = new EllipseShapeM(new Rectangle(x, y, 100, 200));
			ellipse.Opacity = (int)currentOpac * 255;
			ellipse.FillColor = currentFillColor;
			ellipse.StrokeColor = currentStrokeColor;
			ellipse.BorderWidth = currentWidth;
			ShapeMList.Add(ellipse);
		}

		public void AddDumbPointM()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);
			PointShapeM point = new PointShapeM(new Rectangle(x, y, 3, 3));
			point.FillColor = currentFillColor;
			point.Opacity = (int)currentOpac * 255;
			point.BorderWidth = currentWidth;
			ShapeMList.Add(point);
		}

		public void AddDumbLineM()
		{
			Random rnd = new Random();
			int x1 = rnd.Next(100, 1000);
			int y1 = rnd.Next(100, 600);
			LineShapeM line = new LineShapeM(new Rectangle(x1, y1, 300, 300));
			line.Opacity = (int)currentOpac * 255;
			line.BorderWidth = currentWidth;
			line.FillColor = currentFillColor;
			ShapeMList.Add(line);
		}

		public void AddDumbPolygonM()
		{
			Random rnd = new Random();
			int firstX = rnd.Next(50, 550);
			int firstY = rnd.Next(150, 500);
			PointF one = new PointF(firstX, firstY);
			PointF two = new PointF(firstX - 30, firstY - 85);
			PointF three = new PointF(firstX + 50, firstY - 140);
			PointF four = new PointF(firstX + 125, firstY - 85);
			PointF five = new PointF(firstX + 100, firstY);
			PointF six = new PointF(firstX, firstY);
			PolygonShapeM polygon = new PolygonShapeM(one, two, three, four, five, six);
			polygon.Opacity = (int)currentOpac * 255;
			polygon.FillColor = currentFillColor;
			polygon.StrokeColor = currentStrokeColor;
			polygon.BorderWidth = currentWidth;

			ShapeMList.Add(polygon);
		}


		public void AddDumbCircleM()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			CircleShapeM circle = new CircleShapeM(new Rectangle(x, y, 200, 200));
			circle.Opacity = (int)currentOpac * 255;
			circle.FillColor = currentFillColor;
			circle.StrokeColor = currentStrokeColor;
			circle.BorderWidth = currentWidth;
			ShapeMList.Add(circle);
		}



		//Serialization method
		public void SerializeFile(object currentObject, string path = null)
		{

			Stream stream;
			IFormatter binaryFormatter = new BinaryFormatter();
			if (path == null)
			{
				stream = new FileStream("DrawFile.asd", FileMode.Create, FileAccess.Write, FileShare.None);
			}
			else
			{
				string preparePath = path + ".asd";
				stream = new FileStream(preparePath, FileMode.Create);

			}
			binaryFormatter.Serialize(stream, currentObject);
			stream.Close();
		}


		//Deserialization method
		public object DeSerializeFile(string path = null)
		{
			object currentObject;

			Stream stream;
			IFormatter binaryFormatter = new BinaryFormatter();
			if (path == null)
			{
				stream = new FileStream("DrawFile.asd", FileMode.Open);

			}
			else
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			}
			currentObject = binaryFormatter.Deserialize(stream);
			stream.Close();
			return currentObject;
		}


		//Delete Btn method
		public void DeleteSelectedShapeMs()
		{
			foreach (ShapeM shapeM in Selection)
			{
				ShapeMList.Remove(shapeM);

			}
			Selection.Clear();
		}

		public void CopySelectedShapeMs()
		{
			CopiedShapeMList.Clear();
			foreach (ShapeM shapeM in Selection)
			{
				CopiedShapeMList.Add(shapeM);

			}
			//Selection.Clear();
		}

		public void PasteSelectedShapeMs()
		{

			foreach (ShapeM shapeM in CopiedShapeMList)
			{
				var type = shapeM.GetType().Name.ToString();
				if (type.Equals("CircleShapeM"))
				{
					AddDumbCircleM();
				}
				else if (type.Equals("EllipseShapeM"))
				{
					AddDumbEllipseM();
				}
				else if (type.Equals("LineShapeM"))
				{
					AddDumbLineM();
				}
				else if (type.Equals("PointShapeM"))
				{
					AddDumbPointM();
				}
				else if (type.Equals("RectangleShapeM"))
				{
					AddDumbRectangleM();
				}


			}

		}

		public void RotateShapeM(float rotateAngle)
		{
			if (Selection.Count > 0)
			{
				foreach (ShapeM item in Selection)
				{
					if (item.GetType().Name == "GroupShape")
					{
						selection.Add(item);
						foreach (GroupShapeM gs in Selection)
						{
							foreach (ShapeM item2 in gs.groupedShapeM)
							{
								item2.ShapeAngle = rotateAngle;

							}
						}
						selection.Remove(item);
					}
					else
					{
						item.ShapeAngle = rotateAngle;

					}

				}

			}
		}

		public void SelectAllShapeMs()
		{
			foreach (ShapeM shapeM in ShapeMList)
			{
				Selection.Add(shapeM);
			}
		}

		public void GroupSelectedShapeMs()
		{
			//checking if at least 2 shapeMs are selected
			if (Selection.Count < 2) return;

			float minimalX = 10000;
			float minimalY = 10000;
			float maximalX = -10000;
			float maximalY = -10000;
			foreach (var shapeM in Selection)
			{
				if (maximalX < shapeM.Location.X + shapeM.Width)
				{
					maximalX = shapeM.Location.X + shapeM.Width;
				}
				if (maximalY < shapeM.Location.Y + shapeM.Height)
				{
					maximalY = shapeM.Location.Y + shapeM.Height;
				}

				if (minimalX > shapeM.Location.X)
				{
					minimalX = shapeM.Location.X;
				}
				if (minimalY > shapeM.Location.Y)
				{
					minimalY = shapeM.Location.Y;
				}
			}

			var groupShapeM = new GroupShapeM(new RectangleF(minimalX, minimalY, maximalX - minimalX, maximalY - minimalY));
			groupShapeM.groupedShapeM = Selection;
			//Removing shapeMs from the ShapeMList as they become one
			foreach (var shapeM in Selection)
			{
				ShapeMList.Remove(shapeM);
			}

			Selection = new List<ShapeM>();
			ShapeMList.Add(groupShapeM);
			Selection.Add(groupShapeM);

		}

		public void ResizeShapeM(float width, float height)
		{
			foreach (var item in Selection)

			{
				if (width != -1)
				{
					if (item.GetType().Equals(typeof(GroupShapeM)))
					{
						item.GroupResizeWidth(width);
					}
					else
					{
						item.Width = width;
					}
				}
				if (height != -1)
				{
					if (item.GetType().Equals(typeof(GroupShapeM)))
					{
						item.GroupResizeHeight(height);
					}
					else
					{
						item.Height = height;
					}
				}
			}
		}
		

		public void UnGroupSelectedShapeMs()
		{
			List<ShapeM> allShapesInGroup = new List<ShapeM>();
			List<ShapeM> allShapeMMsInGroup = new List<ShapeM>();

			foreach (GroupShapeM groupShape in Selection)
			{
				foreach (var shape in groupShape.groupedShapeM)
				{
					allShapesInGroup.Add(shape);
				}
				groupShape.groupedShapeM.Clear();
				allShapeMMsInGroup.Add(groupShape);
				
				
				foreach (var shape in allShapesInGroup)
				{
					Selection.Remove(shape);
					ShapeMList.Add(shape);
				}
			}
			foreach(ShapeM item in allShapeMMsInGroup)
            {
				Selection.Remove(item);
			}
			allShapeMMsInGroup.Clear();
			
		}


		public ShapeM ContainsPoint(PointF point)
		{
			for (int i = ShapeMList.Count - 1; i >= 0; i--)
			{
				if (ShapeMList[i].Contains(point))
				{
					ShapeMList[i].FillColor = currentSelectedColor;

					return ShapeMList[i];
				}
			}
			return null;
		}

		public void TranslateTo(PointF p)
		{
			foreach (ShapeM shapeM in Selection)
			{
				var type = shapeM.GetType().Name.ToString();
				if (type.Equals("GroupShapeM"))
				{
					shapeM.MoveGroupedShapeM(p.X - lastLocation.X, p.Y - lastLocation.Y);
				}
				else
				{
					shapeM.Location = new PointF(shapeM.Location.X + p.X - lastLocation.X, shapeM.Location.Y + p.Y - lastLocation.Y);
				}


			}

			lastLocation = p;
		}
		public void WriteShapeListToFile(object obj, string path = null)
        {
			Stream stream;
			IFormatter formatter = new BinaryFormatter();
			if (path == null)
			{
				stream = new FileStream("DrawFile.asd", FileMode.Create, FileAccess.Write, FileShare.None);
			}
			else
			{
				string preparePath = path + ".asd";
				stream = new FileStream(preparePath, FileMode.Create);

			}
			formatter.Serialize(stream, obj);
			stream.Close();
		}
		public object LoadShapeListFromFile(string path = null)
        {
			object obj;

			Stream stream;
			IFormatter binaryFormatter = new BinaryFormatter();
			if (path == null)
			{
				stream = new FileStream("DrawFile.asd", FileMode.Open);
			}
			else
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			}
			obj = binaryFormatter.Deserialize(stream);
			stream.Close();
			return obj;
		}

				}

			}



