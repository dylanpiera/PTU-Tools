using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTU_Tools_Web.Data
{
    public class PokemonTableModel
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Type of Column <see cref="ColType"/>
        /// </summary>
        public ColType ColType { get; set; }

        /// <summary>
        /// The Name of the Ability / Move
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Level / Type of ability (Basic, Advanced & High)
        /// Abilities can be referenced from enum <see cref="AbilityType"/>
        /// </summary>
        public int Requirement { get; set; }
    }

    public enum ColType
    {
        LevelUpMove = 1,
        TmMove = 2,
        TutorMove = 3,
        EggMove = 4,
        Ability = 5
    }

    public enum AbilityType
    {
        Basic = 101,
        Advanced = 102,
        High = 103,
    }
}
