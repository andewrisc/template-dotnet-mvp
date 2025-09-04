using System;
using System.Net;

namespace API.Models.Responses;

public class BaseResponse<TData> : BaseResponse
        where TData : class, new()
{
    public TData Data { get; set; }
    public BaseResponse()
        : base()
    {
        this.Data = new TData();
    }

    public BaseResponse(TData data)
        : base()
    {
        this.Data = data;
    }
}

public class BaseResponse
{
    public bool IsValid { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public BaseResponse()
    {
        this.IsValid = true;
        this.Message = string.Empty;
        this.StatusCode = 200;
    }

    public void CopyFrom(BaseResponse source)
    {
        this.Message = source.Message;
        this.StatusCode = source.StatusCode;
        this.IsValid = source.IsValid;
    }
}


public static class BaseResponseExtensions
{
    public static TResponse SetUnauthorized<TResponse>(this TResponse response)
        where TResponse : BaseResponse
    {
        response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return response;
    }

    public static TResponse SetValidationMessage<TResponse>(this TResponse response, string validationMessage)
        where TResponse : BaseResponse
    {
        if (string.IsNullOrWhiteSpace(response.Message))
        {
            response.IsValid = false;
            response.Message = validationMessage;
        }
        return response;
    }

    public static TResponse SetErrorMessage<TResponse>(this TResponse response, string errorMessage)
        where TResponse : BaseResponse
    {
        if (response.StatusCode == 200)
        {
            response.StatusCode = 500;
            response.Message = errorMessage;
        }
        return response;
    }

    public static TResponse SetStatusCodeAndMessage<TResponse>(this TResponse response, HttpStatusCode httpStatusCode, string message)
        where TResponse : BaseResponse => response.SetStatusCodeAndMessage((int)httpStatusCode, message);

    public static TResponse SetStatusCodeAndMessage<TResponse>(this TResponse response, int statusCode, string message)
        where TResponse : BaseResponse
    {
        if (response.StatusCode == 200)
        {
            response.StatusCode = statusCode;
            response.Message = message;
        }
        return response;
    }

    public static bool IsSuccessAndValid<TResponse>(this TResponse response)
        where TResponse : BaseResponse => response.StatusCode == 200 && response.IsValid;
}


// | Kode    | Nama                  | Kapan Digunakan                                                                  |
// | ------- | --------------------- | -------------------------------------------------------------------------------- |
// | **200** | OK                    | Request berhasil, data dikembalikan                                              |
// | **201** | Created               | Data berhasil dibuat (biasanya untuk `POST`)                                     |
// | **204** | No Content            | Request berhasil, tapi tidak ada isi untuk dikembalikan (misal `DELETE`)         |
// | **400** | Bad Request           | Validasi gagal, parameter tidak lengkap/salah                                    |
// | **401** | Unauthorized          | Belum login atau token tidak valid                                               |
// | **403** | Forbidden             | Sudah login tapi tidak punya akses                                               |
// | **404** | Not Found             | Data atau endpoint tidak ditemukan                                               |
// | **409** | Conflict              | Terjadi konflik data, biasanya karena duplikasi (misal: email sudah dipakai)     |
// | **422** | Unprocessable Entity  | Data validasi kompleks gagal (lebih detail dari 400, opsional kalau mau dipakai) |
// | **500** | Internal Server Error | Kesalahan tak terduga dari server                                                |
// | **503** | Service Unavailable   | Server overload atau sedang maintenance (opsional)                               |
