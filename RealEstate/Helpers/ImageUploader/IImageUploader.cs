namespace RealEstate.Helpers.ImageUploader
{
	public interface IImageUploader
	{
		string GenerateUniqueFileName(IFormFile file);
		string UploadFile(IFormFile file, string fileName);
		bool IsImageFile(IFormFile file);
		bool IsImageSizeValid(IFormFile file);
		//string GetImagePath(string folder, string uniqueFileName);
		bool DeleteImage(string image);
		//string GetImageUrl(string folder, string uniqueFileName);
		object TryUploadImage(IFormFile file);
	}
}
