using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SharpGL;
using System.Diagnostics;

namespace _19127644_Lab03
{
    public partial class Form1 : Form
    {

        //Biến đánh dấu đang vẽ hình
        bool isDrawing = false;
        //Biến đanh dấu đang vẽ đa giác
        bool isPolygonDrawing = false;
        //Biến đánh dấu đang biến đổi Affine
        bool isTransforming = false;
        //Biến đánh dấu đang xoay hình hay co giãn hình, 0 là rotate, 1 là scale
        int RotateOrScale = 0;

        //Màu đang chọn
        Color userColor = Color.White;
        //Độ dày nét vẽ đang chọn
        float userWidth = 1.0f;
        //Loại hình vẽ đang chọn để vẽ
        DrawGL.shapeType userType = DrawGL.shapeType.NONE;

        //Tọa độ chuột
        Point pStart = new Point(0, 0), pEnd = new Point(0, 0);
        //Tập các hình đã vẽ
        List<DrawGL> shapes = new List<DrawGL>();
        //Biến đánh dấu tập hình vẽ bị thay đổi => phải vẽ lại
        bool isShapesChanged = true;

        //Sai số pixel bình phương, dùng trong chức năng chọn lại hình
        const double epsilon = 50.0;
        //Số thứ tự của hình đang được chọn lại, -1 là không chọn gì
        int choosingShape = -1;
        //Backup cho hình đang được chọn
        DrawGL backupShape;
        //Số thứ tự của điểm vẽ đang được chọn lại, -1 là không chọn gì
        int choosingRaster = -1;
        //Số thứ tự của điểm điều khiển đang được chọn lại, -2 là không chọn gì, -1 là chọn điểm xoay và co giãn
        int choosingControl = -2;

		//Tạo biến thời gian thực
        System.Windows.Forms.Timer tmr = null;

		public Form1()
        {
            InitializeComponent();

			//Cài đặt màu vừa vào chương trình là màu trắng
			userColor = Color.White;

			//Cài đặt giờ
			tmr = new System.Windows.Forms.Timer();
			tmr.Interval = 1000;
			tmr.Tick += new EventHandler(timer1_Tick);
			tmr.Enabled = true;
		}

        private void ClearBtn_Click(object sender, EventArgs e)
        {
			shapes.Clear();
			isDrawing = isPolygonDrawing = false;
			choosingShape = choosingControl = choosingRaster = -1;
			isShapesChanged = true;
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
        }

        private void LineBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.LINE;
			LbMode.Text = "Mode: Line";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void CircleBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.CIRCLE;
			LbMode.Text = "Mode: Circle";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void RectangleBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.RECTANGLE;
			LbMode.Text = "Mode: Rectangle";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void EllipseBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.ELLIPSE;
			LbMode.Text = "Mode: Ellipse";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void TriangleBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.TRIANGLE;
			LbMode.Text = "Mode: Triangle";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void PentagonBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.PENTAGON;
			LbMode.Text = "Mode: Pentagon";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void HexagonBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.HEXAGON;
			LbMode.Text = "Mode: Hexagon";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void PolygonBtn_Click(object sender, EventArgs e)
        {
			userType = DrawGL.shapeType.POLYGON;
			LbMode.Text = "Mode: Polygon";
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = false;
		}

        private void ScanlineBtn_Click(object sender, EventArgs e)
        {
			if (shapes[choosingShape].type != DrawGL.shapeType.CIRCLE && shapes[choosingShape].type != DrawGL.shapeType.ELLIPSE && shapes[choosingShape].controlPoints.Count < 3)
				return;

			if (choosingShape >= 0 && shapes[choosingShape].fillColor != userColor)
			{
				Thread thread = new Thread
				(
					() => Scanline.Fill(shapes[choosingShape], userColor, ref isShapesChanged)
				);
				thread.IsBackground = true;
				thread.Start();
			}
		}

        private void FloodFillBtn_Click(object sender, EventArgs e)
        {
			if (choosingShape >= 0 && shapes[choosingShape].fillColor != userColor)
			{
				if (shapes[choosingShape].type != DrawGL.shapeType.CIRCLE && shapes[choosingShape].type != DrawGL.shapeType.ELLIPSE && shapes[choosingShape].controlPoints.Count < 3)
					return;

				Thread thread = new Thread
				(
					() => FloodFill.Fill(shapes[choosingShape], userColor, ref isShapesChanged)
				);
				thread.IsBackground = true;
				thread.Start();
			}
		}

