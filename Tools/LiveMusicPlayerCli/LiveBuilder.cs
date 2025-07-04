﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmaMusumeData.Tables;
using UmaMusumeData;
using AssetsTools.NET;
using CriWareLibrary;
using AssetsTools.NET.Extra;

namespace LiveMusicPlayerCli
{
    internal class LiveBuilder
    {
        private readonly AssetsManager assetsManager = new();

        private readonly int musicId;
        private readonly IEnumerable<ManifestEntry> audioAssets;
        private readonly List<PartTrigger> partTriggers = new();
        private readonly AwbReader? okeAwb;
        private readonly List<AwbReader> singerAwbs = new();

        public LiveBuilder(int id)
        {
            musicId = id;

            LoadFiles(UmaDataHelper.GetManifestEntries(ga => ga.Name.StartsWith($"live/musicscores/m{musicId}")));
            audioAssets = UmaDataHelper.GetManifestEntries(ga => ga.Name.StartsWith($"sound/l/{musicId}"));

            StreamReader? partCsv = GetLiveCsv(musicId, "part");
            if (partCsv is null)
                throw new Exception("라이브 파트의 데이터를 읽어올 수 없습니다."); // Unable to read live part data.
            bool hasVolumeRate = partCsv.ReadLine()?.Contains("volume_rate") ?? false;
            while (!partCsv.EndOfStream)
            {
                PartTrigger trigger = new(partCsv.ReadLine() ?? "", hasVolumeRate);
                partTriggers.Add(trigger);
            }

            okeAwb = GetAwbFile(audioAssets.FirstOrDefault(aa => aa.BaseName.Equals($"snd_bgm_live_{musicId}_oke_02.awb")));

            Singers = new int[GetSingingMemberCount()];
        }

        public int[] Singers { get; }

        public bool Solo { get; set; }

        public ISampleProvider Build(bool allSing = false)
        {
            if (okeAwb is null)
                throw new Exception("No background music audio bank.");

            for (int i = 0; i < (Solo ? 1 : Singers.Length); i++)
            {
                AwbReader? charaAwb = GetAwbFile(audioAssets.FirstOrDefault(aa => aa.BaseName.Equals($"snd_bgm_live_{musicId}_chara_{Singers[i]}_01.awb")));

                if (charaAwb is null)
                    throw new Exception($"Audio bank file for singer ID {Singers[i]} does not exist.");

                singerAwbs.Add(charaAwb);
            }

            SongMixer songMixer = new(okeAwb, partTriggers);
            songMixer.InitializeCharaTracks(singerAwbs);

            songMixer.CenterOnly = Solo;
            songMixer.AllSing = allSing;

            return songMixer;
        }

        public int GetSingingMemberCount()
        {
            bool[] membersSing = new bool[partTriggers[0].MemberTracks.Length];

            foreach (var partTrigger in partTriggers)
            {
                for (int i = 0; i < partTrigger.MemberTracks.Length; i++)
                {
                    if (partTrigger.MemberTracks[i] > 0) membersSing[i] = true;
                }
            }

            int activeMembers = 0;
            for (int i = 0; i < membersSing.Length; i++)
            {
                if (membersSing[i]) activeMembers++;
            }

            return activeMembers;
        }

        private void LoadFiles(IEnumerable<ManifestEntry> assets)
        {
            if (!assets.Any()) return;

            List<string> assetPaths = new();
            foreach (var item in assets)
            {
                assetPaths.Add(UmaDataHelper.GetPath(item));
                BundleFileInstance bundleFileInstance = assetsManager.LoadBundleFile(UmaDataHelper.GetPath(item));
                foreach (var bundleFile in bundleFileInstance.file.GetAllFileNames())
                {
                    assetsManager.LoadAssetsFileFromBundle(bundleFileInstance, bundleFile);
                }
            }
        }

        private StreamReader? GetLiveCsv(int musicId, string category)
        {
            //string idString = $"{musicId:d4}";

            //SerializedFile? targetAsset = assetsManager.assetsFileList.FirstOrDefault(
            //    a => (a.Objects.FirstOrDefault(o => o.type == ClassIDType.TextAsset) as NamedObject)?.m_Name.Equals($"m{idString}_{category}") ?? false);
            //if (targetAsset is null) return null;
            //TextAsset? textAsset = targetAsset.Objects.First(o => o.type == ClassIDType.TextAsset) as TextAsset;

            //if (textAsset is not null)
            //    return new StreamReader(new MemoryStream(textAsset.m_Script));
            //else
            //    return null;
            return null;
        }

        private static AwbReader? GetAwbFile(ManifestEntry? entry)
        {
            if (entry is not null)
            {
                string awbPath = UmaDataHelper.GetPath(entry);
                if (File.Exists(awbPath)) return new(File.OpenRead(awbPath));
            }

            return null;
        }
    }
}
