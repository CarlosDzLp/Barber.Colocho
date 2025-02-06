using Barber.Colocho.Core.Error;
using Barber.Colocho.Models.Helpers;

namespace Barber.Colocho.Core.Helpers
{
    public class UploadImage
    {
        private readonly ErrorBL error;

        public UploadImage(ErrorBL error)
        {
            this.error = error;
        }

        public async Task<string> Upload(byte[] file, string folder, string fileName = "")
        {
            try
            {
                var pathUrl = Path.Combine(KeyDictionary.URL_BASE_SAVE_FILE, folder);
                if (file != null && file.Length > 0)
                {
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = Guid.NewGuid().ToString() + ".png";
                    }
                    if (!Directory.Exists(pathUrl))
                    {
                        Directory.CreateDirectory(pathUrl);
                    }
                    var path = Path.Combine(pathUrl, fileName);
                    await File.WriteAllBytesAsync(path, file);
                    return fileName;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                await error.LogError(ex);
                return string.Empty;
            }
        }

        public async Task Remove(string folder, string fileName)
        {
            try
            {
                var host = Path.Combine(KeyDictionary.URL_BASE_SAVE_FILE, folder);
                //string pathCombine = System.IO.Path.Combine(host, folder);
                var path = System.IO.Path.Combine(host, fileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
            catch (Exception ex)
            {
                await error.LogError(ex);
            }
        }

        public string GetFile(string fileName, string folder)
        {
            var fullPath = Path.Combine(KeyDictionary.URL_BASE_IMAGE, folder, fileName);
            return fullPath;
        }
    }
}
