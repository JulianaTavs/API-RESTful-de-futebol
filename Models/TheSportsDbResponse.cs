using System.Text.Json.Serialization;

namespace API_RESTful_de_futebol.Models
{
    public class TheSportsDbTeamsResponse
    {
        [JsonPropertyName("teams")]
        public List<TheSportsDbTeamDto> Teams { get; set; }
    }

    // Representa os dados de um time dentro da resposta da TheSportsDB
    public class TheSportsDbTeamDto
    {
        [JsonPropertyName("idTeam")]
        public string Id { get; set; }

        [JsonPropertyName("strTeam")]
        public string Name { get; set; }

        [JsonPropertyName("strCountry")]
        public string Country { get; set; }
    }
}