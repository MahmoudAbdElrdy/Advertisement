using AuthDomain.Entities.Auth;
using AuthDomain.Enums;
using Common;
using Common.Interfaces;

namespace AuthDomain.Entities {
  public class Image:BaseEntity<string>,IImage {
    public byte[] Data { get; set; }
    public string Imageurl { get; set; }// => $"r/{Id}";//?s=full
    public string ThumbnailUrl => $"r/{Id}";//?s=thumb
    public string RefId { get; set; }
  }
}