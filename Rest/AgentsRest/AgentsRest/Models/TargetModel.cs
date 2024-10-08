﻿namespace AgentsRest.Models
{
    public enum StatusTarget
    {
        Dead = 0,
        Live = 1
    }
    public enum StatusMission
    {
        InActive = 0,
        Active = 1
    }
    public class TargetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; } 
        public string PhotoUrl { get; set; }
        public StatusTarget Status { get; set; } = StatusTarget.Live;
        public StatusMission InMission { get; set; } = StatusMission.InActive;
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        
    }
}
