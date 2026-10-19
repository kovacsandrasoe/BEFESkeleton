using AutoMapper;
using BEFESkeleton.Data;
using BEFESkeleton.Dtos;
using BEFESkeleton.Models;
using BEFESkeleton.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BEFESkeleton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly SkeletonDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<SignalRHub> _hub;
        public CarController(SkeletonDbContext context, IMapper mapper, IHubContext<SignalRHub> hub)
        {
            _context = context;
            _mapper = mapper;
            _hub = hub;
        }

        [HttpPost]
        public async Task<ActionResult<CarViewDto>> CreateCar(CarCreateDto dto)
        {
            var entity = _mapper.Map<Car>(dto);
            _context.Cars.Add(entity);
            await _context.SaveChangesAsync();

            var fb = _mapper.Map<CarViewDto>(entity);
            await _hub.Clients.All.SendAsync("CarCreated", fb);

            return Ok(fb);
        }

        [HttpGet]
        public async Task<List<CarViewDto>> GetCars()
        {
            return await
                _context.Cars.Select(b => _mapper.Map<CarViewDto>(b)).ToListAsync();
        }
    }
}
