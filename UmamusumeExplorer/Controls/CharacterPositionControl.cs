using UmamusumeExplorer.Assets;
using UmamusumeExplorer.Music.Live;
using Image = System.Drawing.Image;

namespace UmamusumeExplorer.Controls
{
    partial class CharacterPositionControl : UserControl
    {
        private int characterPosition = 0;
        private int characterId = 0;
        private Image? characterImage;
        private bool disabled = false;
        private bool showMode = false;

        public CharacterPositionControl(int position, EventHandler clickEventHandler, EventHandler<MultiStateButtonEventArgs>? modeChangedEventHandler, int width = -1)
        {
            InitializeComponent();

            characterPosition = position;

            positionIndexLabel.Text = (characterPosition + 1).ToString();
            characterPictureBox.BackgroundImage = GameAssets.GetCharaIcon(0)?.Bitmap;
            modeButton.Setup(typeof(TrackMode));

            float ratio = (float)characterPictureBox.Height / characterPictureBox.Width;

            characterPictureBox.Click += clickEventHandler;
            modeButton.StateChanged += modeChangedEventHandler;

            if (width > 0)
            {
                Width = width;
            }

            characterPictureBox.Height = (int)(characterPictureBox.Width * ratio);
            modeButton.Top = characterPictureBox.Top + characterPictureBox.Height + 6;

            modeButton.Visible = showMode;
        }

        public int Position
        {
            get { return characterPosition; }
            set
            {
                characterPosition = value;
                positionIndexLabel.Text = (value + 1).ToString();

                Update();
            }
        }

        public int CharacterId
        {
            get { return characterId; }
            set
            {
                characterId = value;
                characterPictureBox.BackgroundImage = characterImage = GameAssets.GetCharaIcon(characterId)?.Bitmap;

                Update();
            }
        }

        public float FontSize
        {
            get => positionIndexLabel.Font.Size;
            set
            {
                positionIndexLabel.Font = new Font(positionIndexLabel.Font.Name, value);
            }
        }

        public bool Disabled
        {
            get => disabled;
            set
            {
                disabled = value;
                characterPictureBox.BackgroundImage = disabled ? null : characterImage;
            }
        }

        public bool ShowEx
        {
            get => showMode;
            set
            {
                showMode = value;
                modeButton.Visible = showMode;
            }
        }

        public TrackMode Mode
        {
            get => modeButton.State is null ? TrackMode.Main : (TrackMode)modeButton.State;
            set => modeButton.State = value;
        }
    }
}
