namespace NetCoreLA7.Areas.Admin.Helpers
{
    public static class ImageHelper
    {
        private static readonly string[] Allowed = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public static bool IsImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return Allowed.Contains(ext);
        }

        public static async Task<string?> SaveAsync(IFormFile? file, string webRootPath, string folder)
        {
            if (file == null || file.Length == 0) return null;

            var dir = Path.Combine(webRootPath, "uploads", folder);
            Directory.CreateDirectory(dir);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName).ToLowerInvariant();
            using var stream = new FileStream(Path.Combine(dir, fileName), FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }

        public static void Delete(string? fileName, string webRootPath, string folder)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            var path = Path.Combine(webRootPath, "uploads", folder, fileName);
            if (File.Exists(path)) File.Delete(path);
        }
    }
}