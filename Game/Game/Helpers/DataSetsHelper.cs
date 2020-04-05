using Game.ViewModels;
using System.Threading.Tasks;

namespace Game.Helpers
{
    /// <summary>
    /// Does wark up and wipe of all data
    /// </summary>
    static public class DataSetsHelper
    {
        /// <summary>
        /// Initialises all the data to be loaded
        /// </summary>
        /// <returns></returns>
        static public bool WarmUp()
        {
            ScoreIndexViewModel.Instance.GetCurrentDataSource();
            ItemIndexViewModel.Instance.GetCurrentDataSource();
            CharacterIndexViewModel.Instance.GetCurrentDataSource();
            MonsterIndexViewModel.Instance.GetCurrentDataSource();
            var battle = BattleEngineViewModel.Instance;
            return true;
        }

        /// <summary>
        /// Wipes data
        /// </summary>
        /// <returns></returns>
        static public async Task<bool> WipeData()
        {
            await ScoreIndexViewModel.Instance.WipeDataListAsync();
            await ItemIndexViewModel.Instance.WipeDataListAsync();
            await CharacterIndexViewModel.Instance.WipeDataListAsync();
            await MonsterIndexViewModel.Instance.WipeDataListAsync();
            return true;
        }
    }
}