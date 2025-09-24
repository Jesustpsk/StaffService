namespace StaffService.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
        
    }

    /// <summary>
    /// Конструктор для мока
    /// </summary>
    public NotFoundException()
    {
        
    }
}