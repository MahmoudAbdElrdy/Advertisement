using System.Collections.Generic;
using System.Linq;
using Common.Infrastructures;
using X.PagedList;

namespace Common {
  public class Result {
    internal Result(bool success, IEnumerable<string> errors, object data = null) {
      Success = success;
      Errors = errors.ToArray();
      Data = data;
    }

    #region Properties

    public bool Success { get; set; }

    public string[] Errors { get; set; }
    public object Data { get; set; }
    public LocalizedData Message { get; set; }

    #endregion


    public static Result Successed() {
      return new Result(true, new string[] { });
    }

    public static Result Successed(object data) {
      return new Result(true, new string[] { }, data);
    }

    public static Result Successed<T>(IPagedList<T> data) {
      object items = new {
        Items = data,
        MetaDate = data.GetMetaData()
      };

      return new Result(true, new string[] { }, items);
    }

    public static Result Failure(IEnumerable<string> errors) {
      return new Result(false, errors);
    }
  }
}