        private void ColorBtn_Click(object sender, EventArgs e)
        {
			if (colorDialog1.ShowDialog() == DialogResult.OK)
				userColor = colorDialog1.Color;
		}

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {

            //Lấy đối tượng OpenGL
            OpenGL gl = openGLControl1.OpenGL;

            //Set màu nền (đen)
            gl.ClearColor(0, 0, 0, 1);

            //Xóa toàn bộ drawBoard
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

			WidthBtn.Text = "Width: 1.0";
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            //Lấy đối tượng OpenGL
            OpenGL gl = openGLControl1.OpenGL;

            //Set viewport theo kích thước mới
            gl.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);

            //Chọn chế độ chiếu
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //Chiếu theo kích thước mới
            gl.LoadIdentity();
            gl.Ortho2D(0, openGLControl1.Width, 0, openGLControl1.Height);

            //Vẽ lại hình
            isShapesChanged = true;
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
			//Lấy đối tượng OpenGL
			OpenGL gl = openGLControl1.OpenGL;

			//Chỉ vẽ khi tập hình vẽ bị thay đổi
			if (isShapesChanged)
			{
				//Xóa toàn bộ drawBoard
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

				//Vẽ lại tất cả hình
				if (shapes.Count > 0)
				{
					for (int i = 0; i < shapes.Count - 1; i++)
					{
						shapes[i].Draw(gl);

						if (shapes[i].fillColor != Color.Black)
							shapes[i].Fill(gl);
					}

					//Đo thời gian vẽ hình cuối cùng
					Stopwatch watch = Stopwatch.StartNew();
					shapes.Last().Draw(gl);
					watch.Stop();
					//tbTime.Text = watch.ElapsedTicks.ToString() + " ticks";

					if (shapes.Last().fillColor != Color.Black)
						shapes.Last().Fill(gl);
				}

				gl.Flush();
				isShapesChanged = false;
			}

			//Vẽ điểm điều khiển của hình đang được chọn lại
			if (choosingShape >= 0)
			{
				gl.PointSize(5.0f);
				gl.Begin(OpenGL.GL_POINTS);

				//Điểm điều khiến
				gl.Color(230.0, 230.0, 0);
				for (int i = 0; i < shapes[choosingShape].controlPoints.Count; i++)
					gl.Vertex(shapes[choosingShape].controlPoints[i].X, gl.RenderContextProvider.Height - shapes[choosingShape].controlPoints[i].Y);

				//Điểm xoay và co giãn
				gl.Color(0, 100.0, 100.0);
				gl.Vertex(shapes[choosingShape].extraPoint.X, gl.RenderContextProvider.Height - shapes[choosingShape].extraPoint.Y);

				gl.End();
				gl.Flush();
			}
		}

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
			//Sự kiện "nhấn chuột"
			pStart = pEnd = e.Location;

