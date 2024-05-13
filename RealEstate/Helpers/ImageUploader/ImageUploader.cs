
using DataAccessLayer;
using DataAccessLayer.DTOs;

namespace RealEstate.Helpers.ImageUploader
{
	public class ImageUploader : IImageUploader
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly string _wwwRootPath;

		public ImageUploader(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
			_wwwRootPath = _webHostEnvironment.WebRootPath;

		}
		public string GenerateUniqueFileName(IFormFile file)
		{
			var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			return uniqueFileName;
		}


		public string UploadFile(IFormFile file, string fileName)
		{
			try
			{
				var folderPath = Path.Combine(_wwwRootPath, "Images");
				var filePath = Path.Combine(folderPath, fileName);

				Directory.CreateDirectory(folderPath);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				var imageFolder = Path.Combine("images");
				var imagePath = Path.Combine(imageFolder, fileName);
				imagePath = imagePath.Replace("\\", "/");
				return imagePath;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public bool IsImageFile(IFormFile file)
		{
			var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
			return !string.IsNullOrEmpty(extension) && _allowedExtensions.Contains(extension);
		}


		public bool DeleteImage(string image)
		{
			try
			{
				string physicalImagePath = Path.Combine(_wwwRootPath, image.Replace("/", "\\"));

				if (File.Exists(physicalImagePath))
				{
					File.Delete(physicalImagePath);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		//public string getimageurl(string folder, string? uniquefilename)
		//{
		//	var relativepath = path.combine("images", folder, uniquefilename==null?"none": uniquefilename);
		//	var baseurl = $"{_webhostenvironment.webrootpath}/";
		//	var imageurl = path.combine(baseurl, relativepath).replace("\\", "/");

		//	return imageurl;
		//}
		public bool IsImageSizeValid(IFormFile file)
		{
			const long maxSize = 10 * 1024 * 1024;

			if (file.Length <= maxSize)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		public object TryUploadImage(IFormFile file)
		{
			var error = new ErrorDTO();
			try
			{
				if (!IsImageFile(file))
				{
					error.Message = "Inalid Image Type, valid extentions " + string.Join(", ", _allowedExtensions);
					return error;
				}
				else if (!IsImageSizeValid(file))
				{
					error.Message = "image is very large , max image size 10MB";
					return error;
				}
				else
				{
					var ImageName = GenerateUniqueFileName(file);
					var imageUrl = UploadFile(file, ImageName);
					if (imageUrl == "")
					{
						error.Message = "Server failed to upload image";
						return error;
					}
					var image = new ImageDTO()
					{
						ImageName = ImageName,
						ImageUrl = imageUrl
					};
					return image;

				}
			}
			catch (Exception ex)
			{
				error.Message = ex.Message;
				return error;
			}
		}




		private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".ico", ".svg" };


	}
}
