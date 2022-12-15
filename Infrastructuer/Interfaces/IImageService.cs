using System.Threading.Tasks;
using AuthDomain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Interfaces {
  public interface IImageService {
    Task<Image> SaveImageAsync(Image image, byte[] dtoFile);
    Task<Image> SaveImageAsync(Image image, string dtoFile);
    Task<Image> SaveImageAsync(Image image, IFormFile dtoFile);
    bool TryGetFromBase64String(string input, out byte[] output);
    string GetImagePath(string imageFullPath);
    Task<string> UploadImg(string base64, string folderName, string fileName);
    Task<string> UploadImg(byte[] img, string folderName, string fileName);
    Task<string> UploadImg(IFormFile file, string folderName);
    Task<Image> AddImage(string base64, string folderName, string fileName);
    void DeleteImageIfExist<T>(T id);
  }
}