			//Nhấn chuột trái => vẽ hình mới, hoặc chọn lại hình đã vẽ, hoặc biến đổi Affine
			if (e.Button == MouseButtons.Left)
			{
				//Nếu userType là NONE => chọn lại hình đã vẽ, hoặc biến đổi Affine
				if (userType == DrawGL.shapeType.NONE)
				{
					Thread thread = new Thread
					(
						delegate ()
						{
							//Nếu một hình nào đó đang được chọn => biến đổi Affine
							if (choosingShape >= 0)
							{
								//Tìm điểm điều khiển hoặc điểm xoay và co giãn gần với tọa độ chuột nhất
								double minDistance = 999999999999999999999999.0;
								int closestControl = -2;

								int dx, dy;
								double distance;

								for (int j = 0; j < shapes[choosingShape].controlPoints.Count; j++)
								{
									dx = shapes[choosingShape].controlPoints[j].X - e.Location.X;
									dy = shapes[choosingShape].controlPoints[j].Y - e.Location.Y;
									distance = dx * dx + dy * dy;

									if (distance < minDistance)
									{
										minDistance = distance;
										closestControl = j;
									}
								}

								dx = shapes[choosingShape].extraPoint.X - e.Location.X;
								dy = shapes[choosingShape].extraPoint.Y - e.Location.Y;
								distance = dx * dx + dy * dy;

								if (distance < minDistance)
								{
									minDistance = distance;
									closestControl = -1;
								}

								//Nếu khoảng cách nhỏ nhất nhỏ hơn epsilon => chọn điểm này
								if (minDistance <= epsilon)
								{
									choosingControl = closestControl;
									isTransforming = true;
									return;
								}

								//Ngược lại => hủy chọn hình và chọn lại hình khác
								choosingShape = choosingRaster = -1;
								choosingControl = -2;
							}

							//Nếu không hình nào đang được chọn => chọn lại hình
							if (choosingShape == -1)
							{
								//Tìm điểm vẽ gần với tọa độ chuột nhất
								double minDistance = 999999999999999999999999.0;
								int closestRaster = -1;

								int dx, dy;
								double distance;

								for (int i = 0; i < shapes.Count; i++)
									for (int j = 0; j < shapes[i].rasterPoints.Count; j++)
									{
										dx = shapes[i].rasterPoints[j].X - e.Location.X;
										dy = shapes[i].rasterPoints[j].Y - e.Location.Y;
										distance = dx * dx + dy * dy;

										if (distance < minDistance)
										{
											choosingShape = i;
											minDistance = distance;
											closestRaster = j;
										}
									}

								//Nếu khoảng cách nhỏ nhất nhỏ hơn epsilon => chọn hình này
								if (minDistance <= epsilon)
								{
									choosingRaster = closestRaster;
									backupShape = shapes[choosingShape].Clone();
									isTransforming = true;
									isShapesChanged = true;
									return;
								}

								//Ngược lại => không chọn hình nào hết
								choosingShape = choosingRaster = -1;
								choosingControl = -2;
								isShapesChanged = true;
								return;
							}
						}
					);
					thread.IsBackground = true;
					thread.Start();
					return;
				}

				//Nếu userType khác NONE => vẽ hình mới
				choosingShape = -1;
				isDrawing = true;

				//Nếu userType khác POLYGON => tạo hình vẽ mới, thêm vào danh sách
				if (userType != DrawGL.shapeType.POLYGON)
				{
					shapes.Add(new DrawGL(userColor, userWidth, userType));
					shapes.Last().controlPoints.Add(pStart);
					shapes.Last().controlPoints.Add(pEnd);
				}
				//Nếu userType là POLYGON => xét 2 trường hợp bên dưới
				else if (userType == DrawGL.shapeType.POLYGON)
				{
					//Nếu bắt đầu vẽ => tạo hình vẽ mới
					if (isPolygonDrawing == false)
					{
						isPolygonDrawing = true;
						shapes.Add(new DrawGL(userColor, userWidth, userType));
						shapes.Last().controlPoints.Add(pStart);
						shapes.Last().controlPoints.Add(pEnd);
					}
					//Nếu đang vẽ => không tạo ra hình mới, mà thêm tọa độ chuột vào tập điểm điều khiển
					else
					{
						shapes.Last().controlPoints.Add(pEnd);
					}
				}
			}

