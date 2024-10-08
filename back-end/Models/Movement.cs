﻿using back_end.Models.Enums;

namespace back_end.Models;

public class Movement
{
    // Propriedades da classe Movement
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PaymentType PaymentType { get; set; }
    public double TotalValue { get; set; }
    public bool IsBlocked { get; set; }

    // Relação muitos para muitos com Product
    public IList<MovementProduct> MovementProducts { get; set; } = new List<MovementProduct>();

    public Guid UserId { get; set; } // Chave estrangeira para User
    public User User { get; set; } // Navegação para o User
}