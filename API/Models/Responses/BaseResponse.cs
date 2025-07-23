using System;

namespace API.Models.Responses;

public class BaseResponse
{
    public bool IsValid { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }

    public static BaseResponse<T> Success(T data, int statusCode = 200, string message = "")
    {
        return new BaseResponse<T>
        {
            Data = data,
            IsValid = true,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static BaseResponse<T> Fail(string message, int statusCode = 400)
    {
        return new BaseResponse<T>
        {
            Data = default,
            IsValid = false,
            StatusCode = statusCode,
            Message = message
        };
    }
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
