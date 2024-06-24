using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using PABlabZalApi.Core.Entities;
using PABlabZalApi.Core.Interfaces;
using PABlabZalApi.Grpc;

namespace PABlabZalApi.GrpcServices
{
    public class CarServiceImpl : PABlabZalApi.Grpc.CarService.CarServiceBase
    {
        private readonly ICarService _carService;

        public CarServiceImpl(ICarService carService)
        {
            _carService = carService;
        }

        public override async Task<CarResponse> GetCar(GetCarRequest request, ServerCallContext context)
        {
            var car = await _carService.GetCarByIdAsync(request.Id);
            if (car == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Car not found"));
            }
            return new CarResponse { Car = ConvertToGrpcCar(car) };
        }

        public override async Task<CarsResponse> GetCars(Empty request, ServerCallContext context)
        {
            var cars = await _carService.GetCarsAsync();
            var response = new CarsResponse();
            response.Cars.AddRange(cars.Select(c => ConvertToGrpcCar(c)));
            return response;
        }

        public override async Task<CarResponse> AddCar(AddCarRequest request, ServerCallContext context)
        {
            var car = ConvertFromGrpcCar(request.Car);
            await _carService.AddCarAsync(car);
            return new CarResponse { Car = ConvertToGrpcCar(car) };
        }

        public override async Task<CarResponse> UpdateCar(UpdateCarRequest request, ServerCallContext context)
        {
            var car = ConvertFromGrpcCar(request.Car);
            await _carService.UpdateCarAsync(car);
            return new CarResponse { Car = ConvertToGrpcCar(car) };
        }

        public override async Task<Empty> DeleteCar(DeleteCarRequest request, ServerCallContext context)
        {
            await _carService.DeleteCarAsync(request.Id);
            return new Empty();
        }

        private PABlabZalApi.Core.Entities.Car ConvertFromGrpcCar(PABlabZalApi.Grpc.Car grpcCar)
        {
            return new PABlabZalApi.Core.Entities.Car
            {
                Id = grpcCar.Id,
                MadeBy = grpcCar.MadeBy,
                Model = grpcCar.Model,
                LicensePlate = grpcCar.LicensePlate,
                PricePerDay = decimal.Parse(grpcCar.PricePerDay)
            };
        }

        private PABlabZalApi.Grpc.Car ConvertToGrpcCar(PABlabZalApi.Core.Entities.Car car)
        {
            return new PABlabZalApi.Grpc.Car
            {
                Id = car.Id,
                MadeBy = car.MadeBy,
                Model = car.Model,
                LicensePlate = car.LicensePlate,
                PricePerDay = car.PricePerDay.ToString()
            };
        }
    }
}