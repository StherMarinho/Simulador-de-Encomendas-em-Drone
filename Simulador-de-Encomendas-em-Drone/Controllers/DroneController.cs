using Simulador_de_Encomendas_em_Drone.Models;
using Simulador_de_Encomendas_em_Drone.Repositories;
using Microsoft.AspNetCore.Mvc;
using Simulador_de_Encomendas_em_Drone.DTOs.Drone;
using Simulador_de_Encomendas_em_Drone.Services;

namespace Simulador_de_Encomendas_em_Drone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly DroneRepository _droneRepository;
        private readonly DroneService _droneService;
        public DroneController (DroneRepository droneRepository, DroneService droneService)
        {
            _droneRepository = droneRepository;
            _droneService = droneService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var drones = _droneRepository.ObtemDrones();
                return Ok(drones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet ("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var drone = _droneRepository.ObtemDrone(id);
                return Ok(drone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CriaDroneDTO droneDTO)
        {
            try
            {
                var drone = new Drone { Nome = droneDTO.Nome, DistanciaMax = droneDTO.DistanciaMax, CargaMax = droneDTO.CargaMax };
                var id = _droneRepository.CriaDrone(droneDTO);
                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
