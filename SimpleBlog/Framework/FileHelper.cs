namespace SimpleBlog.Framework
{
    public static class FileHelper
    {
        public static async Task<string> SaveUploadedFileAsync(this IWebHostEnvironment env, IFormFile file, string localPath, string? optionalName = null, bool useWwwRoot = true, bool overrideIfExists = false)
        {
            if (file != null)
            {
                try
                {
                    string givenName = optionalName?.Replace(localPath, "") ?? (DateTime.Now.ToString("yyyyMMddHHmmss_") + file.FileName);
                    if (givenName[0] == '\\')
                    {
                        givenName = givenName.Substring(1);
                    }
                    string givenlocalName = Path.Combine(localPath, givenName).Replace('/', '\\');
                    string rootPath = env.ContentRootPath;
                    if (useWwwRoot)
                    {
                        rootPath = env.WebRootPath;
                    }
                    string fileName = rootPath + "\\" + givenlocalName;
                    if (File.Exists(fileName))
                    {
                        if (overrideIfExists)
                        {
                            File.Delete(fileName);
                        }
                        else
                        {
                            givenlocalName = Path.Combine(localPath, DateTime.Now.ToString("yyyyMMddHHmmss_") + givenName).Replace('/', '\\');
                            fileName = rootPath + "\\" + givenlocalName;
                        }
                    }
                    string filePath = Path.GetDirectoryName(fileName);
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }

                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return $"/{givenlocalName.Replace('\\', '/')}".Replace("//", "/");
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    return null;
                }
            }
            return null;
        }

    }
}
