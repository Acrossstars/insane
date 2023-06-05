namespace Core;

/// <summary>
/// 
/// </summary>
public class ObjectBuilderContext
{
    /// <summary>
    /// 
    /// </summary>
    public object Model { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string ObjectDataFilePath { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string TextTemplateFilePath { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string OutputFilePath { get; set; } = default!;
}