using System;
using System.Collections.Generic;

namespace Bang.WebApi.Requests
{
    public class EndTurnRequest
    {
        public IEnumerable<Guid> CardIds { get; set; } = new List<Guid>();
    }
}
