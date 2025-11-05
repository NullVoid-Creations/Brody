using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Dock.Model.Core;
using System;

namespace Brody.Client.Base;
internal sealed class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var name = param.GetType().FullName?.Replace("ViewModel", "View");
        if (string.IsNullOrEmpty(name))
            return null;

        var type = Type.GetType(name);
        if (type is not null)
            return Activator.CreateInstance(type) as Control;

        return new TextBlock { Text = $"View not found: {name}" };
    }

    public bool Match(object? data)
    {
        if (data is null) 
            return false;

        if (data is IDockable)
            return true;

        var name = data.GetType().FullName?.Replace("ViewModel", "View");
        if (string.IsNullOrEmpty(name))
            return false;

        return Type.GetType(name) is not null;
    }
}
