using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoholBusTicketing.Core.DTOs;
using BoholBusTicketing.Core.Models;
using BoholBusTicketing.Core.Interfaces;

{
    
}

namespace BoholBusTicketing.Core.Services 
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IBarangayRepository _barangayRepository;

        private const decimal BaseFare = 12m;
        private const double BaseDistance = 5.0;
        private const decimal PerKm = 2.20m;

        public TicketService(ITicketRepository ticketRepository, IBarangayRepository barangayRepository)
        {
            _ticketRepository = ticketRepository;
            _barangayRepository = barangayRepository;
        }

        public decimal ComputeFare(double distance)
        {
            if (distance <= BaseDistance) return BaseFare;
            var extra = Math.Max(0.0, distance - BaseDistance);
            var fare = BaseFare + (decimal)extra * PerKm;
            return Math.Round(fare, 2);
        }

       public async Task<double> EstimateDistanceAsync(int fromBarangayId, int toBarangayId)
        {
            var from = await _barangayRepository.GetByIdAsync(fromBarangayId);
            var to = await _barangayRepository.GetByIdAsync(toBarangayId);

            if (from == null || to == null)
                throw new ArgumentException("Invalid barangay selection.");

            // Same barangay
            if (from.Id == to.Id)
                return 1.0;

            // Same municipality; estimate intra-municipality travel by barangay difference.
            if (from.MunicipalityId == to.MunicipalityId)
            {
                var distance = 3.0 + Math.Abs(from.Id - to.Id) * 0.4;
                return Math.Round(distance, 2);
            }

            if (from.Municipality == null || to.Municipality == null)
                throw new ArgumentException("Unable to resolve municipality for selected barangays.");

            if (DistanceCalculator.TryResolveMunicipalityCoordinate(from.Municipality, out var origin) &&
                DistanceCalculator.TryResolveMunicipalityCoordinate(to.Municipality, out var destination))
            {
                var distance = DistanceCalculator.CalculateDistance(
                    origin.Latitude,
                    origin.Longitude,
                    destination.Latitude,
                    destination.Longitude
                );

                return Math.Round(Math.Max(distance, 1.0), 2);
            }

            // Fallback estimate if coordinates are not available.
            var municipalityDelta = Math.Abs(from.MunicipalityId - to.MunicipalityId);
            var barangayDelta = Math.Abs(from.Id - to.Id);
            var estimatedDistance = 8.0 + municipalityDelta * 1.5 + barangayDelta * 0.12;
            return Math.Round(estimatedDistance, 2);
        }

        public async Task<int> CreateTicketAsync(CreateTicketInputDto dto, int? conductorId = null)
        {
            // Basic validation at service layer (repositories confirm existence)
            var from = await _barangayRepository.GetByIdAsync(dto.FromBarangayId);
            var to = await _barangayRepository.GetByIdAsync(dto.ToBarangayId);
            if (from == null || to == null) throw new ArgumentException("Invalid barangay selection.");

            var ticket = new Ticket
            {
                FromBarangayId = dto.FromBarangayId,
                ToBarangayId = dto.ToBarangayId,
                ConductorId = conductorId,
                Distance = dto.Distance,
                Fare = dto.Fare,
                DateCreated = DateTime.Now
            };

            var created = await _ticketRepository.AddAsync(ticket);
            return created.Id;
        }

        public async Task<TicketOutputDto?> GetTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null) return null;

            return new TicketOutputDto
            {
                Id = ticket.Id,
                FromBarangay = ticket.FromBarangay?.Name ?? string.Empty,
                FromMunicipality = ticket.FromBarangay?.Municipality?.Name ?? string.Empty,
                ToBarangay = ticket.ToBarangay?.Name ?? string.Empty,
                ToMunicipality = ticket.ToBarangay?.Municipality?.Name ?? string.Empty,
                ConductorName = ticket.Conductor?.Name ?? string.Empty,
                Distance = ticket.Distance,
                Fare = ticket.Fare,
                DateCreated = ticket.DateCreated
            };
        }
    }
}
