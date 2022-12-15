using System.Net;

namespace Common {

  public enum ApiExeptionType {
    [Error("10", HttpStatusCode.NotFound, Message = "Record no found")]
    NotFound,      
    [Error("20", HttpStatusCode.BadRequest, Message =  "Invalid login")]
    InvalidLogin,       
   
    [Error("40", HttpStatusCode.BadRequest, Message = "Validation error ")]
    ValidationError,


    //Files errors
    [Error("100", HttpStatusCode.UnsupportedMediaType, Message = "Invalid Image file formate")]
    InvalidImage,    
    [Error("101", HttpStatusCode.UnsupportedMediaType, Message = "Faild to delete image form cache directory")]
    FaildDeleteImage,
    [Error("102", HttpStatusCode.UnsupportedMediaType, Message = "Faild to save image")]
    FaildSaveImage,
  }
}
