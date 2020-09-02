using GameExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace GameExplorer.Uwp.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImageUtils
    {
        /// <summary>
        /// Gets the image picker.
        /// </summary>
        /// <value>
        /// The image picker.
        /// </value>
        public static Windows.Storage.Pickers.FileOpenPicker ImagePicker => new Windows.Storage.Pickers.FileOpenPicker
        {
            ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
            SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary,
            FileTypeFilter = { ".jpg", ".jpeg", ".png", ".gif" }
        };

        /// <summary>
        /// Files to byte array.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string filepath)
        {
            var stream = File.OpenRead(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + filepath);
            var fileBytes = new byte[stream.Length];

            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            return fileBytes;
        }

        /// <summary>
        /// Storages the file to bitmap image.
        /// </summary>
        /// <param name="savedStorageFile">The saved storage file.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        public static async Task<BitmapImage> StorageFileToBitmapImage(StorageFile savedStorageFile, int height, int width)
        {
            using (var fileStream = await savedStorageFile.OpenAsync(FileAccessMode.Read))
            {
                var bitmapImage = new BitmapImage
                {
                    DecodePixelHeight = height,
                    DecodePixelWidth = width
                };
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }

        /// <summary>
        /// Storages the file to byte array.
        /// </summary>
        /// <param name="savedStorageFile">The saved storage file.</param>
        /// <returns></returns>
        public static async Task<byte[]> StorageFileToByteArray(StorageFile savedStorageFile)
        {
            using (var stream = await savedStorageFile.OpenStreamForReadAsync())
            {
                var bytes = new byte[(int)stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                return bytes;
            }   
        }

        /// <summary>
        /// Bitmaps the image to byte array.
        /// </summary>
        /// <param name="bitmapImage">The bitmap image.</param>
        /// <returns></returns>
        public static async Task<byte[]> BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(bitmapImage.UriSource);
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();

                var byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }

        /// <summary>
        /// Bytes the array to bitmap image.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] bytes)
        {
            using (var ms = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(bytes);
                    writer.StoreAsync().GetResults();
                }
                var image = new BitmapImage();
                image.SetSource(ms);
                return image;
            }
        }

        /// <summary>
        /// Files to image minimal.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static Image FileToImageMinimal(string filepath, string description = null)
        {
            var bytearray = FileToByteArray(filepath);
            return new Image(bytearray, description);
        }

        /// <summary>
        /// Files to image.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static Image FileToImage(string filepath, string description = null)
        {
            var bytearray = FileToByteArray(filepath);
            var bitmapImage = ByteArrayToBitmapImage(bytearray);
            return new Image(bytearray, bitmapImage.PixelWidth, bitmapImage.PixelHeight, description);
        }

        /// <summary>
        /// Bytes the array to image.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] bytes, string description = null)
        {
            var bitmapImage = ByteArrayToBitmapImage(bytes);
            return new Image(bytes, bitmapImage.PixelWidth, bitmapImage.PixelHeight, description);
        }

        /// <summary>
        /// Bitmaps the image to image.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static async Task<Image> BitmapImageToImage(BitmapImage bitmap, string description = null)
        {
            var byteArray = await BitmapImageToByteArray(bitmap);
            return new Image(byteArray, bitmap.PixelWidth, bitmap.PixelHeight, description);
        }

        /// <summary>
        /// Storages the file to image.
        /// </summary>
        /// <param name="savedStorageFile">The saved storage file.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static async Task<Image> StorageFileToImage(StorageFile savedStorageFile, string description = null)
        {
            var byteArray = await StorageFileToByteArray(savedStorageFile);
            var bitmap = ByteArrayToBitmapImage(byteArray);

            return new Image(byteArray, bitmap.PixelWidth, bitmap.PixelHeight, description);
        }

        /// <summary>
        /// Picks the multiple images.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Image>> PickMultipleImages()
        {
            var picker = ImagePicker;
            var files = await picker.PickMultipleFilesAsync();
            var images = new List<Image>();

            if (files.Count == 0)
            {
                await NotifyUtils.DisplayErrorMessage("Something seems to have gone wrong when fetching your image(s).");
                return null;
            }

            foreach (var file in files)
            {
                images.Add(await StorageFileToImage(file));
            }

            return images;
        }

        /// <summary>
        /// Picks the single image.
        /// </summary>
        /// <returns></returns>
        public static async Task<Image> PickSingleImage()
        {
            var picker = ImagePicker;
            var file = await picker.PickSingleFileAsync();

            if (file == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something seems to have gone wrong when fetching your image.");
                return null;
            }

            return await StorageFileToImage(file);
        }
    }
}
