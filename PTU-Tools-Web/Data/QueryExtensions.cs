using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PTU_Tools_Web.Data
{
    public static class QueryExtensions
    {

        /// <summary>
        /// Get all LevelUp moves of <paramref name="query"/>
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        public static IQueryable<PokemonTableModel> LevelUpMoves(this IQueryable<PokemonTableModel> query) =>
            query.Where(x => x.ColType == ColType.LevelUpMove);

        /// <summary>
        /// Get all LevelUp moves of <paramref name="query"/> where the <paramref name="predicate"/> matches.
        /// 
        /// Example:
        /// Bulbasaur.WhereLevelUpMove(m => m.Requirement <= 7) returns a set with Tackle, Growl & Leech Seed in it.
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        /// <param name="predicate">Func Predicate - Example: m => m.Requirement <= 7 </param>
        public static IQueryable<PokemonTableModel> WhereLevelUpMove(this IQueryable<PokemonTableModel> query, Expression<Func<PokemonTableModel, bool>> predicate) =>
            query.Where(x => x.ColType == ColType.LevelUpMove).Where(predicate);

        /// <summary>
        /// Get the LevelUp move of <paramref name="query"/> where the required level is <paramref name="level"/> 
        /// 
        /// Example:
        /// Bulbasaur.LevelUpMove(1) returns Tackle
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        /// <param name="level">Pokemon level</param>
        public static PokemonTableModel LevelUpMove(this IQueryable<PokemonTableModel> query, int level) =>
            query.FirstOrDefault(x => x.ColType == ColType.LevelUpMove && x.Requirement == level);

        /// <summary>
        /// Get the first LevelUp moves of <paramref name="query"/> where the <paramref name="predicate"/> matches.
        /// 
        /// Example:
        /// Bulbasaur.LevelUpMove(m => m.Requirement == 1) returns Tackle.
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        /// <param name="predicate">Func Predicate - Example: m => m.Requirement == 1 </param>
        public static PokemonTableModel LevelUpMove(this IQueryable<PokemonTableModel> query, Func<PokemonTableModel, bool> predicate) =>
            query.Where(x => x.ColType == ColType.LevelUpMove).FirstOrDefault(predicate);

        /// <summary>
        /// Checks whether <paramref name="query"/> learns a move at <paramref name="level"/>
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        /// <param name="level">Pokemon level</param>
        public static bool HasLevelUpMoveAtLevel(this IQueryable<PokemonTableModel> query, int level) =>
            query.Any(x => x.ColType == ColType.LevelUpMove && x.Requirement == level);

        /// <summary>
        /// Checks whether <paramref name="query"/> learns move by levelup <paramref name="move"/>
        /// 
        /// Returns true & the level it learns the move at, or false & -1
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        /// <param name="move">Name of the move</param>
        public static (bool learnsMove, int atLevel) LearnsLevelUpMove(this IQueryable<PokemonTableModel> query, string move)
        {
            var result = query.FirstOrDefault(x => x.ColType == ColType.LevelUpMove && x.Name.ToLower() == move.ToLower());
            return (result != null, result?.Id ?? -1);
        }

        /// <summary>
        /// Get all Abilities of <paramref name="query"/>
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        public static IQueryable<PokemonTableModel> Abilities(this IQueryable<PokemonTableModel> query) =>
            query.Where(x => x.ColType == ColType.Ability);

        /// <summary>
        /// Get all EggMoves of <paramref name="query"/>
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        public static IQueryable<PokemonTableModel> EggMoves(this IQueryable<PokemonTableModel> query) =>
            query.Where(x => x.ColType == ColType.EggMove);

        /// <summary>
        /// Get all TmMoves of <paramref name="query"/>
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        public static IQueryable<PokemonTableModel> TmMoves(this IQueryable<PokemonTableModel> query) =>
            query.Where(x => x.ColType == ColType.TmMove);

        /// <summary>
        /// Get all TutorMoves of <paramref name="query"/>
        /// </summary>
        /// <param name="query">Pokemon Query. <seealso cref="ApplicationDbContext.GetPokemonTable(string)"/></param>
        public static IQueryable<PokemonTableModel> TutorMoves(this IQueryable<PokemonTableModel> query) =>
            query.Where(x => x.ColType == ColType.TutorMove);



    }
}
