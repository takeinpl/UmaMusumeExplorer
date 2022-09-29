﻿using AssetStudio;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UmaMusumeData;

namespace UmaMusumeExplorer.Controls
{
    static class UnityAssetHelpers
    {
        private static readonly AssetsManager assetsManager = new();
        private static readonly Dictionary<string, ImagePointerContainer> imagePointerContainers = new();

        private static bool charaIconsLoaded = false;
        private static bool jacketIconsLoaded = false;

        public static void LoadFiles(params string[] paths)
        {
            assetsManager.LoadFiles(paths);
        }

        public static void ClearLoadedFiles()
        {
            foreach (var imagePointerContainers in imagePointerContainers)
            {
                imagePointerContainers.Value.ImageHandle.Free();
            }
            imagePointerContainers.Clear();
            assetsManager.Clear();
        }

        public static PinnedBitmap GetCharaIcon(int id, int raceDressId = 0)
        {
            if (!charaIconsLoaded)
            {
                Regex chrIconRegex = new(@"\bchr_icon_[0-9]{4}\b");
                Regex chrCardIconRegex = new(@"\bchr_icon_[0-9]{4}_[0-9]{6}_[0-9]{2}\b");

                List<string> imagePaths = new();
                List<GameAsset> charaAssetRows = UmaDataHelper.GetGameAssetDataRows(ga => ga.Name.StartsWith("chara/"));
                foreach (var asset in charaAssetRows)
                {
                    if (chrIconRegex.IsMatch(asset.BaseName) || chrCardIconRegex.IsMatch(asset.BaseName) || asset.BaseName == "chr_icon_round_0000")
                        imagePaths.Add(UmaDataHelper.GetPath(asset));
                }

                LoadFiles(imagePaths.ToArray());

                charaIconsLoaded = true;
            }

            string idString = $"{id:d4}";

            StringBuilder imageStringBuilder = new();
            imageStringBuilder.Append($"chr_icon_{idString}");
            if (idString == "0000")
            {
                imageStringBuilder.Clear();
                imageStringBuilder.Append("chr_icon_round_0000");
            }

            if (raceDressId > 0)
                imageStringBuilder.Append($"_{raceDressId:d6}_02");

            string imageString = imageStringBuilder.ToString();

            if (imagePointerContainers.ContainsKey(imageString)) return PinnedBitmapFromKey(imageString);

            SerializedFile targetAsset = GetFile(imageString, ClassIDType.Texture2D);
            Texture2D texture = targetAsset.Objects.Where(o => o.type == ClassIDType.Texture2D).First() as Texture2D;

            Image<Bgra32> image = texture.ConvertToImage(true);

            int adjustedHeight = (int)(image.Height * 1.115f);
            image.Mutate(o => o.Resize(image.Width, adjustedHeight));

            imagePointerContainers.Add(imageString,
                new ImagePointerContainer(
                   GCHandle.Alloc(image.ConvertToBytes(), GCHandleType.Pinned), image.Width, adjustedHeight));

            return PinnedBitmapFromKey(imageString);
        }

        public static PinnedBitmap GetJacket(int musicId, char size = 'm')
        {
            if (!jacketIconsLoaded)
            {
                List<string> imagePaths = new();
                List<GameAsset> liveJacketAssetRows = UmaDataHelper.GetGameAssetDataRows(ga => ga.Name.StartsWith("live/jacket/jacket_icon_l_"));
                foreach (var liveData in AssetTables.LiveDatas)
                {
                    if (liveData.HasLive == 0) continue;

                    GameAsset asset = liveJacketAssetRows.FirstOrDefault(a => a.BaseName == $"jacket_icon_l_{liveData.MusicId}");

                    if (asset is not null)
                        imagePaths.Add(UmaDataHelper.GetPath(asset));
                }

                LoadFiles(imagePaths.ToArray());

                jacketIconsLoaded = true;
            }

            string idString = $"{musicId:d4}";

            StringBuilder imageStringBuilder = new();
            imageStringBuilder.Append($"jacket_icon_{size}_{idString}");

            string imageString = imageStringBuilder.ToString();

            if (imagePointerContainers.ContainsKey(imageString)) return PinnedBitmapFromKey(imageString);

            SerializedFile targetAsset = GetFile(imageString, ClassIDType.Texture2D);
            targetAsset ??= GetFile($"jacket_icon_{size}_0000", ClassIDType.Texture2D);
            Texture2D texture = targetAsset.Objects.Where(o => o.type == ClassIDType.Texture2D).First() as Texture2D;

            Image<Bgra32> image = texture.ConvertToImage(true);

            imagePointerContainers.Add(imageString,
                new ImagePointerContainer(
                   GCHandle.Alloc(image.ConvertToBytes(), GCHandleType.Pinned), image.Width, image.Height));

            return PinnedBitmapFromKey(imageString);
        }

        public static StreamReader GetLiveCsv(int musicId, string category)
        {
            string idString = $"{musicId:d4}";

            SerializedFile targetAsset = GetFile($"m{idString}_{category}", ClassIDType.TextAsset);
            if (targetAsset is null) return null;
            TextAsset textAsset = targetAsset.Objects.Where(o => o.type == ClassIDType.TextAsset).First() as TextAsset;

            return new StreamReader(new MemoryStream(textAsset.m_Script));
        }

        private static SerializedFile GetFile(string objectName, ClassIDType classIdType)
        {
            return assetsManager.assetsFileList.Where(
                a => (a.Objects.Where(o => o.type == classIdType).FirstOrDefault() as NamedObject)?.m_Name.Equals(objectName) ?? false).FirstOrDefault();
        }

        private static PinnedBitmap PinnedBitmapFromKey(string key) =>
            new(imagePointerContainers[key].ImageHandle.AddrOfPinnedObject(),
                imagePointerContainers[key].Width,
                imagePointerContainers[key].Height);
    }

    class ImagePointerContainer
    {
        public ImagePointerContainer(GCHandle imageHandle, int width, int height)
        {
            ImageHandle = imageHandle;
            Width = width;
            Height = height;
        }

        public GCHandle ImageHandle { get; }

        public int Width { get; }

        public int Height { get; }
    }
}
