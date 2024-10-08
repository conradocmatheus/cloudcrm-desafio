﻿using back_end.Models.Enums;

namespace back_end.DTOs.MovementDTOs;

public class MovementDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PaymentType PaymentType { get; set; }
    public double TotalValue { get; set; }
    public bool IsBlocked { get; set; }
    public List<MovementProductDto> Products { get; set; }
}

public class MovementProductDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}