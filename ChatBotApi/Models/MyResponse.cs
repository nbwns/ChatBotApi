
using System;

public class StatusResponse
{
    public string operationState { get; set; }
    public DateTime createdTimestamp { get; set; }
    public DateTime lastActionTimestamp { get; set; }
    public string resourceLocation { get; set; }
    public string userId { get; set; }
    public string operationId { get; set; }
}
