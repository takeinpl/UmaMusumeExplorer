using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using UmamsumeData;
using UmamsumeData.Tables;
using UmamusumeExplorer.Assets;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace UmamusumeExplorer.Controls
{
    public partial class SkillInfoForm : BorderlessAeroForm
    {
        private readonly SkillBackground rarity;

        private bool dragging = false;
        private Point clickPoint;

        public SkillInfoForm(SkillData skill, IEnumerable<SkillData>? evolutionSkills)
        {
            InitializeComponent();

            SetBorderlessAndAdjust();

            rarity = (SkillBackground)skill.Rarity;

            skillNameLabel.Text = AssetTables.GetText(TextCategory.MasterSkillName, skill.Id);
            skillDescriptionLabel.Text = AssetTables.GetText(TextCategory.MasterSkillExplain, skill.Id)
                .Replace("\\n", "\n");
            iconPictureBox.BackgroundImage = GameAssets.GetSkillIcon(skill.IconId)?.Bitmap;

            SingleModeSkillNeedPoint? needSkillPoint = AssetTables.SingleModeSkillNeedPoints.FirstOrDefault(s => s.Id == skill.Id);

            if (needSkillPoint is not null)
            {
                skillPointHint.Visible = true;
                skillPointNeededLabel.Text = needSkillPoint.NeedSkillPoint.ToString();
            }

            if (evolutionSkills is not null && evolutionSkills.Any())
            {
                evolutionButton.Visible = true;
                evolutionButton.Click += (s, e) =>
                {
                    ControlHelpers.ShowFormDialogCenter(new SkillEvolutionsForm(skill, evolutionSkills), this);
                };
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SkillInfoForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            clickPoint = e.Location;
        }

        private void SkillInfoForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point point = Location;

                point.X += e.Location.X - clickPoint.X;
                point.Y += e.Location.Y - clickPoint.Y;

                Location = point;
            }
        }

        private void SkillInfoForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle = ClientRectangle;

            Brush colorBrush = SkillColorGenerator.ColorFromType(rarity, rectangle, out Brush backgroundBrush);

            Rectangle paddedRectangle = rectangle;
            paddedRectangle.X += 4;
            paddedRectangle.Y += 4;
            paddedRectangle.Width -= 10;
            paddedRectangle.Height -= 10;

            GraphicsPath path = new();
            int diameter = 12;
            Rectangle arc = new(paddedRectangle.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = paddedRectangle.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = paddedRectangle.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = paddedRectangle.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillRectangle(backgroundBrush, rectangle);
            e.Graphics.FillPath(colorBrush, path);
            //e.Graphics.FillRectangle(
            //    new SolidBrush(Color.FromArgb(127, 255, 255, 255)),
            //    paddedRectangle);
            e.Graphics.FillPath(
                new SolidBrush(Color.FromArgb(127, 255, 255, 255)),
                //new SolidBrush(Color.FromArgb(255, 24, 24, 24)),
                path);
            //e.Graphics.FillPath(linearGradientBrush, path);

            base.OnPaint(e);
        }
    }
}
