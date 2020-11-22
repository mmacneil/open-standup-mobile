using System;
using System.IO;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Services;
using Xamarin.Essentials;

namespace OpenStandup.Mobile.Infrastructure.Services
{
    public class CameraService : ICameraService
    {
        public async Task<string> TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                return await LoadPhotoAsync(photo);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static async Task<string> LoadPhotoAsync(FileBase photo)
        {
            // canceled
            if (photo == null)
            {
                return null;
            }

            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            await using var stream = await photo.OpenReadAsync();
            await using var newStream = File.OpenWrite(newFile);
            await stream.CopyToAsync(newStream);
            return newFile;
        }
    }
}
