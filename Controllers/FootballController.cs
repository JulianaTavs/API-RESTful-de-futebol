using API_RESTful_de_futebol.Data;
using API_RESTful_de_futebol.Models;
using API_RESTful_de_futebol.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_RESTful_de_futebol.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballController : ControllerBase
    {
        private readonly FootballDataService _dataService;
        private readonly FootballDbContext _dbContext;

        public FootballController(FootballDataService dataService, FootballDbContext dbContext)
        {
            _dataService = dataService;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Busca dados de times da TheSportsDB e os persiste no banco de dados.
        /// </summary>
        /// <param name="leagueId">O ID da liga (ex: 71 para o Brasileirão Série A).</param>
        [HttpPost("fetch-teams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchAndPersistTeams([FromQuery] int leagueId)
        {
            try
            {
                await _dataService.FetchAndSaveTeamsByLeagueAsync(leagueId);
                return Ok("Dados de times buscados e persistidos com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca os detalhes de um time por ID na TheSportsDB.
        /// </summary>
        /// <param name="teamId">O ID do time.</param>
        /// <returns>Os detalhes do time, ou 404 se não encontrado.</returns>
        [HttpGet("team-details/{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TheSportsDbTeamDto>> GetTeamDetails(int teamId)
        {
            try
            {
                var team = await _dataService.GetTeamDetailsAsync(teamId);

                if (team == null)
                {
                    return NotFound($"Time com ID {teamId} não encontrado.");
                }

                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém todos os times do banco de dados.
        /// </summary>
        /// <returns>Uma lista de objetos de times ou uma mensagem de "não encontrado".</returns>
        [HttpGet("all-teams")]
        public IActionResult GetAllTeams()
        {
            var teams = _dbContext.Teams.ToList();
            if (teams == null || !teams.Any())
            {
                return NotFound("Nenhum time encontrado no banco de dados.");
            }
            return Ok(teams);
        }
    }
}