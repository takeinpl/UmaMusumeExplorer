﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UmaMusumeData.Tables;
using UmaMusumeExplorer.Game;

namespace UmaMusumeExplorer.Controls.LiveMusicPlayer
{
    partial class CharacterSelectForm : Form
    {
        private readonly IEnumerable<CharaData> charaDatas = AssetTables.CharaDatas;
        private readonly IEnumerable<LivePermissionData> livePermissionData;
        private readonly List<int> allowedCharas = new();
        private readonly List<int> alreadySelected = new();

        public CharacterSelectForm(IEnumerable<LivePermissionData> permissionData, CharacterPositionControl[] characters)
        {
            InitializeComponent();

            livePermissionData = permissionData;
            foreach (var lpd in livePermissionData)
            {
                allowedCharas.Add(lpd.CharaId);
            }

            foreach (var character in characters)
            {
                alreadySelected.Add(character.CharacterId);
            }
        }

        public int SelectedCharacter { get; private set; } = 0;

        private void CharacterSelectForm_Load(object sender, EventArgs e)
        {
            characterItemsPanel.Filter = (cd) =>
            {
                return allowedCharas.Contains(cd.Id);
            };
            characterItemsPanel.LoadingFinished = (s, e) =>
            {
                foreach (PictureBox charaIcon in characterItemsPanel.Controls)
                {
                    CharaData cd = charaIcon.Tag as CharaData;
                    if (alreadySelected.Contains(cd.Id)) charaIcon.BackColor = Color.FromArgb(234, 54, 128);
                }
            };
            characterItemsPanel.ItemClicked = (s, e) =>
            {
                PictureBox pictureBox = s as PictureBox;
                SelectedCharacter = (pictureBox.Tag as CharaData).Id;
                Close();
            };
            characterItemsPanel.Items = charaDatas;
        }
    }
}
