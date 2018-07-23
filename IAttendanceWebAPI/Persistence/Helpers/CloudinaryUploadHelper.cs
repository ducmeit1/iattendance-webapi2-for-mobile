using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace IAttendanceWebAPI.Persistence.Helpers
{
    public class CloudinaryUploadHelper
    {
        public static readonly Dictionary<string, string> FileUploadErrorsDictionary = new Dictionary<string, string>
        {
            {"Image-Size", "Size of image must be greater than 0 "},
            {"Image-Size-Invalid", "Size of image must be less or equals than 0"},
            {"Image-Extension", "Only accepted .jpg, .png, .gif"}
        };

        private static Cloudinary Cloudinary
        {
            get {
                var settings = ConfigurationManager.AppSettings;
                return new Cloudinary(new Account
                {
                    ApiKey = settings["ApiKey"],
                    ApiSecret = settings["ApiSecret"],
                    Cloud = settings["ApiCloudName"]
                });
            }
        }

        public static int ValidateFileUpload(FileUpload fileUpload)
        {
            if (fileUpload == null || fileUpload.FileName.Length <= 0)
                return 0;

            var maxContentLength = 1024 * 5120 * 1; //Size = 5 MB  
            if (fileUpload.FileStream.Length > maxContentLength)
                return 1;
            var allowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".bmp", ".jpeg" };
            var ext = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf('.'));
            ext = ext.Substring(0, 4);
            var extension = ext.ToLower();
            if (!allowedFileExtensions.Contains(extension))
                return 2;
            return -1;
        }


        public static async Task<IEnumerable<CloudinaryResult>> UploadImagesIncludedFaceToCloudinary(
            IEnumerable<FileUpload> fileUploads)
        {
            var uploadResults = new List<CloudinaryResult>();
            foreach (var file in fileUploads)
            {
                var id = Guid.NewGuid().ToString();
                var result = await Cloudinary.UploadAsync(new ImageUploadParams
                {
                    PublicId = $"IAttendance/{id}",
                    File = new FileDescription(file.FileName, file.FileStream),
                    Transformation = new Transformation().Width(800).Height(800).Crop("thumb")
                });
                if (result.StatusCode == HttpStatusCode.OK)
                    uploadResults.Add(new CloudinaryResult
                    {
                        Uri = Cloudinary.Api.UrlImgUp
                            .Transform(new Transformation().Height(500).Width(500).Crop("thumb").Gravity("face"))
                            .BuildUrl(result.PublicId + ".jpg"),
                        SecureUri = Cloudinary.Api.UrlImgUp.Secure()
                            .Transform(new Transformation().Height(500).Width(500).Crop("thumb").Gravity("face"))
                            .BuildUrl(result.PublicId + ".jpg")
                    });
            }

            return uploadResults;
        }

        public static async Task<IEnumerable<CloudinaryResult>> UploadImagesToCloudinary(
            IEnumerable<FileUpload> fileUploads)
        {
            var uploadResults = new List<CloudinaryResult>();

            foreach (var file in fileUploads)
            {
                var id = Guid.NewGuid().ToString();
                var result = await Cloudinary.UploadAsync(new ImageUploadParams
                {
                    PublicId = $"IAttendance/{id}",
                    File = new FileDescription(file.FileName, file.FileStream),
                    Transformation = new Transformation().Width(800).Height(800).Crop("thumb")
                });

                if (result.StatusCode == HttpStatusCode.OK)
                    uploadResults.Add(new CloudinaryResult
                    {
                        Uri = result.Uri.ToString(),
                        SecureUri = result.SecureUri.ToString()
                    });
            }

            return uploadResults;
        }
    }
}

public class CloudinaryResult
{
    [JsonProperty("uri")] public string Uri { get; set; }

    [JsonProperty("secure_uri")] public string SecureUri { get; set; }
}

public class FileUpload
{
    public Stream FileStream { get; set; }
    public string FileName { get; set; }
}