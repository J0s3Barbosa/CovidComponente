using Services.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Services.Application.Interfaces
{
    public interface IAirPlaneLogic : IGenericLogic<AirPlane>
    {
        int? Delete(Guid id);
        Result<AirPlane> AddEntity(AirPlane Entity);
        List<AirPlane> List(string code, string model, short? numberOfPassengers, DateTime? creationDate);
        Result<AirPlane> Update(Guid identifier, AirPlane Entity);
    }
}
