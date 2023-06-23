﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmaMusumeData.Tables;
using UmaMusumeExplorer.Controls.CharacterInfo.Classes;
using UmaMusumeExplorer.Game;
using static UmaMusumeExplorer.Controls.CharacterInfo.RankedLabel;

namespace UmaMusumeExplorer.Controls.CharacterInfo
{
    public partial class CardInfoControl : UserControl
    {
        private CharaData charaData;
        private PinnedBitmap iconPinnedBitmap;

        public CardInfoControl()
        {
            InitializeComponent();
        }

        public CharaData CharaData
        {
            get
            {
                return charaData;
            }

            set
            {
                charaData = value;

                if (charaData is not null)
                    LoadCharaData();
            }
        }

        private void CostumeSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CardData cardData = (costumeComboBox.SelectedItem as CostumeComboBoxItem).CardData;

            rarityComboBox.Items.Clear();
            foreach (var rarityData in AssetTables.CardRarityDatas.Where(crd => crd.CardId == cardData.Id))
            {
                rarityComboBox.Items.Add(new RarityComboBoxItem(rarityData));
            }

            if (rarityComboBox.Items.Count > 0)
                rarityComboBox.SelectedIndex = 0;
            else
            {
                iconPinnedBitmap.Dispose();
                iconPinnedBitmap = UnityAssets.GetCharaIcon(cardData.CharaId);
                iconPictureBox.Image = iconPinnedBitmap.Bitmap;

                UpdateStats(cardData, new CardRarityData());
            }
        }

        private void RarityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CardData cardData = (costumeComboBox.SelectedItem as CostumeComboBoxItem).CardData;
            CardRarityData rarityData = (rarityComboBox.SelectedItem as RarityComboBoxItem).CardRarityData;

            rarityData ??= new();

            iconPinnedBitmap.Dispose();
            iconPinnedBitmap = UnityAssets.GetCharaIcon(cardData.CharaId, rarityData.RaceDressId);
            iconPictureBox.Image = iconPinnedBitmap.Bitmap;

