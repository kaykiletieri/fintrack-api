﻿namespace FinTrackAPI.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Uuid { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public void Update() => UpdatedAt = DateTime.UtcNow;
}
