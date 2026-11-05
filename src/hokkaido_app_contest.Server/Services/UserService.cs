using hokkaido_app_contest.Server.Models.Contexts;
using hokkaido_app_contest.Shared.Interfaces.Services;
using MagicOnion;
using MagicOnion.Server;

namespace hokkaido_app_contest.Server.Services {
    public class UserService : ServiceBase<IUserService>, IUserService {
        private readonly GameDbContext _context;

        // 排他制御用
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        // DI
        //public UserService(GameDbContext context) {
        //    _context = context;
        //}

        // タイムゾーン設定
        private TimeZoneInfo tokyoTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        /// <summary>
        /// 時間取得
        /// </summary>
        private DateTime GetTimeNow() {
            return TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo);
        }

        /// <summary>
        /// 接続テスト
        /// </summary>
        public UnaryResult ConnectionTest() {
            Console.WriteLine("接続");
            return UnaryResult.CompletedResult;

        }
    }
}
