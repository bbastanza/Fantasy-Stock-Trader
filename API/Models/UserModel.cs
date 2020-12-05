using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace API.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public double Balance { get; set; }
        public double AllocatedFunds { get; set; }
        public List<HoldingModel> Holdings { get; set; } = new List<HoldingModel>();

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}