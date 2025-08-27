using API_RESTful_de_futebol.Data;
using API_RESTful_de_futebol.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API_RESTful_de_futebol.Services
{
    public class FootballDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FootballDataService> _logger;
        private readonly FootballDbContext _dbContext;
        private const string TheSportsDbApiKey = "123";

        public FootballDataService(HttpClient httpClient, ILogger<FootballDataService> logger, FootballDbContext dbContext)
        {
            _httpClient = httpClient;
            _logger = logger;
            _dbContext = dbContext;
            _httpClient.BaseAddress = new Uri($"https://www.thesportsdb.com/api/v1/json/{TheSportsDbApiKey}/");
        }
        public async Task<List<TheSportsDbTeamDto>> GetTeamsAsync(int leagueId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"lookup_all_teams.php?id={leagueId}");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<TheSportsDbTeamsResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return apiResponse?.Teams ?? new List<TheSportsDbTeamDto>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Erro ao acessar a API de Futebol: {ex.Message}");
                return new List<TheSportsDbTeamDto>();
            }
        }
        public async Task FetchAndSaveTeamsByLeagueAsync(int leagueId)
        {
            try
            {
                var teamsFromApi = await GetTeamsAsync(leagueId);

                if (!teamsFromApi.Any())
                {
                    _logger.LogInformation("Nenhum dado de time encontrado na API.");
                    return;
                }

                foreach (var teamDto in teamsFromApi)
                {
                    if (int.TryParse(teamDto.Id, out int teamId))
                    {
                        var existingTeam = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
                        if (existingTeam == null)
                        {
                            var newTeam = new Team
                            {
                                Id = teamId,
                                Name = teamDto.Name,
                                Country = teamDto.Country
                            };
                            _dbContext.Teams.Add(newTeam);
                        }
                    }
                    else
                    {
                        _logger.LogError($"Erro: O ID do time '{teamDto.Id}' não é um número válido e não pode ser persistido.");
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao buscar e persistir os dados: {ex.Message}");
                throw;
            }
        }
        public async Task<TheSportsDbTeamDto> GetTeamDetailsAsync(int teamId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"lookupteam.php?id={teamId}");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<TheSportsDbTeamsResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return apiResponse?.Teams?.FirstOrDefault();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Erro ao buscar detalhes do time: {ex.Message}");
                return null;
            }
        }
    }
}