using Microsoft.AspNetCore.Mvc;
using Simulador_de_Encomendas_em_Drone.DTOs.Pedido;
using Simulador_de_Encomendas_em_Drone.Models;
using Simulador_de_Encomendas_em_Drone.Repositories;
using Simulador_de_Encomendas_em_Drone.Services;
namespace Simulador_de_Encomendas_em_Drone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly PedidoService _pedidoService;
        public PedidoController(PedidoRepository pedidoRepository, PedidoService pedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoService = pedidoService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var pedidos = _pedidoRepository.ObtemPedidos();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var pedido = _pedidoRepository.ObtemPedido(id);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CriaPedidoDTO pedidoDTO)
        {
            try
            {
                var pedido = new Pedido { Peso = pedidoDTO.Peso, ClienteX = pedidoDTO.ClienteX, ClienteY = pedidoDTO.ClienteY, Prioridade = pedidoDTO.Prioridade };
                var id = _pedidoRepository.CriaPedido(pedidoDTO);
                return Ok(new {id});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("DronesAtribuidos")]
        public IActionResult GetDronesPedidos()
        {
            try
            {
                var drones = _pedidoService.DistribuiPedidoAosDrones();
                return Ok(drones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