			//Nhấn chuột phải => nếu đang vẽ POLYGON => kết thúc quá trình vẽ
			else if (isPolygonDrawing)
			{
				isDrawing = isPolygonDrawing = false;
			}
		}

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
			//Sự kiện "bấm bàn phím"
			if (e.KeyCode == Keys.Z && e.Control && shapes.Count > 0)
			{
				//Ctrl + Z => undo
				shapes.RemoveAt(shapes.Count - 1);
				isDrawing = isPolygonDrawing = false;
				choosingShape = choosingRaster = -1;
				choosingControl = -2;
				isShapesChanged = true;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				//Esc => hủy bỏ thao tác
				if (isDrawing)
				{
					shapes.RemoveAt(shapes.Count - 1);
					isDrawing = isPolygonDrawing = false;
					choosingShape = choosingRaster = -1;
					choosingControl = -2;
					isShapesChanged = true;
				}
				else
				{
					LbMode.Text = "Mode: Picking";
					userType = DrawGL.shapeType.NONE;
					isDrawing = isPolygonDrawing = false;
					choosingShape = choosingRaster = -1;
					choosingControl = -2;
					isShapesChanged = true;
					RotateBtn.Enabled = true;
					ZoomBtn.Enabled = true;
				}
			}
		}

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
			//Sự kiện "nhả chuột", kết thúc quá trình vẽ
			if (userType != DrawGL.shapeType.POLYGON)
				isDrawing = false;
			if (isTransforming == true)
			{
				//Nếu hình có tô màu => tô màu lại
				if (shapes[choosingShape].fillColor != Color.Black && shapes[choosingShape].fillPoints.Count == 0)
				{
					shapes[choosingShape].fillPoints.Clear();
					if (shapes[choosingShape].controlPoints.Count < 3)
					{
						Thread thread = new Thread
						(
							() => FloodFill.Fill(shapes[choosingShape], shapes[choosingShape].fillColor, ref isShapesChanged)
						);
						thread.IsBackground = true;
						thread.Start();
					}
					else
					{
						Thread thread = new Thread
						(
							() => Scanline.Fill(shapes[choosingShape], shapes[choosingShape].fillColor, ref isShapesChanged)
						);
						thread.IsBackground = true;
						thread.Start();
					}
				}

				backupShape = shapes[choosingShape].Clone();
				isTransforming = false;
			}
		}

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
			//Sự kiện "kéo chuột", xảy ra liên tục khi người dùng nhấn giữ chuột và kéo đi
			pEnd = e.Location;

			//Nếu đang vẽ => liên tục vẽ lại theo tọa độ pEnd mới
			if (isDrawing)
			{
				Thread thread = new Thread
				(
					delegate ()
					{
						//Cập nhật điểm điều kiển cuối cùng ứng với pEnd
						shapes.Last().controlPoints[shapes.Last().controlPoints.Count - 1] = pEnd;

						//Xóa tập điểm vẽ, vẽ lại tập điểm mới
						shapes.Last().rasterPoints.Clear();

						switch (userType)
						{
							case DrawGL.shapeType.LINE:
								DrawingAlgorithms.Line(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.CIRCLE:
								DrawingAlgorithms.Circle(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.RECTANGLE:
								DrawingAlgorithms.Rectangle(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.ELLIPSE:
								DrawingAlgorithms.Ellipse(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.TRIANGLE:
								DrawingAlgorithms.Triangle(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.PENTAGON:
								DrawingAlgorithms.Pengtagon(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.HEXAGON:
								DrawingAlgorithms.Hexagon(shapes.Last(), pStart, pEnd);
								break;
							case DrawGL.shapeType.POLYGON:
								DrawingAlgorithms.Polygon(shapes.Last());
								break;
						}
						//Vẽ lại hình
						isShapesChanged = true;
					}
				);
				thread.IsBackground = true;
				thread.Start();
			}

			//Nếu đang biến đổi Affine => thực hiện các phép biến đổi lên tập điểm điều khiển
			else if (isTransforming)
			{
				Thread thread = new Thread
				(
					delegate ()
					{
						//Tắt tô màu để biến đổi Affine
						if (shapes[choosingShape].isColored == true)
						{
							shapes[choosingShape].isColored = false;
							shapes[choosingShape].fillPoints.Clear();
						}

						AffineTransform transformer = new AffineTransform();

						//Nếu đang chọn điểm xoay và co giãn => phép xoay hình hoặc co giãn hình
						if (choosingControl == -1)
						{
							//Phép xoay hình
							if (RotateOrScale == 0)
							{
								//Tính góc xoay
								Tuple<double, double> vecA = new Tuple<double, double>(backupShape.extraPoint.X - backupShape.center.Item1, backupShape.extraPoint.Y - backupShape.center.Item2);
								Tuple<double, double> vecB = new Tuple<double, double>(pEnd.X - backupShape.center.Item1, pEnd.Y - backupShape.center.Item2);
								double lenA = Math.Sqrt(vecA.Item1 * vecA.Item1 + vecA.Item2 * vecA.Item2);
								double lenB = Math.Sqrt(vecB.Item1 * vecB.Item1 + vecB.Item2 * vecB.Item2);
								double phi = Math.Acos((vecA.Item1 * vecB.Item1 + vecA.Item2 * vecB.Item2) / (lenA * lenB));
								if (vecA.Item1 * vecB.Item2 - vecA.Item2 * vecB.Item1 < 0)
									phi = -phi;

								//Thiết lập phép xoay
								transformer.LoadIdentity();
								transformer.Translate(-backupShape.center.Item1, -backupShape.center.Item2);
								transformer.Rotate(phi);
								transformer.Translate(backupShape.center.Item1, backupShape.center.Item2);

								//Biến đổi tập điểm điều khiển
								for (int i = 0; i < shapes[choosingShape].controlPoints.Count; i++)
									shapes[choosingShape].controlPoints[i] = transformer.Transform(backupShape.controlPoints[i]);
							}
							//Phép co giãn hình
							else if (RotateOrScale == 1)
							{
								//Tính hệ số co giãn
								Tuple<double, double> vecA = new Tuple<double, double>(backupShape.extraPoint.X - backupShape.center.Item1, backupShape.extraPoint.Y - backupShape.center.Item2);
								Tuple<double, double> vecB = new Tuple<double, double>(pEnd.X - backupShape.center.Item1, pEnd.Y - backupShape.center.Item2);
								double sx = vecB.Item1 / vecA.Item1;
								double sy = vecB.Item2 / vecA.Item2;
								double s = Math.Max(sx, sy);

								//Thiết lập phép co giãn
								transformer.LoadIdentity();
								transformer.Translate(-backupShape.center.Item1, -backupShape.center.Item2);
								transformer.Scale(s, s);
								transformer.Translate(backupShape.center.Item1, backupShape.center.Item2);

								//Biến đổi tập điểm điều khiển
								for (int i = 0; i < shapes[choosingShape].controlPoints.Count; i++)
									shapes[choosingShape].controlPoints[i] = transformer.Transform(backupShape.controlPoints[i]);
							}
						}
						//Nếu đang chọn điểm điều khiển => thay đổi tọa độ điểm điều khiển
						else if (choosingControl >= 0)
						{
							shapes[choosingShape].controlPoints[choosingControl] = pEnd;
						}
						//Nếu đang chọn một điểm vẽ của hình => phép tịnh tiến hình
						else if (choosingRaster >= 0)
						{
							//Thiết lập phép tịnh tiến
							transformer.LoadIdentity();
							transformer.Translate(pEnd.X - pStart.X, pEnd.Y - pStart.Y);

							//Biến đổi tập điểm điều khiển
							for (int i = 0; i < shapes[choosingShape].controlPoints.Count; i++)
								shapes[choosingShape].controlPoints[i] = transformer.Transform(backupShape.controlPoints[i]);
						}

						//Xóa tập điểm vẽ, vẽ lại tập điểm mới
						shapes[choosingShape].rasterPoints.Clear();

						Point controlPoint0 = shapes[choosingShape].controlPoints[0];
						Point controlPoint1 = shapes[choosingShape].controlPoints[1];

						switch (shapes[choosingShape].type)
						{
							case DrawGL.shapeType.LINE:
								DrawingAlgorithms.Line(shapes[choosingShape], controlPoint0, controlPoint1);
								break;
							case DrawGL.shapeType.CIRCLE:
								DrawingAlgorithms.Circle(shapes[choosingShape], controlPoint0, controlPoint1);
								break;
							case DrawGL.shapeType.RECTANGLE:
								DrawingAlgorithms.Polygon(shapes[choosingShape]);
								break;
							case DrawGL.shapeType.ELLIPSE:
								DrawingAlgorithms.Ellipse(shapes[choosingShape], controlPoint0, controlPoint1);
								break;
							case DrawGL.shapeType.TRIANGLE:
								DrawingAlgorithms.Polygon(shapes[choosingShape]);
								break;
							case DrawGL.shapeType.PENTAGON:
								DrawingAlgorithms.Polygon(shapes[choosingShape]);
								break;
							case DrawGL.shapeType.HEXAGON:
								DrawingAlgorithms.Polygon(shapes[choosingShape]);
								break;
							case DrawGL.shapeType.POLYGON:
								DrawingAlgorithms.Polygon(shapes[choosingShape]);
								break;
						}

						//Vẽ lại hình
						isShapesChanged = true;
					}
				);
				thread.IsBackground = true;
				thread.Start();
			}
		}


        private void RotateBtn_Click(object sender, EventArgs e)
        {
			//Chế độ Xoay hình
			RotateOrScale = 0;
			//Vô hiệu hóa nút đã chọn
			RotateBtn.Enabled = false;
			ZoomBtn.Enabled = true;
			LbMode.Text = "Mode: Rotate";
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			//Hiển thị thời gian
			LbTime.Text = "Time: " + DateTime.Now.ToString();
		}

        private void ZoomBtn_Click(object sender, EventArgs e)
        {
			//Chế độ Scale hình
			RotateOrScale = 1;
			//Vô hiệu hóa nút đã chọn
			ZoomBtn.Enabled = false;
			RotateBtn.Enabled = true;
			LbMode.Text = "Mode: Scale";
		}

        private void WidthBtn_Click(object sender, EventArgs e)
        {
			userWidth += 0.5f;
			if (userWidth > 3)
				userWidth = 1.0f;
			WidthBtn.Text = "Width: " + userWidth.ToString("0.0");
		}
    }
}
