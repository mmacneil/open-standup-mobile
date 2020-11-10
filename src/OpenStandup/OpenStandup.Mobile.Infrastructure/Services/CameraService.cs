using System;
using System.IO;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Services;
using Xamarin.Essentials;

namespace OpenStandup.Mobile.Infrastructure.Services
{
    public class CameraService : ICameraService
    {
        public async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                //Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private static async Task LoadPhotoAsync(FileBase photo)
        {
            // canceled
            if (photo == null)
            {
                //PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            await using var stream = await photo.OpenReadAsync();
            await using var newStream = File.OpenWrite(newFile);
            await stream.CopyToAsync(newStream);

            //PhotoPath = newFile;
        }
    }
}
