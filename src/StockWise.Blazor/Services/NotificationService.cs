using System;
using System.Collections.Generic;

namespace StockWise.Blazor.Services;

public class NotificationService
{
    public event Action<string, NotificationType>? OnShow;

    public void ShowSuccess(string message) => OnShow?.Invoke(message, NotificationType.Success);
    public void ShowError(string message) => OnShow?.Invoke(message, NotificationType.Error);
    public void ShowWarning(string message) => OnShow?.Invoke(message, NotificationType.Warning);
}

public enum NotificationType
{
    Success,
    Error,
    Warning
}
