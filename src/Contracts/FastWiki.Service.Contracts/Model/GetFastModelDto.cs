namespace FastWiki.Service.Contracts.Model;

public class GetFastModelDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    /// <summary>
    /// ģ������
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// AI֧�ֵ�ģ��
    /// </summary>
    public List<string> Models { get; set; } = [];
}