﻿using back_end.DTOs.MovementDTOs;
using back_end.Helpers;
using back_end.Models;
using back_end.Models.Enums;

namespace back_end.Services.MovementServices;

public interface IMovementService
{
    Task<MovementDto> CreateMovementAsync(CreateMovementDto createMovementDto);
    Task<List<GetAllMovementsWithUserInfoDto>> GetAllMovementsPaginatedAsync(QueryObject query);
    Task<List<GetAllMovementsDto>> GetAllMovementsByPaymentTypeAsync(PaymentType paymentType);
    Task<Movement?> DeleteMovementByIdAsync(Guid id);

    Task<List<ExportMovementDto>> GetMovementsByFilterAsync(string filterType, int? month = null, int? year = null);

    Task<double> GetTotalValueDebitAsync();
    Task<double> GetTotalValueCreditAsync();
    Task<double> GetTotalValueMovementsAsync();
}