            UpdateStats(cardData, rarityData);
        }

        private void SkillButton_Click(object sender, EventArgs e)
        {
            SkillButtonSmall skillButton = sender as SkillButtonSmall;

            ControlHelpers.ShowFormDialogCenter(new SkillInfoForm((int)skillButton.Tag), this);
        }

        private void LoadCharaData()
        {
            int id = charaData.Id;
            string charaName = AssetTables.GetText(AssetTables.CharaNameTextDatas, id);

            Text = charaName;
            nameLabel.Text = charaName;

            string katakana = AssetTables.GetText(AssetTables.CharaNameKatakanaTextDatas, id);

            if (!katakana.Equals(nameLabel.Text))
                nameLabel.Text += $"（{AssetTables.GetText(AssetTables.CharaNameKatakanaTextDatas, id)}）";
            nameLabel.BackColor = ColorFromHexString(charaData.UIColorMain);
            if (GetBrightness(nameLabel.BackColor) > 128)
                nameLabel.ForeColor = Color.Black;
            else
                nameLabel.ForeColor = Color.White;

            cvNameLabel.Text = "CV. " + AssetTables.GetText(AssetTables.CharaVoiceNameTextDatas, id);
            cvNameLabel.BackColor = ColorFromHexString(charaData.UIColorSub);
            if (GetBrightness(cvNameLabel.BackColor) > 128)
                cvNameLabel.ForeColor = Color.Black;
            else
                cvNameLabel.ForeColor = Color.White;

            genderLabel.Text = charaData.Sex == 1 ? "Male" : "Female";
            birthdayLabel.Text = $"{charaData.BirthDay}/{charaData.BirthMonth}/{charaData.BirthYear} ({DateTime.Now.Year - charaData.BirthYear} years)";

            iconPinnedBitmap = UnityAssets.GetCharaIcon(id);
            iconPictureBox.Image = iconPinnedBitmap.Bitmap;

            foreach (var item in AssetTables.CardDatas.Where(cd => cd.CharaId == charaData.Id))
            {
                costumeComboBox.Items.Add(new CostumeComboBoxItem(item));
            }

            if (costumeComboBox.Items.Count > 0)
                costumeComboBox.SelectedIndex = 0;
        }

        private void UpdateStats(CardData cardData, CardRarityData cardRarityData)
        {
            speedStatusDisplayLabel.Value = cardRarityData.Speed;
            staminaStatusDisplayLabel.Value = cardRarityData.Stamina;
            powerStatusDisplayLabel.Value = cardRarityData.Pow;
            gutsStatusDisplayLabel.Value = cardRarityData.Guts;
            wisdomStatusDisplayLabel.Value = cardRarityData.Wiz;

            turfRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperGroundTurf;
            dirtRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperGroundDirt;

            shortRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperDistanceShort;
            mileRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperDistanceMile;
            middleRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperDistanceMiddle;
            longRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperDistanceLong;

            escapeRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperRunningStyleNige;
            leadingRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperRunningStyleSenko;
            insertRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperRunningStyleSashi;
            driveInRankedLabel.Rank = (RankedLabelRank)cardRarityData.ProperRunningStyleOikomi;

            speedGrowthLabel.Text = cardData.TalentSpeed.ToString() + "%";
            staminaGrowthLabel.Text = cardData.TalentStamina.ToString() + "%";
            powerGrowthLabel.Text = cardData.TalentPow.ToString() + "%";
            gutsGrowthLabel.Text = cardData.TalentGuts.ToString() + "%";
            wisdomGrowthLabel.Text = cardData.TalentWiz.ToString() + "%";

            skillsTableLayoutPanel.Controls.Clear();

            SkillSet uniqueSkillSet = AssetTables.SkillSets.FirstOrDefault(s => s.Id == cardRarityData.SkillSet);
            if (uniqueSkillSet is not null)
            {
                IEnumerable<AvailableSkillSet> availableSkillSet = AssetTables.AvailableSkillSets
                    .Where(a => a.AvailableSkillSetId == cardData.AvailableSkillSetId)
                    .OrderBy(a => a.SkillId);

                skillsTableLayoutPanel.Controls.Add(ButtonFromSkillData(uniqueSkillSet.SkillId1, uniqueSkillSet.SkillLevel1), 0, 0);

                int currentColumn = 1;
                int currentRow = 0;
                foreach (var availableSkill in availableSkillSet)
                {
                    skillsTableLayoutPanel.Controls.Add(ButtonFromSkillData(availableSkill.SkillId), currentColumn % 2, currentRow);

                    currentColumn++;
                    if (currentColumn % 2 == 0)
                        currentRow++;
                }
            }
        }

        private SkillButtonSmall ButtonFromSkillData(int skillId, int level = 0)
        {
            SkillData skill = AssetTables.SkillDatas.First(s => s.Id == skillId);
            SkillButtonSmall skillButton = new()
            {
                Dock = DockStyle.Fill,
                IconId = skill.IconId,
                Rarity = (SkillRarity)skill.Rarity,
                SkillLevel = level,
                SkillName = AssetTables.GetText(AssetTables.SkillNameTextDatas, skillId),
                Tag = skill.Id
            };

            skillButton.SkillClick += SkillButton_Click;

            return skillButton;
        }

        private static Color ColorFromHexString(string hexString)
        {
            byte a = 0xFF;
            byte r = Convert.FromHexString(hexString[..2])[0];
            byte g = Convert.FromHexString(hexString[2..4])[0];
            byte b = Convert.FromHexString(hexString[4..6])[0];

            return Color.FromArgb(a, r, g, b);
        }

        private static byte GetBrightness(Color color)
        {
            return (byte)((color.R + color.R + color.G + color.G + color.B + color.B) / 6);
        }
    }

    class CostumeComboBoxItem
    {
        public CostumeComboBoxItem(CardData cardData)
        {
            CardData = cardData;
        }

        public CardData CardData { get; }

        public override string ToString()
        {
            return AssetTables.GetText(AssetTables.CharaCostumeNameTextDatas, CardData.Id);
        }
    }

    class RarityComboBoxItem
    {
        public RarityComboBoxItem(CardRarityData cardRarityData)
        {
            CardRarityData = cardRarityData;
        }

        public CardRarityData CardRarityData { get; }

        public override string ToString()
        {
            StringBuilder starsString = new();
            for (int i = 0; i < CardRarityData.Rarity; i++)
            {
                starsString.Append('★');
            }

            return starsString.ToString();
        }
    }
}