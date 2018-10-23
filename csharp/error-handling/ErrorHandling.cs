using System;

public static class ErrorHandling
{
    public static void HandleErrorByThrowingException() => throw new Exception(nameof(HandleErrorByThrowingException));

    public static int? HandleErrorByReturningNullableType(string input) => int.TryParse(input, out int success) ? success : (int?)null;

    public static bool HandleErrorWithOutParam(string input, out int result) => int.TryParse(input, out result);

    public static void DisposableResourcesAreDisposedWhenExceptionIsThrown(IDisposable disposableObject)
    {
        try
        {
            throw new Exception(nameof(DisposableResourcesAreDisposedWhenExceptionIsThrown));
        }
        finally
        {
            disposableObject.Dispose();
        }
    }
}
