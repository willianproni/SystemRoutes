using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCRouteSystem.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string NameTeam { get; set; }
        public string NamePerson { get; set; }
        public City City { get; set; }

        [NotMapped]
        public virtual List<SelectListItem> Citys { get; set; }
    }
}
