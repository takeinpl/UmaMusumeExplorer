﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmaMusumeData.Tables;
using UmaMusumeExplorer.Controls.CharacterInfo;
using UmaMusumeExplorer.Game;

namespace UmaMusumeExplorer.Controls.Common
{
    internal class CharacterItemsPanel : ItemsPanel<CharaData>
    {
        public override bool ProcessItem(CharaData charaData, ref Control characterPictureBox)
        {
            bool playable = AssetTables.CardDatas.Any(cd => cd.CharaId == charaData.Id);
            characterPictureBox = new CharacterPictureBox(playable)
            {
                BackgroundImage = UnityAssets.GetCharaIcon(charaData.Id).Bitmap,
                BackgroundImageLayout = ImageLayout.Zoom,
                Cursor = Cursors.Hand,
                Height = 100,
                Width = 100,
                Tag = charaData
            };

            ToolTip toolTip = new();
            toolTip.SetToolTip(characterPictureBox, $"{charaData.Id}: {AssetTables.GetText(AssetTables.CharaNameTextDatas, charaData.Id)}");

            return true;
        }
    }
}